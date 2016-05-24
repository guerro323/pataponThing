using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Settings : MonoBehaviour
{
    public static Settings singleton;

    public SKeyInput KeyInput;

    public struct SKeyInput
    {
        public KeyCode Pata;
        public KeyCode Pon;
        public KeyCode Chaka;
        public KeyCode Don;
    }

    public void Load(string settingName)
    {
        switch (settingName)
        {
            case "KeyInput":
                // Ici, il faudra load le json
                Reset(settingName);
                break;
        }
    }

    void Start()
    {
        Reset("KeyInput");
    }

    void Awake()
    {
        if (singleton != null)
            GameObject.Destroy(singleton);
        else
            singleton = this;

        DontDestroyOnLoad(this);
    }

    public void Reset(string settingName)
    {
        switch (settingName)
        {
            case "KeyInput":
                KeyInput = new SKeyInput
                {
                    Pata = KeyCode.LeftArrow,
                    Pon = KeyCode.RightArrow,
                    Chaka = KeyCode.UpArrow,
                    Don = KeyCode.DownArrow
                };
                break;
        }
    }
}
