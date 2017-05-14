namespace SuperMassive.Fakers
{
    using System;
    using SuperMassive.Extensions;

    public class DistributedDateSequence
    {
        //private Random _rnd = new Random();
        private double _secBetweenDates;
        private double _start;
        private double _end;
        private double _last;
        private float _variance;

        /// <summary>
        /// Creates a new instance of the distributed date sequence
        /// </summary>
        /// <param name="sequenceStart">Minimum date for the sequence</param>
        /// <param name="sequenceEnd">Maximum date for the sequence</param>
        /// <param name="sequenceLength">Estimated number of date generations</param>
        /// <param name="percentVariance">A number between 0 and 100. 0% means date will be equally distributed. 100% means that for each generation,  the interval can double</param>
        public DistributedDateSequence(DateTime sequenceStart, DateTime sequenceEnd, int sequenceLength, int percentVariance = 0)
        {
            _start = sequenceStart.ToUnixTime();
            _end = sequenceEnd.ToUnixTime();
            _secBetweenDates = (sequenceEnd.ToUnixTime() - sequenceStart.ToUnixTime()) / sequenceLength;
            if (_secBetweenDates < 1) _secBetweenDates = 1;

            if (percentVariance < 0)
                percentVariance = 0;
            if (percentVariance > 100)
                percentVariance = 100;
            _variance = percentVariance / 100;

        }
        public DateTime Next()
        {
            if (_last == 0)
            {
                _last = _start;
                return DateHelper.FromUnixTime(_last);
            }

            _last = _last + (_secBetweenDates * RandomNumberGenerator.Float(1F - _variance, 1F + _variance));
            if (_last > _end)
                _last = _start; // restart from the beginning to not go over end limit
            return DateHelper.FromUnixTime(_last);
        }
    }
}
