using System;
using System.Net.Sockets;
using System.Net;


namespace DemoPacman
{
    class Client
    {
        public Socket socket;
        public bool closing = false;
    }
}
