//Object of this class will hold the data
//And then this object will be converted to JSON
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Pipes;

[System.Serializable]

public class NPCData { //Add the npc parameters you want to save
    // Don`t remove these-----
    public int Id;
    public GameObject objeto;
    public Vector3 NpcPos;
    public string currentSceneNPC;
    //----------------------------
    //Add more parameters here:
    
    //exemple:
    // public int Health;
}

public class PlayerData {
    // Don`t remove these-----
    public GameObject playerObject;
    public Vector3 PlayerPos;
    public string currentScene;
    private GameObject x;
    private void Awake(){
        x = GameObject.Find("NAME OF YOUR PLAYER GAME OBJECT HERE"); //change this
    }
    public PlayerData() { 
        this.playerObject = x;
        this.PlayerPos = new Vector3((float)1.5, (float)-20.83);
        this.currentScene = "NAME OF THE STARTING SCENE HERE";// note that the scene must be in build for this to work
    }
    //----------------------------
        //----------------------------
    //Add more parameters here:
    
    //exemple:
    // public int Ammo;
}

[System.Serializable] public class GameData{
    //Add the world parameters you want to save here:
    
    //exemple:
    // public int completed_achievements;
}

[System.Serializable] public class Data {
    // If you want to add extra classes you want to save separately, create the classes like the others and add them here:
    public List<NPCData> npcs;
    public PlayerData jogador;
    public GameData gameData;
}
