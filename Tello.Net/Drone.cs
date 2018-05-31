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
        private readonly Thread cmdThread = new Thread(CommandThreadMain);
        private readonly Thread videoThread = new Thread(VideoThreadMain);
        private readonly UdpClient cmdClient;
        private readonly UdpClient videoClient;

        public Drone()
        {

        }

        private static void CommandThreadMain()
        {

        }


        private static void VideoThreadMain()
        {

        }
    }
}
