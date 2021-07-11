using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOut : MonoBehaviour
{
    public floorCreator floor;
    public TileSetter theTiles;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(theTiles.width + 0.35f, theTiles.height / 2.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (floor.waves <= 0)
        {
            floor.player.GetComponent<PlayerClass>().nextScene();
        }
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Lobby")
        {
            floor.player.GetComponent<PlayerClass>().theCanvas.transform.Find("Shade").GetComponent<UnityEngine.UI.Text>().enabled = false;
        }
    }
}
