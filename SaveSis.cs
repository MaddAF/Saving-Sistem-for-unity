using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JetBrains.Annotations;
using static PlayerData;
using static NPCData;
using static Data;
using static GameData;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.Assertions.Must;
using UnityEngine.Windows.WebCam;

public class SaveSis : MonoBehaviour
{
    private string currentScene;
    private GameObject[] allObjects;
    // Start is called before the first frame update
    void Start() //On start it gets all objects in your game and the current scene and saves it into a variable.
    {
        allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame

    public void Save(List<NPCData> npcsData, PlayerData playerData, GameData worldData) // Saves all data onto the apropriate file path
    {
        Data data = new Data();
        data.npcs = npcsData;
        data.player = playerData;
        data.gameData = worldData;
        string dn = JsonUtility.ToJson(npcsData);
        string dj = JsonUtility.ToJson(playerData);
        string dm = JsonUtility.ToJson(worldData);
        string dd = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/NpcsSave", dn);
        File.WriteAllText(Application.dataPath + "/PlayerSave", dj);
        File.WriteAllText(Application.dataPath + "/WorldSave", dm);
        File.WriteAllText(Application.dataPath + "/SaveData", dd);
        Debug.Log("Game saved successfully");
    }

    public void Load() //checks the allObjects variable for objects with either the PlayerClass or NpcClass components and transforms them based
    {                  // on the data that is saved in the save files.
        foreach (GameObject item in allObjects)
        {
            if (item.GetComponent<PlayerClass>() != null)
            {
                transformPlayer(item);
            }
            if (item.GetComponent<NpcClass>() != null)
            {
                transformNpc(item);
            }
        }
        Debug.Log("Game loaded successfully");
    }

    private void transformNpc(GameObject npcChar) // Auxiliary function that tranforms a single npc to the state saved on the npc file
    {
        NpcClass classComponent = npcChar.GetComponent<NpcClass>();
        NPCData data_ = classComponent.npcData;
        npcChar.transform.position = data_.NpcPos;
        
    }
    private void transformPlayer(GameObject playerChar)// Auxiliary function that tranforms a single player to the state saved on the player file
    {
        PlayerClass classComponent = playerChar.GetComponent<PlayerClass>();
        PlayerData data_ = classComponent.playerData;
        playerChar.transform.position = data_.PlayerPos; // do this for every changable parameter you want to load
    }

    public PlayerData getCurrentPlayer() // If you only have one player, this function can be used to get the data of it
    {                                    // Note: this only works if the player is active on the current scene, which most likely is
        PlayerData result = new PlayerData();
        allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject i in allObjects)
        {
            if (i.activeInHierarchy && (i.GetComponent<PlayerClass>() != null))
            {
                PlayerClass n = i.GetComponent<PlayerClass>();
                result = n.playerData;
            }
        }
        return result;
    }

    public List<NPCData> GetNpcs() // This is a particularly usefull function, it returns the data of all active npc. This can be useful to forcibly change
    {                              // Something about the npcs like is shown in the exemple. I often use this if i need an npc to have a different dialog
                                   // the next time a load an specific scene.
        List<GameObject> lista = new List<GameObject>();
        List<NPCData> result = new List<NPCData>();
        allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject i in allObjects)
        {
            if (i.activeInHierarchy && (i.GetComponent<NpcClass>() != null))
            {
                NpcClass n = i.GetComponent<NpcClass>();
                //exemple:
                if (n.id == 1) // Here i want to change the position of the NPC with the id of 1
                {
                    NPCData john = n.npcData;
                    Vector3 newPos = new Vector3(0.0, 1.0, 0.0);
                    john.transform.position = newPos; // this will change the position of the npc john directly on the save file, so the next time you load
                                                      // his position will be (0.0, 1.0, 0.0), note that if you want this to change only on a certain scene
                                                      // you`ll need to creat another version of this code for other scenes
                }
                result.Add(n.npcData);
            }
        }
        return result;
    }

    public GameData GetGameData() // Returns the saved World data
    {
        string saveStringGame = File.ReadAllText(Application.dataPath + "/WorldSave");
        GameData loadedGameData = JsonUtility.FromJson<GameData>(saveStringGame);
        return loadedGameData;
    }

    public Data LoadAll() // Returns every data saved as an instance of the Data class
    {
        string saveStringData = File.ReadAllText(Application.dataPath + "/SaveData");
        Data loadedSaveData = JsonUtility.FromJson<Data>(saveStringData);
        return loadedSaveData;

    }

    public NPCData GetIndividual(int index) // Gets the NPCData of an npc with a specific id
    {
        Data data = LoadAll();
        List<NPCData> allNpcs = data.npcs;
        NPCData individual = new NPCData();
        foreach (NPCData i in allNpcs)
        {
            if (i.Id == index)
            {
                individual = i;
            }
        }
        return individual;
    }
}
