using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class NewGameButton : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject canvasPrefab;
    public GameObject ContinueButton;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(createNewGame);
        ContinueButton.GetComponent<Button>().onClick.AddListener(loadGame);
        
    }

    public void createNewGame()
    {
        PermVar.current = new PermVar();
        SaveGame.current = new SaveGame();
        if (File.Exists(Application.persistentDataPath + "/savedGame.dt"))
        {
            File.Delete(Application.persistentDataPath + "/savedGame.dt");
        }
        if (File.Exists(Application.persistentDataPath + "/PermVar.dt"))
        {
            File.Delete(Application.persistentDataPath + "/PermVar.dt");
        }
        GameObject go = Instantiate(playerPrefab);
        GameObject c = Instantiate(canvasPrefab);
        go.GetComponent<PlayerClass>().theCanvas = c;
        go.GetComponent<MovementScript>().canvas = c;
        PlayerClass pc = go.GetComponent<PlayerClass>();
        pc.curHP = SaveGame.current.curHP;
        pc.curMana = SaveGame.current.curMana;
        pc.gold = SaveGame.current.gold;
        pc.totalkills = SaveGame.current.totalKills;
        pc.curSceneIndex = SaveGame.current.curSceneIndex;
        pc.curShade = SaveGame.current.curShade;
        GameObject p = Instantiate(go);
        Debug.Log(p == go);
        playerPrefab = go;
        Destroy(go);
        p.GetComponent<PlayerClass>().orderScenes = SaveGame.current.orderScenes;
        p.name = "MainChar";
        c.name = "Canvas";
        c.GetComponentsInChildren<QuitScript>()[0].player = p.GetComponent<PlayerClass>();
        c.GetComponentsInChildren<RestartScript>()[0].myPlayer = p;
    }

    public void loadGame()
    {
        if(File.Exists(Application.persistentDataPath + "/savedGame.dt") || File.Exists(Application.persistentDataPath + "/PermVar.dt"))
        {
            SaveLoad.Load();
            GameObject go = Instantiate(playerPrefab);
            GameObject c = Instantiate(canvasPrefab);
            go.GetComponent<PlayerClass>().theCanvas = c;
            go.GetComponent<MovementScript>().canvas = c;
            PlayerClass pc = go.GetComponent<PlayerClass>();
            c.GetComponentsInChildren<KillCounter>()[0].timeSpent = SaveGame.current.time;
            c.transform.Find("Gold").GetComponent<Text>().text = "Gold: " + SaveGame.current.gold;
            pc.curHP = SaveGame.current.curHP;
            pc.maxHP = 100 + PermVar.current.healthBuff;
            pc.curMana = SaveGame.current.curMana;
            pc.maxMana = 100 + PermVar.current.manaBuff;
            pc.gold = SaveGame.current.gold;
            pc.totalkills = SaveGame.current.totalKills;
            pc.orderScenes = SaveGame.current.orderScenes;
            pc.curSceneIndex = SaveGame.current.curSceneIndex;
            pc.curShade = SaveGame.current.curShade;
            GameObject p = Instantiate(go);
            Debug.Log(p == go);
            Destroy(go);
            p.name = "MainChar";
            c.name = "Canvas";
            c.GetComponentsInChildren<QuitScript>()[0].player = p.GetComponent<PlayerClass>();
            c.GetComponentsInChildren<RestartScript>()[0].myPlayer = p;
            c.GetComponentsInChildren<ShadeScript>()[0].updateShade();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
