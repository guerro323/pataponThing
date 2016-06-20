using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RythmCommandManager
{
    public class CommandManager
    {
        public int[] Command;
        public string Name;

        public CommandManager(int[] Command, string Name)
        {
            this.Command = Command;
            this.Name = Name;
        }
    }

    public static CommandManager[] Commands
    {
        get
        {
            return new CommandManager[]
            {
                new CommandManager(new int[4] { 1, 1, 1, 2 }, "PataPata"),
                new CommandManager(new int[4] { 2, 2, 1, 2 }, "PonPata"),
                new CommandManager(new int[4] { 2, 2, 3, 3 }, "PonChaka"),
                new CommandManager(new int[4] { 3, 3, 1, 2 }, "ChakaChaka")
            };
        }
    }

    public bool Exist;
    public int[] Found;
    public string[] FoundString
    {
        get
        {
            string[] found = new string[]
            {
                EngineBase.PataponInput.GetString(Found[0]),
                EngineBase.PataponInput.GetString(Found[1]),
                EngineBase.PataponInput.GetString(Found[2]),
                EngineBase.PataponInput.GetString(Found[3])
            };
            return found;
        }
    }

    public RythmCommandManager Verify(Dictionary<int, EngineBase.PataponInput> keys)
    {
        var toReturn = new RythmCommandManager();
        toReturn.Exist = false;
        //Debug.Log(keys.Count);
        if (keys.Count == 4)
        {
            foreach (var command in Commands)
            {
                var commandArray = keys.Values.Select(k => k.KeyInt).ToArray();
                if (commandArray.SequenceEqual(command.Command))
                {
                    toReturn.Exist = true;
                    toReturn.Found = command.Command;
                }
            }
        }

        return toReturn;
    }
}
