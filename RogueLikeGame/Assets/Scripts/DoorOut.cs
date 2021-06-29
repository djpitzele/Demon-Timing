using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOut : MonoBehaviour
{
    public floorCreator floor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (floor.waves <= 0)
        {
            floor.player.GetComponent<MovementScript>().resetscene();
        }
    }
}
