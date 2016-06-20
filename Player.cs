using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Helper;

using CB = Assets.Scripts.Helper.Callbacks;
using UnityEngine.Networking;

public class Player : Entity {

    public float WalkTime;
    public float AttackTime;

    public RythmCommandManager CurrentCommand;

    // Use this for initialization
    public void Start () {
        // DONT WRITE ANYTHING HERE
        if (!isLocalPlayer)
            return;
        // ------------------------ //
        // PLAYER CLASS
        //<
        //print(NetId + " diff " + netId);

        Type = EntityType.Player;

        ScriptFollower.GameManager.OnEvent(CB.Player.Join, new object[1] { NetId });            //< SOLO EVENT
        ScriptFollower.GameManager.SendNetworkEvent(CB.Player.Join, new object[1] { NetId });   //< MULTIPLAYER EVENT

        var test = new DamageInfo
        {
            CauserId = NetId,
            VictimId = NetId
        };
        OnEvent(CB.Collision.Hit, new object[] { Login, Login, JsonUtility.ToJson(test) }); // DEBUG 
    }

	void FixedUpdate () {
	    if (!isLocalPlayer)
            return;
        //print(CurrentCommand.FoundString + " < player");
	}

    public override void OnEvent(string eventName, object[] oParams)
    {
        ScriptFollower.GameManager.EventDebug(eventName, oParams);
        ScriptFollower.GameManager.SendNetworkEvent(eventName, oParams);
        if (eventName == "Collision.Hit" && oParams.Length > 0
            && (int)oParams[0] == NetId)
        {
            var hitInfo = EntityConverter.FromDamageInfo(JsonUtility.FromJson<DamageInfo>(oParams[2].ToString()));
            //print(hitInfo.CauserLogin);
        }
        if (eventName == "VerifyCommand.Success" && oParams.Length > 0)
        {
            switch((string)oParams[0])
            {
                case "PataPataPataPon":
                    WalkTime += 4;
                    CurrentCommand = new RythmCommandManager().Verify((Dictionary < int, EngineBase.PataponInput > )oParams[1]);
                    break;
                case "PonPonPataPon":
                    // TODO
                    break;
            }
        }
    }
}
