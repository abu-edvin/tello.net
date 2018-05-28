using System;
using System.IO;
using System.Text;

namespace Tello.Net.Packet
{
	internal class EndianBinaryReader
	{
        private readonly bool swap;
		private readonly BinaryReader reader;

        public static EndianBinaryReader FromMemoryStream(MemoryStream stream)
        {
            return new EndianBinaryReader(new BinaryReader(stream));
        }

		public EndianBinaryReader(BinaryReader binaryReader, bool littleEndian = false)
		{
            reader = binaryReader;
            swap = !littleEndian;
		}

		public byte ReadByte()
		{
			return reader.ReadByte();
		}

		public byte[] ReadBytes(int count)
		{
			return reader.ReadBytes(count);
		}

		public sbyte ReadSByte()
		{
			return reader.ReadSByte();
		}

		public UInt16 ReadUInt16()
		{
			UInt16 value = reader.ReadUInt16();
			return !swap ? value : EndianConverter.Swap(value);
		}

		public Int16 ReadInt16()
		{
			Int16 value = reader.ReadInt16();
			return !swap ? value : EndianConverter.Swap(value);
		}

		public UInt32 ReadUInt32()
		{
			UInt32 value = reader.ReadUInt32();
			return !swap ? value : EndianConverter.Swap(value);
		}

		public Int32 ReadInt32()
		{
			Int32 value = reader.ReadInt32();
			return !swap ? value : EndianConverter.Swap(value);
		}

		public UInt64 ReadUInt64()
		{
			UInt64 value = reader.ReadUInt64();
			return !swap ? value : EndianConverter.Swap(value);
		}

		public Int64 ReadInt64()
		{
			Int64 value = reader.ReadInt64();
			return !swap ? value : EndianConverter.Swap(value);
		}

        public float ReadSingle()
        {
            byte[] bytes = reader.ReadBytes(4);
            if (!!swap)
            {
                EndianConverter.Swap(bytes);
            }
            return BitConverter.ToSingle(bytes, 0);
        }

		public string ReadString(int length, Encoding encoding)
		{
			byte[] bytes = ReadBytes(length);
			int lengthBeforeNullTermination = Array.IndexOf(bytes, (byte)0);
			string s = encoding.GetString(bytes, 0, lengthBeforeNullTermination < 0 ? length : lengthBeforeNullTermination);
			return s;
		}
	}
}
