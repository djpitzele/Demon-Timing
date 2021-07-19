using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpellItemScript : MonoBehaviour, NPClass
{
    public int interactions;
    public int spellIndex2;
    public int[] spells2 = new int[3];
    public int spellIndex 
    {
        get { return spellIndex2; }
        set { spellIndex2 = value; GetComponent<SpriteRenderer>().sprite = SpellTracker.main.spells[value].spellSprite; }
    }
    public BuyMenuScript bms;
    // Start is called before the first frame update
    void Start()
    {
        
        bms = PlayerClass.main.bms;
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
            bms.purchaseActions = new List<UnityEngine.Events.UnityAction>();
            bms.purchaseActions.Add(slot1);
            bms.purchaseActions.Add(slot2);
            bms.purchaseActions.Add(slot3);
            //Debug.Log("shady" + bms.purchaseActions.Count);
            bms.labels = new List<string>();
            bms.labels.Add("replace slot 1");
            bms.labels.Add("replace slot 2");
            bms.labels.Add("replace slot 3");
            bms.costs = new List<string>();
            bms.costs.Add(SpellTracker.main.spells[PlayerClass.main.spells[0]].spellName);
            bms.costs.Add(SpellTracker.main.spells[PlayerClass.main.spells[1]].spellName);
            bms.costs.Add(SpellTracker.main.spells[PlayerClass.main.spells[2]].spellName);
            bms.gameObject.SetActive(true);


        }
        else if (interactions == 1)
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
    public void OnTriggerExit2D(Collider2D collision)
    {
        interactions = 0;
        bms.labels = new List<string>();
        bms.purchaseActions = new List<UnityEngine.Events.UnityAction>();
        bms.costs = new List<string>();
        bms.gameObject.SetActive(false);
        bms.destroyAllOptions();
    }
    public void slot1() 
    {
        Array.Copy(PlayerClass.main.spells, spells2, 3);
        spells2[0] = spellIndex;
        PlayerClass.main.spells = spells2;
        interactions = 0;
        bms.labels = new List<string>();
        bms.purchaseActions = new List<UnityEngine.Events.UnityAction>();
        bms.costs = new List<string>();
        bms.gameObject.SetActive(false);
        bms.destroyAllOptions();
        Destroy(this.gameObject);
    }
    public void slot2()
    {

        Array.Copy(PlayerClass.main.spells, spells2, 3);
        spells2[1] = spellIndex;
        PlayerClass.main.spells = spells2;
        interactions = 0;
        bms.labels = new List<string>();
        bms.purchaseActions = new List<UnityEngine.Events.UnityAction>();
        bms.costs = new List<string>();
        bms.gameObject.SetActive(false);
        bms.destroyAllOptions();
        Destroy(this.gameObject);
    }
    public void slot3()
    {
        Array.Copy(PlayerClass.main.spells, spells2, 3);
        spells2[2] = spellIndex;
        PlayerClass.main.spells = spells2;
        interactions = 0;
        bms.labels = new List<string>();
        bms.purchaseActions = new List<UnityEngine.Events.UnityAction>();
        bms.costs = new List<string>();
        bms.gameObject.SetActive(false);
        bms.destroyAllOptions();
        Destroy(this.gameObject);
    }
}
