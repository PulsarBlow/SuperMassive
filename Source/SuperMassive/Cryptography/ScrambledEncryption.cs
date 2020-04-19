namespace SuperMassive.Cryptography
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using Extensions;

    /// <summary>
    /// Scramble Encryption
    /// </summary>
    public class ScrambledEncryption
    {
        private readonly string _key;
        private readonly string _scramble1 = "0123456789-ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
        private readonly string _scramble2 = "UKAH652LMOQ FBDIEG03JT17N4C89XPV-WRSYZ";
        private readonly float _adj = 1.75F;
        private readonly int _mod = 3;

        /// <summary>
        /// Creates a new instance of the <see cref="ScrambledEncryption"/> class.
        /// </summary>
        /// <param name="key"></param>
        public ScrambledEncryption(string key)
        {
            Guard.ArgumentNotNullOrEmpty(key, nameof(key));
            _key = key;
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="source"></param>
        /// <param name="minLength"></param>
        /// <returns></returns>
        public string Encrypt(string source, int minLength = 8)
        {
            Guard.ArgumentNotNullOrEmpty(source, nameof(source));

            var fudgeFactor = ConvertKey(_key);

            source = source.PadRight(minLength);
            var target = new StringBuilder(minLength);
            var factor2 = 0F;

            foreach (var @char in source)
            {
                var num1 = _scramble1.IndexOf(@char);
                if (num1 == -1)
                {
                    throw new InvalidOperationException($"Source string contains an invalid character ({@char})");
                }

                var adj = ApplyFudgeFactor(fudgeFactor);
                var factor1 = factor2 + adj;
                var num2 = (int)Math.Round(factor1) + num1;
                num2 = CheckRange(num2);
                factor2 = factor1 + num2;

                char c2 = _scramble2[num2];
                target.Append(c2);
            }
            return target.ToString();
        }

        /// <summary>
        /// Decrypt the given value
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string? Decrypt(string source)
        {
            Guard.ArgumentNotNullOrEmpty(source, "source");

            var fudgeFactor = ConvertKey(_key);
            var target = new StringBuilder();
            var factor2 = 0F;

            foreach (var @char in source)
            {
                int num2 = _scramble2.IndexOf(@char);
                if (num2 == -1)
                    return null;
                var adj = ApplyFudgeFactor(fudgeFactor);
                var factor1 = factor2 + adj;
                var num1 = num2 - (int)Math.Round(factor1);
                num1 = CheckRange(num1);
                factor2 = factor1 + num2;
                char c1 = _scramble1[num1];
                target.Append(c1);
            }
            return target.ToString();
        }

        private float ApplyFudgeFactor(Queue<float> fudgeFactor)
        {
            var fudge = fudgeFactor.Dequeue();
            fudge += _adj;
            fudgeFactor.Enqueue(fudge);
            if (_mod != 0 && ((double)fudge % _mod).AlmostEquals(0))
            {
                fudge *= -1;
            }
            return fudge;

        }

        private int CheckRange(int num)
        {
            var limit = _scramble1.Length;
            while (num >= limit)
            {
                num -= limit;
            }
            while (num < 0)
            {
                num += limit;
            }
            return num;
        }

        private Queue<float> ConvertKey(string key)
        {
            Guard.ArgumentNotNullOrEmpty(key, "key");

            var result = new Queue<float>();
            result.Enqueue(key.Length); // first entry in array is length of key
            var total = 0;
            foreach (var c in key)
            {
                int pos = _scramble1.IndexOf(c);
                if (pos == -1)
                    throw new InvalidOperationException($"Key contains an invalid character ({c})");

                result.Enqueue(pos);
                total += pos;
            }
            result.Enqueue(total); // last entry in array is computed total
            return result;
        }
    }
}
