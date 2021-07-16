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
    public PlayerClass pc;
    // Start is called before the first frame update
    void Start()
    {
        bms = GameObject.Find("Canvas").transform.Find("BuyMenu").gameObject.GetComponent<BuyMenuScript>();
        PlayerClass.main.bms = bms;
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
            bms.purchaseActions.Add(UpHealth);
            bms.purchaseActions.Add(UpMana);
            //Debug.Log("shady" + bms.purchaseActions.Count);
            bms.labels = new List<string>();
            bms.labels.Add("melee attack up");
            bms.labels.Add("health up");
            bms.labels.Add("mana up");
            bms.costs = new List<string>();
            bms.costs.Add(FindCostMelee().ToString() + " Shade");
            bms.costs.Add(FindCostHealth().ToString() + " Shade");
            bms.costs.Add(FindCostMana().ToString() + " Shade");
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

    public void UpHealth()
    {
        if (PermVar.current.Shade >= FindCostHealth())
        {
            //PermVar.setCurrent(PermVar.current.Shade -= FindCost(), PermVar.current.meleeBuff += .1f);
            PermVar.current.Shade -= FindCostHealth();
            PermVar.current.healthBuff += 5;
            pc.curHP += 5;
            pc.maxHP += 5;
            bms.transform.parent.Find("Health").GetComponent<HealthCheck>().updateHealth(pc);
            bms.transform.parent.GetComponentsInChildren<ShadeScript>()[0].updateShadeLobby();
            bms.options[1].gameObject.GetComponentsInChildren<UnityEngine.UI.Text>()[1].text = FindCostHealth().ToString() + " Shade";
        }
    }

    public void UpMana()
    {
        if (PermVar.current.Shade >= FindCostMana())
        {
            //PermVar.setCurrent(PermVar.current.Shade -= FindCost(), PermVar.current.meleeBuff += .1f);
            PermVar.current.Shade -= FindCostMana();
            PermVar.current.manaBuff += 5;
            pc.curMana += 5;
            pc.maxMana += 5;
            bms.transform.parent.GetComponentsInChildren<ShadeScript>()[0].updateShadeLobby();
            bms.options[2].gameObject.GetComponentsInChildren<UnityEngine.UI.Text>()[1].text = FindCostMana().ToString() + " Shade";
        }
    }

    public void UpMelee()
    {
        if(PermVar.current.Shade >= FindCostMelee())
        {
            //PermVar.setCurrent(PermVar.current.Shade -= FindCost(), PermVar.current.meleeBuff += .1f);
            PermVar.current.Shade -= FindCostMelee();
            PermVar.current.meleeBuff += 0.1f;
            bms.transform.parent.GetComponentsInChildren<ShadeScript>()[0].updateShadeLobby();
            bms.options[0].gameObject.GetComponentsInChildren<UnityEngine.UI.Text>()[1].text = FindCostMelee().ToString() + " Shade";
        }
        
       /* if(TryGetComponent<Animator>(out Animator a) && a.GetBool("Talking"))
        {
            a.SetBool("Talking", true);
        }*/
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass pcg))
        {
            inRange = true;
            pc = pcg;
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

    private static int FindCostHealth()
    {
        return 1 + (int)Math.Pow(PermVar.current.healthBuff / 5, 2.43f);
    }

    private static int FindCostMelee()
    {
        return 1 + (int)Math.Pow((PermVar.current.meleeBuff / .1f), 1.62f);
    }

    private static int FindCostMana()
    {
        return 1 + (int)Math.Pow(PermVar.current.manaBuff / 5, 1.38);
    }
}
