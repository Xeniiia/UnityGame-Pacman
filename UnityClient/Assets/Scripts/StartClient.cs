using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Net;

namespace ClientNamespace
{

    public class StartClient : MonoBehaviour
    {
        public void Awake()
        {
            Client client = new Client();
            client.StartClient();

        }
    }
}
