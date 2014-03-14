
namespace SuperMassive.Maths
{
    /// <summary>
    /// Real number interval
    /// </summary>
    public class RealInterval
    {
        #region Properties
        /// <summary>
        /// Upper interval limit
        /// </summary>
        public double UpperLimit
        {
            get;
            set;
        }
        /// <summary>
        /// Lower interval limit
        /// </summary>
        public double LowerLimit
        {
            get;
            set;
        }
        /// <summary>
        /// Interval type
        /// </summary>
        public IntervalTypes IntervalType
        {
            get;
            set;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns true if the given real number is in interval limits
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsValueInLimits(double value)
        {
            switch (this.IntervalType)
            {
                case IntervalTypes.HalfInclusive:
                    return value >= this.LowerLimit && value < this.UpperLimit;
                case IntervalTypes.Inclusive:
                    return value >= this.LowerLimit && value <= this.UpperLimit;
                case IntervalTypes.HalfExclusive:
                    return value > this.LowerLimit && value <= this.UpperLimit;
                case IntervalTypes.Exclusive:
                    return value > this.LowerLimit && value < this.UpperLimit;
                case IntervalTypes.NotSet:
                default:
                    return false;
            }
        }
        #endregion
    }
}
