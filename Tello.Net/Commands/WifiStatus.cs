using System;
using Tello.Net.Packet;

namespace Tello.Net.Commands
{
    internal class WifiStatus : IEventCommand
    {
        public TelloCommandId Id { get => TelloCommandId.WifiSignal; }

        public byte Disturb { get; private set; }

        public byte Strength { get; private set; }

        public class Reader : ICommandReader
        {
            public TelloCommandId Id => TelloCommandId.WifiSignal;

            public IEventCommand Read(TelloCommand command)
            {
                EndianBinaryReader reader = command.CreateDataReader();

                byte disturb = reader.ReadByte();
                byte strength = reader.ReadByte();
                return new WifiStatus(disturb, strength);
            }
        }

        public WifiStatus(byte disturb, byte strength)
        {
            Disturb = disturb;
            Strength = strength;
        }
    }
}
