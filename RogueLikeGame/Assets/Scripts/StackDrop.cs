using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackDrop : MonoBehaviour
{
    public int min;
    public int max;
    public int value;
    public System.Random r;
    // Start is called before the first frame update
    void Start()
    {
        r = new System.Random();
        value = r.Next(min, max);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass pc) && gameObject.CompareTag("GoldStack"))
        {
            pc.gold += value;
            Destroy(this.gameObject);
        }
        if (collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass plc) && gameObject.CompareTag("ManaJar"))
        {
            plc.curMana += value;
            Destroy(this.gameObject);
        }
    }
}
