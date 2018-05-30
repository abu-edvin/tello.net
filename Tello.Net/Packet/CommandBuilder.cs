using NLog;
using System;
using System.IO;
using System.Text;
using Tello.Net.Crc;
using Tello.Net.Commands;

namespace Tello.Net.Packet
{
    public class CommandTranslator
    {
        private const int HeaderSize = 11;
        private const int AckSize = 11;
        private const byte CommandType = 0xcc;
        private const byte AckType = 0x63;
        private const ushort VideoPort = 6037;

        private static readonly Encoding encoding = Encoding.ASCII;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public byte[] Build(byte type, ushort cmdId, ushort seqId, byte[] data)
        {
            int size = data.Length + HeaderSize;
            MemoryStream outStream = new MemoryStream();
            EndianBinaryWriter writer = EndianBinaryWriter.FromStream(outStream);
            writer.Write(0xCC);
            writer.Write((ushort)size << 3);
            writer.Write(CrcCalculator.Crc8(outStream.ToArray()));
            writer.Write(type);
            writer.Write(cmdId);
            writer.Write(seqId);
            if (data != null)
            {
                writer.Write(data);
            }
            writer.Write(CrcCalculator.Crc16(outStream.ToArray()));
            return outStream.ToArray();
        }

        public void Parse(byte[] data)
        {
            if (data.Length < HeaderSize)
            {
                throw new TelloException(
                    $"Packet length {data.Length:d} does not " +
                    $"match expected {data[0]}.");
            }
            MemoryStream inStream = new MemoryStream();
            EndianBinaryReader reader = EndianBinaryReader.FromStream(inStream);
            byte type = reader.ReadByte();
            switch (type)
            {
                case CommandType:
                    ParseCommand(inStream, reader);
                    break;
                case AckType:
                    ParseAck(reader);
                    break;
            }
        }

        private void ParseCommand(Stream inStream, EndianBinaryReader reader)
        {
            byte[] header = reader.ReadBytes(3);
            inStream.Position = 0;

            ushort size = (ushort)(reader.ReadUInt16() >> 3);
            if (size < 11)
            {
                throw new TelloException(
                    $"Packet too small, expected at least {size}.");
            }
            byte crc8 = reader.ReadByte();
            byte calcCrc8 = CrcCalculator.Crc8(header);
            if (crc8 != calcCrc8)
            {
                throw new TelloException(
                    $"CRC8 mismatch, {crc8:x} != {calcCrc8:x}.");
            }

            byte packetType = reader.ReadByte();
            ushort commandId = reader.ReadUInt16();
            ushort sequenceId = reader.ReadUInt16();
            ushort dataSize = (ushort)(size - HeaderSize);
            byte[] data = reader.ReadBytes(dataSize);
            ushort crc16 = reader.ReadUInt16();

            long pos = inStream.Position;
            inStream.Position = 0;
            byte[] crc16Data = reader.ReadBytes(size);
            inStream.Position = pos;
            ushort calcCrc16 = CrcCalculator.Crc16(crc16Data);
            if (crc16 != calcCrc16)
            {
                throw new TelloException(
                    $"CRC16 mismatch, {crc16:x} != {calcCrc16:x}.");
            }
        }

        private void ParseAck(EndianBinaryReader reader)
        {
            MemoryStream outStream = new MemoryStream();
            EndianBinaryWriter writer = EndianBinaryWriter.FromStream(outStream);
            writer.Write(encoding.GetBytes("conn_ack:"));
            writer.Write(VideoPort);
            if (!Array.Equals(outStream.ToArray(), reader.ReadBytes(HeaderSize)))
            {
                throw new TelloException("Connection not acknowledged.");
            }

        }
    }
}
