using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tello.Net.Commands;
using Tello.Net.Packet;

namespace Tello.Net
{
    public class DroneIo
    {
        private const ushort CommandPort = 8889;
        private const ushort VideoPort = 6037;

        private static readonly Encoding encoding = Encoding.ASCII;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private static readonly CommandSerializer serializer = new CommandSerializer();
        private static bool isActive;

        private readonly Thread cmdThread = new Thread(CommandThreadMain) { IsBackground = true };
        private readonly Thread videoThread = new Thread(VideoThreadMain) { IsBackground = true };
        private readonly Thread eventThread = new Thread(EventThreadMain) { IsBackground = true };
        private readonly BlockingCollection<TelloCommand> commandQueue = new BlockingCollection<TelloCommand>();
        private readonly UdpClient cmdClient;
        private readonly UdpClient videoClient;
        private readonly object sendLock = new object();

        public event EventHandler<TelloCommand> CommandReceived;

        private interface IBaseState
        {
            UdpClient Client { get; set; }
        }

        private struct CommandThreadState : IBaseState
        {
            public UdpClient Client { get; set; }
            public BlockingCollection<TelloCommand> Queue { get; set; }
        }

        private struct EventThreadState : IBaseState
        {
            public UdpClient Client { get; set; }
            public BlockingCollection<TelloCommand> Queue { get; set; }
            public Action<TelloCommand> Handler { get; set; }
        }

        private struct VideoThreadState : IBaseState
        {
            public UdpClient Client { get; set; }
        }

        public DroneIo(string hostName)
        {
            cmdClient = new UdpClient(hostName, CommandPort);
            videoClient = new UdpClient(hostName, VideoPort);
            CommandThreadState cmdState = new CommandThreadState() {
                Client = cmdClient, Queue = commandQueue };
            EventThreadState eventState = new EventThreadState() {
                Client = cmdClient, Queue = commandQueue, Handler = HandleCommandEvent };
            VideoThreadState videoState = new VideoThreadState() {
                Client = cmdClient };
            cmdThread.Start(cmdState);
            videoThread.Start(videoState);
            eventThread.Start(eventState);
            RequestConnection();
        }

        public void SendTextCommand(byte[] command)
        {
            lock (sendLock)
            {
                int result = cmdClient.Send(command, command.Length);
                int x = 5;
            }
        }

        private void RequestConnection()
        {
            byte[] data = TelloCommands.ConnectionRequest(VideoPort);
            SendTextCommand(data);
        }

        private static void CommandThreadMain(object state)
        {
            CommandThreadState cmdState = (CommandThreadState)state;
            Action<byte[]> handler = delegate(byte[] data)
            {
                HandleCommandFrame(data, cmdState.Queue);
            };
            CommonThreadMain(state, handler);
        }

        private static void VideoThreadMain(object state)
        {
            CommonThreadMain(state, HandleVideoFrame);
        }

        private static void EventThreadMain(object state)
        {
            EventThreadState eventState = (EventThreadState)state;
            BlockingCollection<TelloCommand> queue = eventState.Queue;
            isActive = true;
            while (isActive)
            {
                TelloCommand command = null;
                try { command = queue.Take(); }
                catch (InvalidOperationException) { Thread.Sleep(100); }

                if (command == null) { continue; }
            }
        }

        private static void HandleCommandFrame(byte[] frame, BlockingCollection<TelloCommand> queue)
        {
            byte[] subFrame = new byte[frame.Length - 1];
            Array.Copy(frame, 1, subFrame, 0, subFrame.Length);
            if (frame[0] == 0xcc)
            {
                HandleBinaryCommand(frame, queue);
                return;
            }
            HandleTextCommand(frame);
        }

        private static void HandleVideoFrame(byte[] frame)
        {
        }

        private static void CommonThreadMain(object state, Action<byte[]> frameHandler)
        {
            object receiveLock = new object();
            UdpClient client = ((IBaseState)state).Client;
            isActive = true;
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            while (isActive)
            {
                byte[] rxData;
                lock (receiveLock)
                {
                    rxData = client.Receive(ref ep);
                }
                frameHandler(rxData);
            }
            client.Close();
        }

        private static void HandleBinaryCommand(byte[] data, BlockingCollection<TelloCommand> queue)
        {
            TelloCommand command = serializer.Read(data);
            queue.Add(command);
        }
        
        private void HandleCommandEvent(TelloCommand command)
        {
            CommandReceived?.Invoke(this, command);
        }

        private static void HandleTextCommand(byte[] data)
        {
            string text = encoding.GetString(data);
            int x = 5;
        }
    }
}
