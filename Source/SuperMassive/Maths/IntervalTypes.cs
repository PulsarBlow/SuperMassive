namespace SuperMassive.Maths
{
    /// <summary>
    /// Enumerates the different existing interval types.
    /// </summary>
    public enum IntervalTypes
    {
        /// <summary>
        /// Not set, unknown or undefined.
        /// </summary>
        NotSet,
        /// <summary>
        /// Half inclusive : [x;y[
        /// </summary>
        HalfInclusive,
        /// <summary>
        /// Inclusive : [x;y]
        /// </summary>
        Inclusive,
        /// <summary>
        /// Half exclusive : ]x;y]
        /// </summary>
        HalfExclusive,
        /// <summary>
        /// Exclusive : ]x;y[
        /// </summary>
        Exclusive
    }
}
