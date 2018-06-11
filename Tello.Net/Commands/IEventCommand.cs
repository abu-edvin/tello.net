using System;
namespace Tello.Net.Commands
{
    public interface IEventCommand
    {
        TelloCommandId Id { get; }
    }
}
