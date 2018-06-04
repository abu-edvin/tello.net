namespace Tello.Net.Crc
{
    public class CrcCalculator
    {
        private const ushort InitSeed16 = 0x3692;
        private const byte   InitSeed8  = 0x77;

        public static ushort Crc16(byte[] data)
        {
            ushort crc = InitSeed16;
            foreach (byte b in data)
            {
                int idx = (crc ^ b) & 0xff;
                crc = (ushort)((ushort)CrcTables.Crc16Table[idx] ^ (ushort)(crc >> 8));
            }
            return crc;
        }

        public static byte Crc8(byte[] data)
        {
            byte crc = InitSeed8;
            foreach (byte b in data)
            {
                int idx = (crc ^ b) & 0xff;
                crc = CrcTables.Crc8Table[idx];
            }
            return crc;
        }
    }
}
