using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMartie : MonoBehaviour, NPClass
{
    public int interactions = 0;
    public delegate void drop(int a);
    public GameObject spellPrefab;
    public bool inRange = false;
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
       
        //bms.gameObject.SetActive(false);
        r = new System.Random();
        int total = 0;
        //Debug.Log(total);
        List<int> s = new List<int>();
        s.Add(1); s.Add(2); s.Add(3); s.Add(4);
        total = s.Count;
        s1 = s[r.Next(total)];
        s.Remove(s1);
        s2 = s[r.Next(total - 1)];
        s.Remove(s2);
        s3 = s[r.Next(total - 2)];
        s.Remove(s3);
        p1 = 5;
        p2 = 10;
        p3 = 15;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DropSpell(int ind)
    {
        GameObject g = Instantiate(spellPrefab);
        SpellItemScript sis = g.GetComponent<SpellItemScript>();
        g.transform.position = transform.position + new Vector3(-5f, 5f, 0);
        sis.spellIndex = ind;
        sis.interactions = 0;
        GetComponent<BoxCollider2D>().enabled = false;
    }
    public Interaction Interact()
    {
        bms = PlayerClass.main.bms;
        if (interactions == 0)
        {
           
            interactions++;
            return (new Interaction(null, "Greetings. I can exchange your excess shade for some magical abilities.", Resources.GetBuiltinResource<Font>("Arial.ttf")));

        }
        else if (interactions == 1)
        {
            SkillTreeScript.sts.updateTemp();
            SpellTracker.main.Start2();
            interactions++;
            bms.purchaseActions = new List<UnityEngine.Events.UnityAction>();
            bms.purchaseActions.Add(spell1);
            bms.purchaseActions.Add(spell2);
            bms.purchaseActions.Add(spell3);
            //Debug.Log("shady" + bms.purchaseActions.Count);
            bms.labels = new List<string>();
            bms.labels.Add(SpellTracker.main.spells[s1].spellName);
            bms.labels.Add(SpellTracker.main.spells[s2].spellName);
            bms.labels.Add(SpellTracker.main.spells[s3].spellName);
            bms.costs = new List<string>();
            bms.costs.Add(p1 + " Shade");
            bms.costs.Add(p2 + " Shade");
            bms.costs.Add(p3 + " Shade");
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
    public void spell1()
    {
        if (SkillTreeScript.sts.tempShade - SkillTreeScript.sts.spentShade >= p1)
        {
            DropSpell(s1);
            SkillTreeScript.sts.spentShade += p1;
        }
    }
    public void spell2()
    {
        if (SkillTreeScript.sts.tempShade - SkillTreeScript.sts.spentShade >= p2)
        {
            DropSpell(s2);
            SkillTreeScript.sts.spentShade += p2;
        }
    }
    public void spell3()
    {
        if (SkillTreeScript.sts.tempShade - SkillTreeScript.sts.spentShade >= p3)
        {
            DropSpell(s3);
            SkillTreeScript.sts.spentShade += p3;
        }
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


