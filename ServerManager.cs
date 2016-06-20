using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class ServerManager : NetworkBehaviour
    {
        bool lastConnection = true;
        public NetworkManager NetworkManager;
        public NetworkClient client;

        public override void OnStartClient()
        {
            base.OnStartClient();
        }

        public void OnConnection()
        {
            //NetworkConnection.RegisterHandler(MsgType.Highest + 1, OnCallback);
        }

        void Start()
        {
            ScriptFollower.ServerManager = this;
        }

        public void OnResetNetworkManager()
        {
            NetworkManager = GameObject.FindObjectOfType<NetworkManager>();
            client = NetworkManager.client;
            if (client.isConnected)
            {
                client.RegisterHandler(MsgType.Highest + 1, OnCallback);
            }
            NetworkServer.RegisterHandler(MsgType.Highest + 1, OnCallback);
        }

        public bool SendToAll(short msgType, MessageBase msg)
        {
            if (isServer)
            {
                return NetworkServer.SendToAll(msgType, msg);
            }
            if (!client.isConnected && !isServer) return false;
            return client.Send(msgType, msg);

        }

        public void SendToClient(int connectionId, short msgType, MessageBase msg)
        {
            NetworkServer.SendToClient(connectionId, msgType, msg);
        }

        public void OnCallback(NetworkMessage netMsg)
        {
            print("callback");
            GameManager.ObjectMessage msg = netMsg.ReadMessage<GameManager.ObjectMessage>();
            var allparams = "";
            foreach (var param in msg.Params) allparams += param.ToString() + "\n";
            Debug.Log("NETWORK (" + msg.EventName + ")\n" + allparams);
        }
    }
}
