using Tello.Net.Packet;

namespace Tello.Net.Commands
{
    public class TelloCommand
    {
        public byte Type { get; }

        public TelloCommandId Id { get;  }

        public ushort SeqId { get; }
        
        public byte[] Data { get;  }

        public TelloCommand(byte type, TelloCommandId id) :
            this(type, id, new byte[0])
        {
        }

        public TelloCommand(byte type, TelloCommandId id, byte[] data) :
            this(type, id, 0, data)
        {
        }

        public TelloCommand(byte type, TelloCommandId id, ushort seqId, byte[] data)
        {
            Type = type;
            Id = id;
            SeqId = seqId;
            Data = data;
        }

        public EndianBinaryReader CreateDataReader()
        {
            return new EndianBinaryReader(Data);
        }
    }
}
