using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

using CB = Assets.Scripts.Helper.Callbacks;

/// <summary>
/// Base Class for Player
/// </summary>
public class EngineBase : MonoBehaviour
{

    public class PataponInput
    {
        public double TimeStamp;
        public KeyCode Key;
        public int KeyInt
        {
            get
            {
                if (Key == Settings.singleton.KeyInput.Pata) return 1;
                if (Key == Settings.singleton.KeyInput.Pon) return 2;
                if (Key == Settings.singleton.KeyInput.Chaka) return 3;
                if (Key == Settings.singleton.KeyInput.Don) return 4;
                return 0;
            }
        }
        public string KeyString
        {
            get
            {
                if (Key == Settings.singleton.KeyInput.Pata) return "Pata";
                if (Key == Settings.singleton.KeyInput.Pon) return "Pon";
                if (Key == Settings.singleton.KeyInput.Chaka) return "Chaka";
                if (Key == Settings.singleton.KeyInput.Don) return "Don";
                return "Nothing";
            }
        }

        public static string GetString(int key)
        {
            var input = new PataponInput();
            if (key == 1)
                input.Key = Settings.singleton.KeyInput.Pata;
            if (key == 2)
                input.Key = Settings.singleton.KeyInput.Pon;
            if (key == 3)
                input.Key = Settings.singleton.KeyInput.Chaka;
            if (key == 4)
                input.Key = Settings.singleton.KeyInput.Don;

            return input.KeyString;
        }
    }

    /// <summary>
    /// List of the current rythm
    /// The last key will be the last input triggered
    /// </summary>
    public Dictionary<int, PataponInput> CurrentRythms = new Dictionary<int, PataponInput>();
    public RythmCommandManager CommandManager = new RythmCommandManager();

    public Image test;

    /// <summary>
    /// The precision of an input
    /// </summary>
    public float SyncKeyTime;
    private float _key;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Settings.singleton.KeyInput.Pata))
        {
            CurrentRythms[CurrentRythms.Count] = new PataponInput { TimeStamp = Time.time, Key = Settings.singleton.KeyInput.Pata };
            OnKeyCommand(Time.time);
        }
        if (Input.GetKeyDown(Settings.singleton.KeyInput.Pon))
        {
            CurrentRythms[CurrentRythms.Count] = new PataponInput { TimeStamp = Time.time, Key = Settings.singleton.KeyInput.Pon };
            OnKeyCommand(Time.time);
        }
        if (Input.GetKeyDown(Settings.singleton.KeyInput.Chaka))
        {
            CurrentRythms[CurrentRythms.Count] = new PataponInput { TimeStamp = Time.time, Key = Settings.singleton.KeyInput.Chaka };
            OnKeyCommand(Time.time);
        }
        if (Input.GetKeyDown(Settings.singleton.KeyInput.Don))
        {
            CurrentRythms[CurrentRythms.Count] = new PataponInput { TimeStamp = Time.time, Key = Settings.singleton.KeyInput.Don };
            OnKeyCommand(Time.time);
        }

        // Manage Time Precision
        SyncKeyTime = (Environment.TickCount - _key) / 1000;
        if (Environment.TickCount - _key >= 500)
        {
            _key = Environment.TickCount;
        }

        /*print(string.Concat(
            CurrentRythms.ContainsKey(0) ? CurrentRythms[0].KeyString : "Nothing",
            CurrentRythms.ContainsKey(1) ? CurrentRythms[1].KeyString : "Nothing",
            CurrentRythms.ContainsKey(2) ? CurrentRythms[2].KeyString : "Nothing",
            CurrentRythms.ContainsKey(3) ? CurrentRythms[3].KeyString : "Nothing"));*/


        var newcolor = test.color;
        newcolor.a = SyncKeyTime / 0.5F;
        test.color = newcolor;
    }

    public void OnKeyCommand(float time)
    {
        var rank = 0;
        var key = CurrentRythms.Last();
        if (SyncKeyTime >= 0.0 && 0.225 >= SyncKeyTime)
        {
            // Score bof
            rank = 0;
        }
        else if (SyncKeyTime > 0.225 && 0.320 >= SyncKeyTime)
        {
            // Bon score
            rank = 1;
        }
        else if (SyncKeyTime > 0.320 && 0.500 >= SyncKeyTime)
        {
            // Très bon score
            rank = 2;
        }
        //print(key.Value.KeyString + "_" + rank);
        ScriptFollower.GetMainPlayer().AudioSource.PlayOneShot(ScriptFollower.AudioBank.Audios[key.Value.KeyString + "_" + rank]);

        VerifyCommand();

        if (CurrentRythms.Count >= 4)
        {
            CurrentRythms.Remove(0);
            CurrentRythms = CurrentRythms.OrderBy(t => t.Value.TimeStamp).ToDictionary(o => o.Key - 1, o => o.Value);
        }
    }

    public void VerifyCommand()
    {
        var match = CommandManager.Verify(CurrentRythms);
        //print(match.Exist + "<");
        if (match.Exist)
        {
            //print(match.FoundString);
            var concated = string.Concat(
            match.FoundString[0],
            match.FoundString[1],
            match.FoundString[2],
            match.FoundString[3]);

            ScriptFollower.GameManager.OnEvent(CB.VerifyCommand.Success, new object[2] { ScriptFollower.GetMainPlayer(), match.FoundString });
            foreach (var localPlayer in ScriptFollower.LocalEntities.Where(localPlayer => localPlayer.Type == Entity.EntityType.Player))
            {
                localPlayer.OnEvent(CB.VerifyCommand.Success, new object[2] { concated, CurrentRythms });
            }
        }
    }
}
