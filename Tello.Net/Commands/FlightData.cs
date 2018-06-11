using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Net.Packet;

namespace Tello.Net.Commands
{
    public class FlightData : IEventCommand
    {
        public TelloCommandId Id => TelloCommandId.Status;

        public bool BatteryLow { get; private set; }

        public bool BatteryLower { get; private set; }

        public byte BatteryPercentage { get; private set; }

        public bool BatteryState { get; private set; }

        public byte CameraState { get; private set; }

        public bool DownVisualState { get; private set; }

        public ushort DroneBatteryLeft { get; private set; }

        public ushort DroneFlyTimeLeft { get; private set; }

        public bool DroneHover { get; private set; }

        public bool EmOpen { get; private set; }

        public bool EmSky { get; private set; }

        public bool EmGround { get; private set; }

        public short EastSpeed { get; private set; }

        public ushort ElectricalMachineryState { get; private set; }

        public bool FactoryMode { get; private set; }

        public byte FlyMode { get; private set; }

        public short FlySpeed { get; private set; }

        public ushort FlyTime { get; private set; }

        public bool FrontIn { get; private set; }

        public bool FrontLsc { get; private set; }

        public bool FrontOut { get; private set; }

        public bool GravityState { get; private set; }

        public short GroundSpeed { get; private set; }

        public short Height { get; private set; }

        public byte ImuCalibrationSet { get; private set; }

        public bool ImuState { get; private set; }

        public byte LightStrength { get; private set; }

        public short SmartVideoExitMode { get; private set; }

        public bool TemperatureHeight { get; private set; }

        public byte ThrowFlyTimer { get; private set; }

        public byte WifiDisturb { get; private set; }

        public byte WifiStrength { get; private set; }

        public bool WindState { get; private set; }
    }

    public class StatusReader : ICommandReader
    {
        public TelloCommandId Id => TelloCommandId.Status;

        public IEventCommand Read(TelloCommand command)
        {
            EndianBinaryReader reader = command.CreateDataReader();
            return null;
        }
    }
}
