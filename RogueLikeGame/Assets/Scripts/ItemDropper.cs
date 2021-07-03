using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public float chance;
    public GameObject spawned;
    private System.Random r;

    // Start is called before the first frame update
    void Start()
    {
        r = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnItem()
    {
        if(r.Next(1000) <= (chance * 1000))
        {
            Instantiate(spawned, this.transform.position, Quaternion.identity);
        }
    }
}
