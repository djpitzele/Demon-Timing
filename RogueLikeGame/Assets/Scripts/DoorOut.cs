using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOut : MonoBehaviour, NPClass
{
    public static DoorOut main;
    public floorCreator floor;
    public TileSetter theTiles;
    // Start is called before the first frame update
    void Start()
    {
        main = this;
        transform.position = new Vector3(theTiles.width + 0.35f, theTiles.height / 2.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Interaction Interact()
    {
        PlayerClass pc = PlayerClass.main;
        if(SceneManager.GetActiveScene().name == "Lobby")
        {
            pc.updateChoices();
            ShadeScript.sh.DisableTempText();
        }
            if (floor.waves <= 0)
            {
               
                pc.nextScene();
            }
               pc.theCanvas.transform.Find("Shade").GetComponent<UnityEngine.UI.Text>().enabled = false;
            pc.theCanvas.transform.Find("Gold").GetComponent<UnityEngine.UI.Text>().enabled = false;
        return null;
    }
}
