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

    public IEnumerator updateShade()
    {
        GetComponent<Text>().enabled = true;
        transform.parent.Find("Gold").GetComponent<Text>().enabled = false;
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Lobby")
        {
            GetComponent<Text>().text = "Shade: " + (PermVar.current.Shade + transform.parent.Find("Pause Menu").GetComponentsInChildren<RestartScript>()[0].myPlayer.GetComponent<PlayerClass>().curShade);
        }
        else
        {
            yield return new WaitForSeconds(3f);
            GetComponent<Text>().enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
