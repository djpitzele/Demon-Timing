using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void updateShade()
    {
        GetComponent<Text>().enabled = true;
        transform.parent.Find("Gold").GetComponent<Text>().enabled = false;
        GetComponent<Text>().text = "Shade: " + (PermVar.current.Shade + transform.parent.Find("Pause Menu").GetComponentsInChildren<RestartScript>()[0].myPlayer.GetComponent<PlayerClass>().curShade);
        Invoke("DisableText", 3f);
        
    }
    public void updateShadeLobby()
    {
        GetComponent<Text>().enabled = true;
        transform.parent.Find("Gold").GetComponent<Text>().enabled = false;
        GetComponent<Text>().text = "Shade: " + (PermVar.current.Shade + PlayerClass.main.curShade);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisableText()
    {
        GetComponent<Text>().enabled = false;
    }
}
