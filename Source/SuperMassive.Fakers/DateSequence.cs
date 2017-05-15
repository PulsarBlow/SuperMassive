using System;

namespace SuperMassive.Fakers
{
    public class DateSequence
    {
        private readonly DateTime _sequenceStart;
        private readonly TimeSpan _maxTimeBetween;
        private DateTime _lastGenerated = DateTime.MinValue;

        public DateSequence(DateTime sequenceStart, TimeSpan maxTimeBetween)
        {
            _sequenceStart = sequenceStart;
            _maxTimeBetween = maxTimeBetween;
        }
        public DateTime Next()
        {
            if (_lastGenerated == DateTime.MinValue)
                return First();
            _lastGenerated = _lastGenerated.AddSeconds(
                RandomNumberGenerator.Int(1, (int)_maxTimeBetween.TotalSeconds));
            return _lastGenerated;
        }
        public void Reset()
        {
            _lastGenerated = DateTime.MinValue;
        }
        private DateTime First()
        {
            int maxSec = (int)_maxTimeBetween.TotalSeconds;
            _lastGenerated = _sequenceStart.AddSeconds(RandomNumberGenerator.Int(1, maxSec));
            return _lastGenerated;
        }
    }
}
