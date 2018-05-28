namespace Tello.Net.Crc
{
    public class Crc
    {
        private const ushort InitSeed16 = 0x3692;
        private const byte   InitSeed8  = 0x77;

        public ushort Calculate16(byte[] data)
        {
            ushort crc = InitSeed16;
            foreach (byte b in data)
            {
                int idx = (crc ^ b) & 0xff;
                crc = (ushort)(CrcTables.Crc16Table[idx] ^ (crc >> 8));
            }
            return crc;
        }

        public byte Calculate8(byte[] data)
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
