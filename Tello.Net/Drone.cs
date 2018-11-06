using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tello.Net
{
    public class Drone
    {
        private static readonly string hostName = "192.168.1.52";
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private readonly DroneIo io;

        public Drone()
        {
            io = new DroneIo(hostName);
        }

        public void TakeOff()
        {
            log.Info("Taking off.");
            io.TakeOff();
        }

        public void Land()
        {
            log.Info("Landing.");
            io.Land();
        }
    }
}
