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
        Destroy(myPlayer);
        GameObject c = Instantiate(canvasPrefab);
        playerPrefab.GetComponent<PlayerClass>().theCanvas = c;
        playerPrefab.GetComponent<MovementScript>().canvas = c;
        PlayerClass pc = playerPrefab.GetComponent<PlayerClass>();
        pc.gold = 0;
        pc.totalkills = 0;
        pc.curSceneIndex = 1;
        GameObject p = Instantiate(playerPrefab);
        p.name = "MainChar";
        c.name = "Canvasbb";
        c.GetComponentsInChildren<QuitScript>()[0].player = p.GetComponent<PlayerClass>();
        c.GetComponentsInChildren<RestartScript>()[0].myPlayer = p;
        c.GetComponentsInChildren<KillCounter>()[0].timeSpent = 0f;
        Destroy(this.transform.parent.parent.gameObject);
    }
}
