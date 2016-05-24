using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
                return "nothing";
            }
        }
    }

    /// <summary>
    /// List of the current rythm
    /// The last key will be the last input triggered
    /// </summary>
    public SortedList<int ,PataponInput> CurrentRythms = new SortedList<int ,PataponInput>();

    /// <summary>
    /// The precision of an input
    /// max per tap : 3 seconds
    /// </summary>
    public float SyncKeyTime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Settings.singleton.KeyInput.Pata))
        {
            OnKeyCommand(Time.time);
            CurrentRythms.Add(CurrentRythms.Count + 1, new PataponInput { TimeStamp = Time.time, Key = Settings.singleton.KeyInput.Pata });
        }
        if (Input.GetKeyDown(Settings.singleton.KeyInput.Pon))
        {
            OnKeyCommand(Time.time);
            CurrentRythms.Add(CurrentRythms.Count + 1, new PataponInput { TimeStamp = Time.time, Key = Settings.singleton.KeyInput.Pon });
        }
        if (Input.GetKeyDown(Settings.singleton.KeyInput.Chaka))
        {
            OnKeyCommand(Time.time);
            CurrentRythms.Add(CurrentRythms.Count + 1, new PataponInput { TimeStamp = Time.time, Key = Settings.singleton.KeyInput.Chaka });
        }
        if (Input.GetKeyDown(Settings.singleton.KeyInput.Don))
        {
            OnKeyCommand(Time.time);
            CurrentRythms.Add(CurrentRythms.Count + 1, new PataponInput { TimeStamp = Time.time, Key = Settings.singleton.KeyInput.Don });
        }

        // Manage Time Precision
        SyncKeyTime += Time.deltaTime;
        if (SyncKeyTime >= 0.51F)
        {
            SyncKeyTime = 0F;
        }
    }

    public void OnKeyCommand(float time)
    {
        if (SyncKeyTime > 0.0 && 0.325 >= SyncKeyTime)
        {
            print("noob");
        }
        else if (SyncKeyTime > 0.325 && 0.420 >= SyncKeyTime)
        {
            print("mm k");
        }
        else if (SyncKeyTime > 0.420 && 0.501 > SyncKeyTime)
        {
            print("DAMN IT'S NICE");
        }
    }
}
