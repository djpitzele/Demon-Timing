using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(closePause);
    }

    // Update is called once per frame
    public void closePause()
    {
        Time.timeScale = 1f;
        Transform theQuit = transform.parent.Find("Quit");
        theQuit.Find("Save").GetComponent<Image>().enabled = false;
        theQuit.Find("Save").Find("Text").GetComponent<Text>().enabled = false;
        theQuit.Find("Save").GetComponent<Button>().enabled = false;
        theQuit.Find("Do Not Save").GetComponent<Image>().enabled = false;
        theQuit.Find("Do Not Save").Find("Text").GetComponent<Text>().enabled = false;
        theQuit.Find("Do Not Save").GetComponent<Button>().enabled = false;
        theQuit.GetComponent<Image>().enabled = true;
        theQuit.Find("Text").GetComponent<Text>().enabled = true;
        theQuit.GetComponent<Button>().enabled = true;
        transform.parent.gameObject.SetActive(false);
    }
}
