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
    private string cenaAtual;
    private GameObject[] todosObjetos;
    // Start is called before the first frame update
    void Start()
    {
        todosObjetos = UnityEngine.Object.FindObjectsOfType<GameObject>();
        cenaAtual = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame

    public void Save(List<NPCData> dadosNpcs, PlayerData dadosJogador, GameData dadosMundo) {
        Data data = new Data();
        data.npcs = dadosNpcs;
        data.jogador = dadosJogador;
        data.gameData = dadosMundo;
        //string dn = JsonUtility.ToJson(dadosNpcs);
        string dj = JsonUtility.ToJson(dadosJogador);
        string dm = JsonUtility.ToJson(dadosMundo);
        string dd = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/PlayerSave", dj);
        File.WriteAllText(Application.dataPath + "/WorldSave", dm);
        File.WriteAllText(Application.dataPath + "/SaveData", dd);
    }

    public void Load() {
        foreach (GameObject item in todosObjetos){
            if (item.GetComponent<PlayerClass>() != null) {
                transformPlayer(item);
            }
            if (item.GetComponent<NpcClass>() != null) {
                transformNpc(item);
            }
        }
        Debug.Log("Jogo carregado com sucesso");
    }

    private void transformNpc(GameObject personagem){
        NpcClass cpf = personagem.GetComponent<NpcClass>();
        NPCData dados = cpf.npcData;
        personagem.transform.position = dados.NpcPos;
        if (personagem.GetComponent<DialogueTrigger>() != null){
            personagem.GetComponent<DialogueTrigger>().messages = dados.dialog.messages;
            personagem.GetComponent<DialogueTrigger>().actors = dados.dialog.actors;
        }
    }
    private void transformPlayer(GameObject personagem) {
        PlayerClass cpf = personagem.GetComponent<PlayerClass>();
        PlayerData dados = cpf.playerData;
        personagem.transform.position = dados.PlayerPos;
    }

    public PlayerData getAtualPlayer() {
        PlayerData result = new PlayerData();
        todosObjetos = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject i in todosObjetos)
        {
            //Debug.Log(i);
            if (i.activeInHierarchy && (i.GetComponent<PlayerClass>() != null))
            {
                PlayerClass n = i.GetComponent<PlayerClass>();
                result = n.playerData;
            }
        }
        return result;
    }

    public List<NPCData> GetNpcs()
    {
        List<GameObject> lista = new List<GameObject>();
        List<NPCData> result = new List<NPCData>();
        todosObjetos = GameObject.FindObjectsOfType<GameObject>();
        
        foreach (GameObject i in todosObjetos)
        {
            //Debug.Log(i);
            if (i.activeInHierarchy && (i.GetComponent<NpcClass>()!= null)) {
                NpcClass n = i.GetComponent<NpcClass>();
                if (n.id == 1)
                {
                    Debug.Log("a");
                    NPCData kid = n.npcData;
                    Message msg = new Message(0,"aaaaaaa", true, "FalaAnnie", "FalaAnnie");
                    kid.dialog.messages[0] = msg;
                }
                result.Add(n.npcData);
            }
        }
        return result;
    }

    public GameData GetGameData()
    {
        string saveStringGame = File.ReadAllText(Application.dataPath + "/WorldSave");
        GameData loadedGameData = JsonUtility.FromJson<GameData>(saveStringGame);
        return loadedGameData;
    }

    public Data LoadAll() {

        string saveStringData = File.ReadAllText(Application.dataPath + "/SaveData");
        Data loadedSaveData = JsonUtility.FromJson<Data>(saveStringData);
        return loadedSaveData;

    }

    public NPCData GetIndividuo(int index) {
        Data data = LoadAll();
        List<NPCData> allNpcs = data.npcs;
        NPCData individuo = new NPCData();
        foreach (NPCData i in allNpcs)
        {
            if (i.Id == index)
            {
                individuo = i;
            }
        }
        return individuo;
    }
}
