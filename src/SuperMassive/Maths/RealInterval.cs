namespace SuperMassive.Maths
{
    /// <summary>
    /// Real number interval
    /// </summary>
    public class RealInterval
    {
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

        /// <summary>
        /// Returns true if the given real number is in interval limits
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsValueInLimits(double value)
        {
            switch (IntervalType)
            {
                case IntervalTypes.HalfInclusive:
                    return value >= LowerLimit && value < UpperLimit;
                case IntervalTypes.Inclusive:
                    return value >= LowerLimit && value <= UpperLimit;
                case IntervalTypes.HalfExclusive:
                    return value > LowerLimit && value <= UpperLimit;
                case IntervalTypes.Exclusive:
                    return value > LowerLimit && value < UpperLimit;
                default:
                    return false;
            }
        }
    }
}
