using System;

namespace Tello.Net.Packet
{
	internal class EndianConverter
	{
		public static UInt16 Swap(UInt16 value)
		{
			return (UInt16)((value >> 8) | (value << 8));
		}

		public static Int16 Swap(Int16 value)
		{
			return (Int16)Swap((UInt16)value);
		}

		public static UInt32 Swap(UInt32 value)
		{
			return (value >> 24) | ((value & 0x00FF0000) >> 8) | ((value & 0x0000FF00) << 8) | (value << 24);
		}

		public static Int32 Swap(Int32 value)
		{
			return (Int32)Swap((UInt32)value);
		}

		public static UInt64 Swap(UInt64 value)
		{
			return (value >> 56) | ((value & 0x00FF000000000000) >> 40) | ((value & 0x0000FF0000000000) >> 24) | ((value & 0x000000FF00000000) >> 8) |
			       ((value & 0x00000000FF000000) << 8) | ((value & 0x0000000000FF0000) << 24) | ((value & 0x0000000000FF0000) << 40) | (value << 56);
		}

		public static Int64 Swap(Int64 value)
		{
			return (Int64)Swap((UInt64)value);
        }

        public static void Swap(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length / 2; i++)
            {
                int mirrorIndex = bytes.Length - 1 - i;
                byte tmp = bytes[i];
                bytes[i] = bytes[mirrorIndex];
                bytes[mirrorIndex] = tmp;
            }
        }
    }
}
