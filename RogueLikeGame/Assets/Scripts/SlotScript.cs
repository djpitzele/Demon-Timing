using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SlotScript : MonoBehaviour, NPClass
{
    public GameObject Gold;
    public bool[] roll = new bool[3];
    public float cooldown = .3f;
    public float[] initialcooldowns = { .6f, .4f, .2f };
    public float initialcd;
    public float cooldown2;
    public System.Random r = new System.Random();
    public bool talked = false;
    public int tokenDiff;
    // Start is called before the first frame update
    void Start()
    {
        talked = true;
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        if(cooldown <= 0)
        {
            foreach(bool b in roll)
            {
   
                if (b)
                {
                    Debug.Log("Number" + (i + 1).ToString());
                    transform.Find("Number" + (i+1).ToString()).GetComponent<SlotNumber>().number = r.Next(9);
                   
                    
                }
                i++;
            }
            cooldown = initialcd;

        }
        cooldown -= Time.deltaTime;
        cooldown2 -= Time.deltaTime;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("GamblingToken"))
        {
            talked = false;
            tokenDiff = other.gameObject.GetComponent<TokenScript>().difficulty;
            initialcd = initialcooldowns[tokenDiff];
            roll[0] = true;
            roll[1] = true;
            roll[2] = true;
            Destroy(other.gameObject);
        }
        Debug.Log(other.gameObject.name);
    }
    public Interaction Interact()
    {
        if (cooldown2 <= 0)
        {
            cooldown2 = .05f;
            if (talked)
            {
                return null;
            }
            else
            {
                int i = 0;
                foreach (bool b in roll)
                {

                    if (b)
                    {
                        roll[i] = false;
                        if (i == 2)
                        {
                            return endGame();
                        }
                        break;
                    }
                    i++;

                }
                

            }
        }
        return null;


    }
    public Interaction endGame()
    {

        talked = true;
        float w = won(transform.Find("Number" + (1).ToString()).GetComponent<SlotNumber>().number, transform.Find("Number" + (1 + 1).ToString()).GetComponent<SlotNumber>().number, transform.Find("Number" + (2 + 1).ToString()).GetComponent<SlotNumber>().number);
        if (w > 0)
        {
            GameObject g = Instantiate(Gold);
            g.transform.position = transform.position + new Vector3(4, -4, 0);
            g.GetComponent<StackDrop>().min = (int)(w * 10);
            g.GetComponent<StackDrop>().max = (int)(w * 10);
            g.GetComponent<StackDrop>().value = (int)(w * 10);
            return new Interaction(GamblerScript.gamblerSprite, "Good Job You Won " + w * 10 + " Gold", Resources.GetBuiltinResource<Font>("Arial.ttf"));
        }
        else
        {
            if (PlayerClass.main.gold < 0)
            {
                PlayerClass.main.getHit(Int32.MaxValue, "gambling");
            }
            return new Interaction(GamblerScript.gamblerSprite, "get better lol", Resources.GetBuiltinResource<Font>("Arial.ttf"));
        }


        
       
    } 
    public float won(int n1, int n2, int n3)
    {
        if(same3(n1,n2,n3))
        {
            return 1.6f * (tokenDiff + 1);
        }
        else if(straight(n1, n2, n3))
        {
            return 1.4f * (tokenDiff + 1);
        }
        else if (same2(n1, n2, n3))
        {
            return 1.2f * (tokenDiff + 1);
        }
        return 0f;
    }
    public bool same3(int n1, int n2, int n3)
    {
        return (n1 == n2 && n2 == n3);
    }
    public bool same2(int n1, int n2, int n3)
    {
        return (n1 == n2 || n2 == n3 || n1 == n3);
    }
    public bool straight(int n1, int n2, int n3)
    {
        if(n2 == n1 + 1)
        {
            return (n3 == n2 + 1);
        }
        if (n2 == n1 - 1)
        {
            return (n3 == n2 - 1);
        }
        else { return false; }
        
    }
}
