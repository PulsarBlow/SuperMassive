namespace SuperMassive.Tests.Extensions
{
    using System;
    using System.Linq.Expressions;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class ExpressionExtensionsTest
    {
        [TestCase(-1, false)]
        [TestCase(11, false)]
        [TestCase(0, true)]
        [TestCase(10, true)]
        public void Compose_Returns_Composed_Expression(int value, bool expected)
        {
            Expression<Func<int, bool>> first = x => x >= 0;
            Expression<Func<int, bool>> second = x => x <= 10;

            var composed = first.Compose(second, Expression.AndAlso);

            // An integer between 0 and 10 ?
            Assert.That(composed.Compile().Invoke(value), Is.EqualTo(expected));
        }

        [TestCase(-1, false)]
        [TestCase(11, false)]
        [TestCase(0, true)]
        [TestCase(10, true)]
        public void And_Conditionally_Evaluates_Second_Expression(int value, bool expected)
        {
            Expression<Func<int, bool>> first = x => x >= 0;
            Expression<Func<int, bool>> second = x => x <= 10;

            var composed = first.And(second);

            // An integer between 0 and 10 ?
            Assert.That(composed.Compile().Invoke(value), Is.EqualTo(expected));
        }

        [TestCase(-1, true)]
        [TestCase(11, true)]
        [TestCase(0, true)]
        [TestCase(10, true)]
        public void Or_Conditionally_Evaluates_Second_Expression(int value, bool expected)
        {
            Expression<Func<int, bool>> first = x => x >= 0;
            Expression<Func<int, bool>> second = x => x <= 10;

            var composed = first.Or(second);

            // An integer between 0 and 10 ?
            Assert.That(composed.Compile().Invoke(value), Is.EqualTo(expected));
        }
    }
}
