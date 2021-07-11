using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShadyFigure : MonoBehaviour, NPClass
{
    public Sprite shadySprite;
    public bool inRange = false;
    public int interactions = 0;
    public BuyMenuScript bms;
    // Start is called before the first frame update
    void Start()
    {
        bms = GameObject.Find("Canvas").transform.Find("BuyMenu").gameObject.GetComponent<BuyMenuScript>();
        bms.gameObject.SetActive(false);
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
    public Interaction Interact()
    {
        //.Log(interactions);
        if (interactions == 0)
        {
            interactions++;
            return (new Interaction(shadySprite, "Greetings. I can exchange your shade for a bonus in combat.", Resources.GetBuiltinResource<Font>("Arial.ttf")));
        }
        else if(interactions == 1)
        {
            interactions++;
            bms.purchaseActions = new List<UnityEngine.Events.UnityAction>();
            bms.purchaseActions.Add(UpMelee);
            //Debug.Log("shady" + bms.purchaseActions.Count);
            bms.labels = new List<string>();
            bms.labels.Add("melee attack up");
            bms.gameObject.SetActive(true);
            

        }
        else if (interactions == 2)
        {
            interactions = 0;
            bms.labels = new List<string>();
            bms.purchaseActions = new List<UnityEngine.Events.UnityAction>();
            bms.gameObject.SetActive(false);
           

        }
        return null;
    }
    public void UpMelee()
    {
        if(PermVar.current.Shade > FindCost())
        {
            PermVar.setCurrent(PermVar.current.Shade -= FindCost(), PermVar.current.meleeBuff += .1f);
        }
        
        if(TryGetComponent<Animator>(out Animator a) && a.GetBool("Talking"))
        {
            a.SetBool("Talking", true);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass pc))
        {
            inRange = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass pc))
        {
            inRange = false;
        }
    }

    private int FindCost()
    {
        return (int)Math.Pow((PermVar.current.meleeBuff / .1f), 1.42f);
    }
}
