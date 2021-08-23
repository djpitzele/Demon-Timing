using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitScript : MonoBehaviour
{
    public PlayerClass player;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerClass.main;
        GetComponent<Button>().onClick.AddListener(doClick);
        transform.Find("Save").GetComponent<Button>().onClick.AddListener(doClickSave);
        transform.Find("Do Not Save").GetComponent<Button>().onClick.AddListener(doClickDoNotSave);
    }

    public void doClick()
    {
        foreach(Image i in GetComponentsInChildren<Image>())
        {
            i.enabled = true;
        }
        foreach (Text i in GetComponentsInChildren<Text>())
        {
            i.enabled = true;
        }
        foreach (Button i in GetComponentsInChildren<Button>())
        {
            i.enabled = true;
        }
        GetComponent<Image>().enabled = false;
        GetComponent<Button>().enabled = false;
        this.transform.Find("Text").GetComponent<Text>().enabled = false;
    }

    public void doClickSave()
    {
        SaveGame.current = player.makeSaveGame();
        SaveLoad.Save();
        Application.Quit();
    }

    public void doClickDoNotSave()
    {
        SaveGame.current = new SaveGame();
        SaveLoad.Save();
        //Debug.Log("stop the game");
        Application.Quit();
    }
}
