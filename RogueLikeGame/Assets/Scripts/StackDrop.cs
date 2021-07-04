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
        if(collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass pc))
        {
            pc.gold += value;
            pc.theCanvas.transform.GetChild(4).gameObject.GetComponent<UnityEngine.UI.Text>().text = "Gold: " + pc.gold;
            Destroy(this.gameObject);
        }
    }
}
