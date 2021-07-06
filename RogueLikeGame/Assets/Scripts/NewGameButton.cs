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
        GameObject c = Instantiate(canvasPrefab);
        playerPrefab.GetComponent<PlayerClass>().theCanvas = c;
        playerPrefab.GetComponent<MovementScript>().canvas = c;
        PlayerClass pc = playerPrefab.GetComponent<PlayerClass>();
        pc.curHP = SaveGame.current.curHP;
        pc.curMana = SaveGame.current.curMana;
        pc.gold = SaveGame.current.gold;
        pc.totalkills = SaveGame.current.totalKills;
        pc.orderScenes = SaveGame.current.orderScenes;
        pc.curSceneIndex = SaveGame.current.curSceneIndex;
        GameObject p = Instantiate(playerPrefab);
        p.name = "MainChar";
        c.name = "Canvas";
        c.GetComponentsInChildren<QuitScript>()[0].player = p.GetComponent<PlayerClass>();
    }

    public void loadGame()
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
        GameObject p = Instantiate(playerPrefab);
        p.name = "MainChar";
        c.name = "Canvas";
        c.GetComponentsInChildren<QuitScript>()[0].player = p.GetComponent<PlayerClass>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
