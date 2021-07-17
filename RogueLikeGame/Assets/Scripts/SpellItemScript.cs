using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellItemScript : MonoBehaviour, NPClass
{
    public int interactions;
    public int spellIndex;
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
            bms.costs.Add(SpellTracker.main.spellNames[PlayerClass.main.spells[0]]);
            bms.costs.Add(SpellTracker.main.spellNames[PlayerClass.main.spells[1]]);
            bms.costs.Add(SpellTracker.main.spellNames[PlayerClass.main.spells[2]]);
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
        PlayerClass.main.spells[0] = spellIndex;
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
        PlayerClass.main.spells[1] = spellIndex;
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
        PlayerClass.main.spells[2] = spellIndex;
        interactions = 0;
        bms.labels = new List<string>();
        bms.purchaseActions = new List<UnityEngine.Events.UnityAction>();
        bms.costs = new List<string>();
        bms.gameObject.SetActive(false);
        bms.destroyAllOptions();
        Destroy(this.gameObject);
    }
}
