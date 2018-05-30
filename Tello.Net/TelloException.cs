using System;

namespace Tello.Net
{
    public class TelloException : Exception
    {
        public TelloException(string message) : base(message) { }
    }
}
