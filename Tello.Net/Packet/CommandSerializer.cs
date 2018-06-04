using NLog;
using System;
using System.IO;
using System.Text;
using Tello.Net.Crc;
using Tello.Net.Commands;

namespace Tello.Net.Packet
{
    public class CommandSerializer
    {
        private const int HeaderSize = 11;
        private const int AckSize = 11;
        private const byte AckType = 0x63;
        private const ushort VideoPort = 6037;

        private static readonly Encoding encoding = Encoding.ASCII;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public byte[] Write(byte cmdType, ushort cmdId, ushort seqId, byte[] data)
        {
            int size = data.Length + HeaderSize;
            MemoryStream outStream = new MemoryStream();
            EndianBinaryWriter writer = EndianBinaryWriter.FromStream(outStream, true);
            writer.Write(0xcc);
            writer.Write((ushort)size << 3);
            writer.Write(CrcCalculator.Crc8(outStream.ToArray()));
            writer.Write(cmdType);
            writer.Write(cmdId);
            writer.Write(seqId);
            if (data != null)
            {
                writer.Write(data);
            }
            writer.Write(CrcCalculator.Crc16(outStream.ToArray()));
            return outStream.ToArray();
        }

        public TelloCommand Read(byte[] data)
        {
            EndianBinaryReader reader = new EndianBinaryReader(data);
            Stream inStream = reader.BaseSteam;
            byte[] header = reader.ReadBytes(3);
            inStream.Position = 0;

            byte fid = reader.ReadByte();

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
            byte[] messageData = reader.ReadBytes(dataSize);
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

            return new TelloCommand(packetType, (TelloCommandId)commandId, sequenceId, messageData);
        }
    }
}
