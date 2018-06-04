using System;
using System.IO;
using System.Text;

namespace Tello.Net.Packet
{
	internal class EndianBinaryWriter
	{
		private readonly bool swap;
		private readonly BinaryWriter writer;

        public Stream BaseStream { get; }

        public static EndianBinaryWriter FromStream(Stream stream, bool littleEndian)
        {
            return new EndianBinaryWriter(new BinaryWriter(stream), littleEndian);
        }

        public EndianBinaryWriter() : this(new BinaryWriter(new MemoryStream()), true)
        {
        }

		public EndianBinaryWriter(BinaryWriter binaryWriter, bool littleEndian = false)
		{
            swap = !littleEndian;
            writer = binaryWriter;
            BaseStream = binaryWriter.BaseStream;
		}

		public void Write(byte value)
		{
			writer.Write(value);
		}

		public void Write(byte[] value)
		{
			writer.Write(value);
		}

		public void Write(sbyte value)
		{
			writer.Write(value);
		}

		public void Write(UInt16 value)
		{
			writer.Write(!swap ? value : EndianConverter.Swap(value));
		}

		public void Write(Int16 value)
		{
			writer.Write(!swap ? value : EndianConverter.Swap(value));
		}

		public void Write(UInt32 value)
		{
			writer.Write(!swap ? value : EndianConverter.Swap(value));
		}

		public void Write(Int32 value)
		{
			writer.Write(!swap ? value : EndianConverter.Swap(value));
		}

		public void Write(UInt64 value)
		{
			writer.Write(!swap ? value : EndianConverter.Swap(value));
		}

		public void Write(Int64 value)
		{
			writer.Write(!swap ? value : EndianConverter.Swap(value));
		}

        public void Write(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (!swap)
            {
                EndianConverter.Swap(bytes);
            }
            writer.Write(bytes);
        }

        public void Write(string value, Encoding encoding)
        {
            writer.Write(encoding.GetBytes(value));
        }
	}
}
