using System.Security.Cryptography;

namespace SuperMassive.Cryptography
{
    using System;

    /// <summary>
    /// Implements a 16-bit CRC hash of type CRC-16-CCITT
    /// </summary>
    /// <remarks>
    /// X.25, V.41, HDLC FCS, XMODEM, Bluetooth, PACTOR, SD, many others; known as CRC-CCITT
    /// </remarks>
    internal sealed class Crc16Ccitt
    {
        private const uint DefaultPolynomial = 0x8408;
        private ushort[] _table;

        public Crc16Ccitt()
        {
            InitializeTable();
        }

        public ushort ComputeHash(byte[] buffer)
        {
            ushort crc = 0;
            for (int i = 0; i < buffer.Length; ++i)
            {
                crc = (ushort) (_table[(crc ^ buffer[i]) & 0xFF] ^ (crc >> 8));
            }

            return crc;
        }

        public byte[] ComputeHashBytes(byte[] buffer)
        {
            return BitConverter.GetBytes(ComputeHash(buffer));
        }

        private void InitializeTable()
        {
            _table = new ushort[256];
            for (ushort i = 0; i < 256; ++i)
            {
                ushort value = 0;
                ushort tmp = i;
                for (int j = 0; j < 8; ++j)
                {
                    if (((value ^ tmp) & 0x0001) > 0)
                        value = (ushort) ((value >> 1) ^ DefaultPolynomial);
                    else
                        value = (ushort) (value >> 1);
                    tmp = (ushort) (tmp >> 1);
                }

                _table[i] = value;
            }
        }
    }
}
