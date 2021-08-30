using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShadeScript : MonoBehaviour
{
    public static ShadeScript sh;
    public GameObject tempShade;
    // Start is called before the first frame update
    void Start()
    {
        tempShade = transform.Find("TempShade").gameObject;

        Invoke("updateTempShade", .1f);
    }
    private void Awake()
    {
        sh = this;
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
    public void updateTempShade()
    {
        try
        {
            tempShade.GetComponent<Text>().enabled = true;
            tempShade.GetComponent<Text>().text = "Temp Shade: " + (SkillTreeScript.sts.tempShade - SkillTreeScript.sts.spentShade - PlayerClass.main.spentShadeSpell);
        }
        catch(NullReferenceException n)
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisableTempText()
    {
        tempShade.GetComponent<Text>().enabled = false;
    }
    public void DisableText()
    {
        GetComponent<Text>().enabled = false;
    }
}
