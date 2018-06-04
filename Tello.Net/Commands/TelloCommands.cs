using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Net.Packet;

namespace Tello.Net.Commands
{
    static class TelloCommands
    {
        private static Encoding encoding = Encoding.ASCII;

        public static byte[] ConnectionRequest(ushort videoPort)
        {
            EndianBinaryWriter writer = new EndianBinaryWriter();
            byte[] header = encoding.GetBytes("conn_req:");
            writer.Write(header);
            writer.Write(videoPort);
            return ((MemoryStream)writer.BaseStream).ToArray();
        }
    }
}
