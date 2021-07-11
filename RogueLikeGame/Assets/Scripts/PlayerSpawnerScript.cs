using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerScript : MonoBehaviour
{
    public TileSetter theCreator;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 pos = new Vector2(1, theCreator.height / 2.0f);
        GameObject g = GameObject.Find("MainChar");
        g.transform.position = new Vector3(pos.x, pos.y, 0);
        g.GetComponent<PlayerClass>().theCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
