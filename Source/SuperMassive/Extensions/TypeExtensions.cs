#nullable enable

namespace SuperMassive.Extensions
{
    using System;

    public static class TypeExtensions
    {
        public static bool IsNullable(this Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
