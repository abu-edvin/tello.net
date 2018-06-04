namespace Tello.Net.Commands
{
    public class TelloCommand
    {
        public byte Type { get; }

        public TelloCommandId Id { get;  }

        public ushort SeqId { get; }
        
        public byte[] Data { get;  }

        public TelloCommand(byte type, TelloCommandId id, ushort seqId, byte[] data)
        {
            Type = type;
            Id = id;
            SeqId = seqId;
            Data = data;
        }
    }
}
