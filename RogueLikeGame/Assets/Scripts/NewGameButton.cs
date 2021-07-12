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
        SaveGame.current = new SaveGame();
        if (File.Exists(Application.persistentDataPath + "/savedGame.dt"))
        {
            File.Delete(Application.persistentDataPath + "/savedGame.dt");
        }
        if (File.Exists(Application.persistentDataPath + "/PermVar.dt"))
        {
            File.Delete(Application.persistentDataPath + "/PermVar.dt");
        }
        PermVar.current = new PermVar();
        GameObject c = Instantiate(canvasPrefab);
        playerPrefab.GetComponent<PlayerClass>().theCanvas = c;
        playerPrefab.GetComponent<MovementScript>().canvas = c;
        PlayerClass pc = playerPrefab.GetComponent<PlayerClass>();
        pc.curHP = SaveGame.current.curHP;
        pc.curMana = SaveGame.current.curMana;
        pc.gold = SaveGame.current.gold;
        pc.totalkills = SaveGame.current.totalKills;
        pc.curSceneIndex = SaveGame.current.curSceneIndex;
        pc.curShade = SaveGame.current.curShade;
        GameObject p = Instantiate(playerPrefab);
        pc.orderScenes = SaveGame.current.orderScenes;
        p.name = "MainChar";
        c.name = "Canvas";
        c.GetComponentsInChildren<QuitScript>()[0].player = p.GetComponent<PlayerClass>();
        c.GetComponentsInChildren<RestartScript>()[0].myPlayer = p;
    }

    public void loadGame()
    {
        if(File.Exists(Application.persistentDataPath + "/savedGame.dt"))
        {
            SaveLoad.Load();
            GameObject c = Instantiate(canvasPrefab);
            playerPrefab.GetComponent<PlayerClass>().theCanvas = c;
            playerPrefab.GetComponent<MovementScript>().canvas = c;
            PlayerClass pc = playerPrefab.GetComponent<PlayerClass>();
            c.GetComponentsInChildren<KillCounter>()[0].timeSpent = SaveGame.current.time;
            c.transform.GetChild(4).GetComponent<Text>().text = "Gold: " + SaveGame.current.gold;
            pc.curHP = SaveGame.current.curHP;
            pc.curMana = SaveGame.current.curMana;
            pc.gold = SaveGame.current.gold;
            pc.totalkills = SaveGame.current.totalKills;
            pc.orderScenes = SaveGame.current.orderScenes;
            pc.curSceneIndex = SaveGame.current.curSceneIndex;
            pc.curShade = SaveGame.current.curShade;
            GameObject p = Instantiate(playerPrefab);
            p.name = "MainChar";
            c.name = "Canvas";
            c.GetComponentsInChildren<QuitScript>()[0].player = p.GetComponent<PlayerClass>();
            c.GetComponentsInChildren<ShadeScript>()[0].updateShade();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
