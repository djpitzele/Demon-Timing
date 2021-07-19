using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    //chance is from 0 to 1, 1 = 100% chance
    public float[] chance;
    public GameObject[] spawned; //array index in chance matches array index in spawned
    private System.Random r;
    public int[] min;
    public int[] max;

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
        for(int i = 0; i < chance.Length; i++)
        {
            if (r.Next(1000) <= (chance[i] * 1000))
            {
                GameObject theSpawned = Instantiate(spawned[i], this.transform.position, Quaternion.identity);
                if (theSpawned.TryGetComponent<StackDrop>(out StackDrop gss))
                {
                    gss.min = min[i];
                    gss.max = max[i];
                }
            }
        }
    }
}
