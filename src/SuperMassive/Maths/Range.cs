namespace SuperMassive.Maths
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines a range
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DataContractAttribute]
    public class Range<T>
        where T : struct
    {
        /// <summary>
        /// Lower bound limit of the range
        /// </summary>
        [DataMember]
        public T Min { get; set; }

        /// <summary>
        /// Upper bound limit of the range
        /// </summary>
        [DataMember]
        public T Max { get; set; }
    }
}
