using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartieScript : MonoBehaviour, NPClass
{
    public Sprite martieSprite;
    public delegate void drop(int a);
    public GameObject spellPrefab;
    public bool inRange = false;
    public int interactions = 0;
    public BuyMenuScript bms;
    public PlayerClass pc;
    public System.Random r;
    private int s1;
    private int s2;
    private int s3;
    private int p1;
    private int p2;
    private int p3;
    // Start is called before the first frame update
    void Start()
    {
        SpellTracker.main.Start2();
        bms = PlayerClass.main.bms;
        bms.gameObject.SetActive(false);
        r = new System.Random();
        int total = SpellTracker.main.spellNames.Count;
        //Debug.Log(total);
        s1 = r.Next(total);
        s2 = r.Next(total);
        s3 = r.Next(total);
        p1 = 25 + r.Next(25);
        p2 = 25 + r.Next(30);
        p3 = 25 + r.Next(40);
    }
    // Update is called once per frame
    public void spell1()
    {
        if(PlayerClass.main.gold >= p1)
        {
            DropSpell(s1);
            PlayerClass.main.gold -= p1;
        }
    }
    public void spell2()
    {
        if (PlayerClass.main.gold >= p2)
        {
            DropSpell(s2);
            PlayerClass.main.gold -= p2;
        }
    }
    public void spell3()
    {
        if (PlayerClass.main.gold >= p3)
        {
            DropSpell(s3);
            PlayerClass.main.gold -= p3;
        }
    }
    public void DropSpell(int ind)
     {
        GameObject g = Instantiate(spellPrefab);
        SpellItemScript sis = g.GetComponent<SpellItemScript>();
        g.transform.position = transform.position + new Vector3(0, -1f, 0);
        sis.spellIndex = ind;
        sis.interactions = 0;
        GetComponent<BoxCollider2D>().enabled = false;
     }

     public Interaction Interact()
     {
         if (interactions == 0)
         {
             interactions++;
             return (new Interaction(martieSprite, "Greetings. I can exchange your gold for some magical abilities.", Resources.GetBuiltinResource<Font>("Arial.ttf")));
         }
         else if (interactions == 1)
         {
             SpellTracker.main.Start2();
             interactions++;
             bms.purchaseActions = new List<UnityEngine.Events.UnityAction>();
             bms.purchaseActions.Add(spell1);
             bms.purchaseActions.Add(spell2);
             bms.purchaseActions.Add(spell3);
             //Debug.Log("shady" + bms.purchaseActions.Count);
             bms.labels = new List<string>();
             bms.labels.Add(SpellTracker.main.spellNames[s1]);
             bms.labels.Add(SpellTracker.main.spellNames[s2]);
             bms.labels.Add(SpellTracker.main.spellNames[s3]);
             bms.costs = new List<string>();
             bms.costs.Add(p1 + " Gold");
             bms.costs.Add(p2 + " Gold");
             bms.costs.Add(p3 + " Gold");
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
}
