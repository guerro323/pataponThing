using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public interface IGameMode
    {
        void OnEvent();
        void OnChange();
        void OnKeyPressed();
        void OnPlayerJoin();
        void OnPlayerChangeTeam();
        void OnPlayerLeave();
        void OnNewMatch();
    }

    public class GameManager
    {
        protected IGameMode currGameMode;

        public GameManager gameManager;

        public class ObjectMessage : MessageBase
        {
            public string EventName;
            public object[] Params;
        }

        public void LoadGamemode(string path)
        {

        }

        public void OnEvent(string eventName, object[] oParams) {
            EventDebug(eventName, oParams);
        }

        public void SendNetworkEvent(string eventName, object[] oParams, int[] players = null)
        {
            var globalEvent = players != null;

            if (ScriptFollower.ServerManager.NetworkManager == null)
            {
                ScriptFollower.ServerManager.OnResetNetworkManager();
            }

            /*if (globalEvent)
            {
                ScriptFollower.ServerManager.SendToAll(MsgType.Highest + 1, new ObjectMessage() { EventName = eventName, Params = oParams });
            }
            else
            {
                foreach (var i in players)
                {
                    ScriptFollower.ServerManager.SendToClient(i, MsgType.Highest + 1, new ObjectMessage() { EventName = eventName, Params = oParams});
                }
            }*/
            ScriptFollower.ServerManager.SendToAll(MsgType.Highest + 1, new ObjectMessage() { EventName = eventName, Params = oParams });
        }

        public void EventDebug(string eventName, object[] oParams)
        {
            var allparams = "";
            foreach (var param in oParams) allparams += param.ToString() + "\n";
            Debug.Log(DateTime.Now + " | [" + eventName + "]\n" + allparams);
        }
    }
}
