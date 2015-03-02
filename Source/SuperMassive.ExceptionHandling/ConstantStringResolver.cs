using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMassive.ExceptionHandling
{
    /// <summary>
    /// Resolves strings by returning a constant value.
    /// </summary>
    public sealed class ConstantStringResolver : IStringResolver
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConstantStringResolver"/> with a constant value.
        /// </summary>
        public ConstantStringResolver(string value)
        {
            this.value = value;
        }

        private readonly string value;

        string IStringResolver.GetString()
        {
            return this.value;
        }
    }
}
