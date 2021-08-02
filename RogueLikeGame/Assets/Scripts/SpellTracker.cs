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
        GameObject go = new GameObject();
        go.name = "Spell";
        GameObject g = Instantiate(spellPrefab, go.transform);
        g.GetComponent<Spells>().createSpell(spells[index].spell, manaUsed);
    }
    
    // Start is called before the first frame update
    //Keep spells in alphabetical order
    public void Start2()
    {
        spells.Clear();
        putIn(nothing, "nothing", null);
        putIn(lightning, "Lightning", Resources.Load<Sprite>("Spells/SpellItems/LIGHTINGITEM"));
        putIn(meteor, "Meteor", Resources.Load<Sprite>("Spells/SpellItems/meteoritem"));
        putIn(freeZe, "Freeze", Resources.Load<Sprite>("Spells/SpellItems/FreezeItem"));
        putIn(rage, "Rage", Resources.Load<Sprite>("Spells/SpellItems/rageitem"));
    }

    public void destroySpell(Spells s)
    {
        GameObject g = s.gameObject.transform.parent.gameObject;
        s.onDestroy.Invoke(s.manaUsed, s);
        Destroy(g);
    }
    public IEnumerator lightning(int manaUsed, Spells s)
    {
        Vector3 mouse = Input.mousePosition;
        s.gameObject.transform.parent.position = (Vector2)Camera.main.ScreenToWorldPoint(mouse);
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
        destroySpell(s);
    }
    public IEnumerator meteor(int manaUsed, Spells s)
    {
        Vector3 mouse = Input.mousePosition;
        s.gameObject.transform.parent.position = (Vector2)Camera.main.ScreenToWorldPoint(mouse) + new Vector2(5f, 10f);
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
        destroySpell(s);
    }

    public IEnumerator nothing(int manaUsed, Spells s)
    {
        yield return new WaitForSeconds(1f);
        destroySpell(s);
    }
    public IEnumerator freeZe(int manaUsed, Spells s)

    {
        Vector3 mouse = Input.mousePosition;
        s.gameObject.transform.parent.position = (Vector2)Camera.main.ScreenToWorldPoint(mouse);
       // Debug.Log("deez moved" + s.gameObject.transform.position);
        //Debug.Log(sp);
        float sp = 1f - (manaUsed / 120f);
        s.showSpell(1, 3);
        yield return new WaitForEndOfFrame();
        s.onDestroy = freezeDestroy;
        s.onEnter = freezeEnter;
        s.onExit = freezeExit;
        List<Collider2D> cd = new List<Collider2D>();
        foreach(Collider2D c in s.inside)
        {
            cd.Add(c);
        }
       
        foreach (Collider2D c in cd)
        {
            EntityClass ec = c.gameObject.GetComponent<EntityClass>();
            ec.setSpeed(sp);
        }
        yield return new WaitForSeconds(4f);
        foreach (Collider2D c in cd)
        {
            EntityClass ec = c.gameObject.GetComponent<EntityClass>();
            ec.setSpeed(1/sp);
        }
        destroySpell(s);
    }
    public IEnumerator freezeEnter(int manaUsed, Spells s, Collider2D c)
    {
        yield return null;
        if (c.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
        {
            ec.setSpeed(1f - (manaUsed / 120f));

        }
    }
    public IEnumerator freezeExit(int manaUsed, Spells s, Collider2D c)
    {
        yield return null;
        if (c.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
        {
            ec.setSpeed(1f / (1f - (manaUsed / 120f)));

        }
    }
    public void freezeDestroy(int manaUsed, Spells s)
    {
        
        foreach (Collider2D c in s.inside)
        {
            if (c.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
            {
                ec.setSpeed(1f / (1f - (manaUsed / 120f)));

            }
        }
    }
    public IEnumerator rage(int manaUsed, Spells s)
    {
        Vector3 mouse = Input.mousePosition;
        s.showSpell(3, 4);
        s.gameObject.transform.parent.position = (Vector2)Camera.main.ScreenToWorldPoint(mouse);
        float speedChange = 1 + (manaUsed / 85f);
        List<Collider2D> cd = new List<Collider2D>();
        yield return new WaitForSeconds(0.3f);
        s.GetComponent<SpriteRenderer>().enabled = false;
        foreach (Collider2D c in s.inside)
        {
            cd.Add(c);
        }
        foreach (Collider2D c in cd)
        {
            EntityClass ec = c.gameObject.GetComponent<EntityClass>();
            ec.setSpeed(speedChange);
        }
        yield return new WaitForSeconds(5f);
        foreach (Collider2D c in cd)
        {
            EntityClass ec = c.gameObject.GetComponent<EntityClass>();
            ec.setSpeed(1f / speedChange);
        }
        destroySpell(s);
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
