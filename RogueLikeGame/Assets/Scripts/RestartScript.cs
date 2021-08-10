using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    public GameObject myPlayer;
    public GameObject canvasPrefab;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(doClick);
    }

    public void doClick()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        myPlayer.GetComponent<PlayerClass>().sm.sheithe();
        Destroy(myPlayer);
        GameObject c = Instantiate(canvasPrefab);
        GameObject p = Instantiate(playerPrefab);
        p.GetComponent<PlayerClass>().theCanvas = c;
        p.GetComponent<MovementScript>().canvas = c;
        SkillTreeScript.sts.resetChoices();
        PlayerClass pc = p.GetComponent<PlayerClass>();
        pc.gold = 0;
        pc.totalkills = 0;
        pc.curSceneIndex = 1;
        pc.maxHP = 100;
        pc.maxMana = 100;
        pc.curHP = 0;
        pc.spells = new int[3];
        GameObject go = Instantiate(p);
        Destroy(p);
        go.name = "MainChar";
        c.name = "Canvas";
        c.GetComponentsInChildren<QuitScript>()[0].player = go.GetComponent<PlayerClass>();
        c.GetComponentsInChildren<RestartScript>()[0].myPlayer = go;
        c.GetComponentsInChildren<KillCounter>()[0].timeSpent = 0f;
        Destroy(this.transform.parent.parent.gameObject);
    }
}
