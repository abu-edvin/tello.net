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
        private static readonly string hostName = "192.168.10.1";
        private readonly DroneIo io;

        public Drone()
        {
            io = new DroneIo(hostName);
        }
    }
}
