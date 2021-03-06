﻿namespace SuperMassive.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Provides extensions methods for the <see cref="Expression"/> type.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Compose
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="merge"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<T> Compose<T>(
            this Expression<T> first,
            Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new {f, s = second.Parameters[i]})
                .ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ReplaceParameterVisitor.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// Conditional And (AndAlso). Will be evaluated only if the first operand evaluates to true;
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        /// <summary>
        /// Conditional Or (OrElse). Will be evaluated only if the first operand evaluates to false;
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }

        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

            private ReplaceParameterVisitor(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(
                Dictionary<ParameterExpression, ParameterExpression> map,
                Expression exp)
            {
                return new ReplaceParameterVisitor(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                if (_map.TryGetValue(p, out var replacement))
                {
                    p = replacement;
                }

                return base.VisitParameter(p);
            }
        }
    }
}
