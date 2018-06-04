using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tello.Net;

namespace TelloTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Drone drone = new Drone();
            for (; ;)
            {
                Thread.Sleep(100);
            }
        }
    }
}
