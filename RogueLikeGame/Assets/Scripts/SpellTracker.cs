using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpellTracker : MonoBehaviour
{
    public PlayerClass pc;
    public delegate IEnumerator spell(int manaUsed, Spells s);
    /*public List<spell> allSpells = new List<spell>();
    public List<string> spellNames = new List<string>();
    public List<Sprite> spellSprites = new List<Sprite>();*/
    public List<SpellStruct> spells = new List<SpellStruct>();
    public static SpellTracker main;
    public GameObject spellPrefab;
    // Start is called before the first frame update
    void Start()
    {
       
        main = this;
        Start2();
       
    }

    private void putIn(spell s, string n, Sprite theS)
    {
        SpellStruct sp = new SpellStruct(s, theS, n);
        spells.Add(sp);
    }
    public void CreateSpell(int index, int manaUsed)
    {
        GameObject g = Instantiate(spellPrefab);
        g.GetComponent<Spells>().createSpell(spells[index].spell, manaUsed);
        
    }
    
    // Start is called before the first frame update
    //Keep spells in alphabetical order
    public void Start2()
    {
        spells.Clear();
        putIn(nothing, "nothing", null);
        putIn(lightning, "Lightning", Resources.Load<Sprite>("Spells/SpellItems/LIGHTINGITEM"));
        putIn(meteor, "Meteor", null);
    }


    public IEnumerator lightning(int manaUsed, Spells s)
    {
        Vector3 mouse = Input.mousePosition;
        s.gameObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(mouse);
        s.showSpell(1, 1);
        yield return new WaitForSeconds(.2f);
        EntityClass[] hits = new EntityClass[s.inside.Count];
        int i = 0;
        //Debug.Log(s.inside.Count + "balls");
        foreach (Collider2D c in s.inside)
        {
            hits[i] = c.gameObject.GetComponent<EntityClass>();
            //Debug.Log("boomed");
            i++;
        }
        foreach (EntityClass ec in hits)
        {
            if (ec != null)
            {
                ec.getHit(Convert.ToSingle(.5f * manaUsed), "lightning");
                
            }
        }
        yield return new WaitForSeconds(2f);
        Destroy(s.gameObject);
    }
    public IEnumerator meteor(int manaUsed, Spells s)
    {
        Vector3 mouse = Input.mousePosition;
        s.gameObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(mouse);
        s.showSpell(2, 2);
        yield return new WaitForSeconds(3f);
        EntityClass[] hits = new EntityClass[s.inside.Count];
        int i = 0;
        //Debug.Log(s.inside.Count + "balls");
        foreach (Collider2D c in s.inside)
        {
            hits[i] = c.gameObject.GetComponent<EntityClass>();
            //Debug.Log("boomed");
            i++;
        }
        foreach (EntityClass ec in hits)
        {
            if (ec != null)
            {
                ec.getHit(Convert.ToSingle((2 - Vector3.Distance(s.transform.position, ec.ecgetObject().transform.position)) * (manaUsed / 2.0f)), "explosion");

            }
        }
        yield return new WaitForSeconds(0.2f);
        Destroy(s.gameObject);
    }

    public IEnumerator nothing(int manaUsed, Spells s)
    {
        yield return new WaitForSeconds(1f);
        Destroy(s.gameObject);
    }
    
}
public struct SpellStruct
{
    public SpellTracker.spell spell;
    public Sprite spellSprite;
    public string spellName;
    public SpellStruct(SpellTracker.spell s, Sprite sp, string spn)
    {
        spell = s;
        spellSprite = sp;
        spellName = spn;
    }
}
