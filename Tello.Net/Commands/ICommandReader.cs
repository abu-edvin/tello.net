using Tello.Net.Packet;

namespace Tello.Net.Commands
{
    internal interface ICommandReader
    {
        TelloCommandId Id { get;  }

        IEventCommand Read(TelloCommand command);
    }
}
