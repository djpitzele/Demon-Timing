using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotScript : MonoBehaviour, NPClass
{
    public bool[] roll = new bool[3];
    public float cooldown = .2f;
    public System.Random r = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        if(cooldown <= 0)
        {
            foreach(bool b in roll)
            {
                i++;
                if (b)
                {
                    transform.GetChild(i).GetComponent<SlotNumber>().number = r.Next(9) + 1;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GamblingToken"))
        {
            roll[0] = true;
            roll[1] = true;
            roll[2] = true;
            Destroy(other.gameObject);
        }
        Debug.Log(other.name);
     
    }
    public Interaction Interact()
    {
        int i = 0;
        foreach(bool b in roll)
        {
            i++;
            if (b)
            {
                roll[i] = false;
                break;
            }
        }
        return null;
    }
}
