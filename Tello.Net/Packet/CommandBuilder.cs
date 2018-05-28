using System.IO;

namespace Tello.Net.Packet
{
    public class CommandTranslator
    {
       public byte[] Build(byte type, ushort cmdId, ushort seqId, byte[] data)
       {
            int size = data.Length + 11;
            MemoryStream outStream = new MemoryStream();
            EndianBinaryWriter writer = EndianBinaryWriter.FromMemoryStream(outStream);
            //byte[] header = new byte[] { 0xcc, }
            writer.Write(0xCC);
            writer.Write((ushort)size << 3);
            //byte crc8 = Crc.Crc8(header);
            return null;

       }
    }
}
