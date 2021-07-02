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
        GameObject.Find("MainChar").transform.position = new Vector3(pos.x, pos.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
