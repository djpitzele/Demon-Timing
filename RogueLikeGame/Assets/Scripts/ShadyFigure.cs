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
            bms.costs = new List<string>();
            bms.costs.Add(FindCost().ToString() + " Shade");
            bms.gameObject.SetActive(true);
            

        }
        else if (interactions == 2)
        {
            interactions = 0;
            bms.labels = new List<string>();
            bms.purchaseActions = new List<UnityEngine.Events.UnityAction>();
            bms.costs = new List<string>();
            bms.gameObject.SetActive(false);
            bms.destroyAllOptions();

        }
        return null;
    }
    public void UpMelee()
    {
        if(PermVar.current.Shade >= FindCost())
        {
            //PermVar.setCurrent(PermVar.current.Shade -= FindCost(), PermVar.current.meleeBuff += .1f);
            PermVar.current.Shade -= FindCost();
            PermVar.current.meleeBuff += 0.1f;
            bms.transform.parent.GetComponentsInChildren<ShadeScript>()[0].updateShadeLobby();
            bms.options[0].gameObject.GetComponentsInChildren<UnityEngine.UI.Text>()[1].text = FindCost().ToString() + " Shade";
        }
        
       /* if(TryGetComponent<Animator>(out Animator a) && a.GetBool("Talking"))
        {
            a.SetBool("Talking", true);
        }*/
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
            bms.transform.parent.Find("Interaction").gameObject.SetActive(false);
            bms.gameObject.SetActive(false);
            bms.destroyAllOptions();
            interactions = 0;
        }
    }

    private int FindCost()
    {
        return 1 + (int)Math.Pow((PermVar.current.meleeBuff / .1f), 1.62f);
    }
}
