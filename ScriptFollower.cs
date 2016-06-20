using UnityEngine;
using System.Collections;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Helper.Enums;
using System.Collections.Generic;
using Assets.Scripts.Helper;

public class ScriptFollower : MonoBehaviour {

    static public GameManager GameManager;
    static public ServerManager ServerManager;
    static public AudioBank AudioBank = new AudioBank();
    static public AudioSource AudioSource;

    static public Entity[] LocalEntities
    {
        get
        {
            var tempList = new List<Entity>();
            foreach (Entity entity in GameObject.FindObjectsOfType<Entity>().Where(entity => entity.NetworkImportance == NetworkImportance.Local))
            {
                tempList.Add(entity);
            }
            return tempList.ToArray();
        }
    }

    /// <summary>
    /// Will return the main player ( = hero )
    /// </summary>
    /// <returns>The main Player</returns>
    static public Player GetMainPlayer()
    {
        foreach (Entity entity in LocalEntities.Where(entity => entity.Type == Entity.EntityType.Player
                                                             && entity.GetType() == typeof(Player)))
        {
            return (Player)entity;
        }
        return null;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
