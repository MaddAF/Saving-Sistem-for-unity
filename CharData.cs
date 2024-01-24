//Object of this class will hold the data
//And then this object will be converted to JSON
using static DialogueTrigger;
using static DialogueManager;
using static Npc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Pipes;

[System.Serializable]

public class NPCData {
    public int Id;
    public GameObject objeto;
    public string Name;
    public DialogueTrigger dialog;
    public DialogueManager dialogueManager;
    public Vector3 NpcPos;
    public Sprite NpcSprite;
    public Npc dialogueBallon;
    public string currentSceneNPC;
}

public class PlayerData {
    public GameObject playerObject;
    public Vector3 PlayerPos;
    public Sprite PlayerSprite;
    public string currentScene;
    private GameObject x;
    private Sprite y;
    private void Awake()
    {
        x = GameObject.Find("Pocket Annie");
        y = Resources.Load<Sprite>("Assets/Scripts/Dialogo/Sprites Dialogues/pocket annie v2-Front.png");
    }
    public PlayerData() { 
        this.playerObject = x;
        this.PlayerPos = new Vector3((float)1.5, (float)-20.83);
        this.PlayerSprite = y;
        this.currentScene = "NavioMain";
    }
}

[System.Serializable] public class GameData{
    public int desafios_completados;
    public bool isAnnie;
}

[System.Serializable] public class Data {
    public List<NPCData> npcs;
    public PlayerData jogador;
    public GameData gameData;
}