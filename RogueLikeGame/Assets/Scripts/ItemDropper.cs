using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    //chance is from 0 to 1, 1 = 100% chance
    public float chance;
    public GameObject spawned;
    private System.Random r;
    public int min = 0;
    public int max = 0;

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
            GameObject theSpawned = Instantiate(spawned, this.transform.position, Quaternion.identity);
            if(theSpawned.TryGetComponent<StackDrop>(out StackDrop gss))
            {
                gss.min = min;
                gss.max = max;
            }
        }
    }
}
