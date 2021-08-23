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
    public float spellDmg = 1f; //1 = default spell damage
    // Start is called before the first frame update
    void Awake()
    {
       
        main = this;
        
       
    }
    private void Start()
    {
        Start2();
    }
    private void putIn(spell s, string n, Sprite theS)
    {
        SpellStruct sp = new SpellStruct(s, theS, n);
        spells.Add(sp);
    }
    public void hitList(List<Collider2D> cs, float dmg, string dmgType)
    {
        Collider2D[] cds = new Collider2D[cs.Count];
        int i = 0;
        foreach(Collider2D c in cs)
        {
            cds[i] = c;
            i++;
        }
        foreach(Collider2D c in cds)
        {
            if(c.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
            {
                ec.getHit(dmg * spellDmg, dmgType);
            }
        }
    }
    public void CreateSpell(int index, int manaUsed)
    {
        GameObject go = new GameObject();
        go.name = "Spell";
        GameObject g = Instantiate(spellPrefab, go.transform);
        g.GetComponent<Spells>().createSpell(spells[index].spell, manaUsed);
    }
    
    // Start is called before the first frame update
    public void Start2()
    {
        spells.Clear();
        putIn(nothing, "nothing", null);
        putIn(lightning, "Lightning", Resources.Load<Sprite>("Spells/SpellItems/LIGHTINGITEM"));
        putIn(meteor, "Meteor", Resources.Load<Sprite>("Spells/SpellItems/meteoritem"));
        putIn(freeZe, "Freeze", Resources.Load<Sprite>("Spells/SpellItems/FreezeItem"));
        putIn(rage, "Rage", Resources.Load<Sprite>("Spells/SpellItems/rageitem"));
        putIn(firePipe, "FirePipe", Resources.Load<Sprite>("Firepipeitem")); // 5 //put animation into firepipe
        putIn(arrowRain, "Arrow Rain", Resources.Load<Sprite>("arrowRainItem"));
    }

    public void destroySpell(Spells s)
    {
        GameObject g = s.gameObject.transform.parent.gameObject;
        if(s.runFunction[3])
        {
            s.onDestroy.Invoke(s.manaUsed, s);
        }
        Destroy(g);
    }
    public IEnumerator lightning(int manaUsed, Spells s)
    {
        s.showSpell(1, 1);
        Vector3 mouse = Input.mousePosition;
        s.gameObject.transform.parent.position = (Vector2)Camera.main.ScreenToWorldPoint(mouse);
        yield return new WaitForSeconds(.2f);
        hitList(s.inside, Convert.ToSingle(.5f * manaUsed), "lightning");
        yield return new WaitForSeconds(2f);
        destroySpell(s);
    }
    public IEnumerator meteor(int manaUsed, Spells s)
    {
        Vector3 mouse = Input.mousePosition;
        s.gameObject.transform.parent.position = (Vector2)Camera.main.ScreenToWorldPoint(mouse) + new Vector2(5f, 10f);
        s.showSpell(2, 2);
        yield return new WaitForSeconds(3f);
        Collider2D[] cds = new Collider2D[s.inside.Count];
        int i = 0;
        foreach (Collider2D c in s.inside)
        {
            cds[i] = c;
            i++;
        }
        foreach (Collider2D c in cds)
        {
            if (c.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
            {
                ec.getHit(Convert.ToSingle((2 - Vector3.Distance(s.transform.position, ec.ecgetObject().transform.position)) * (manaUsed / 2.0f)) * spellDmg, "explosion");
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
        s.onDestroy = freezeDestroy;
        s.onEnter = freezeEnter;
        s.onExit = freezeExit;
        Vector3 mouse = Input.mousePosition;
        s.gameObject.transform.parent.position = (Vector2)Camera.main.ScreenToWorldPoint(mouse);
        float sp = 1f - (manaUsed / 120f);
        s.showSpell(1, 3);
        yield return new WaitForSeconds(4f);
        destroySpell(s);
    }
    public IEnumerator freezeEnter(int manaUsed, Spells s, Collider2D c)
    {
        if (c.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
        {
            ec.setSpeed(1f - (manaUsed / 120f));

        }
        yield return null;
    }
    public IEnumerator freezeExit(int manaUsed, Spells s, Collider2D c)
    {
        yield return null;
        if (c != null && c.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
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
        s.onDestroy = rageDestroy;
        s.onEnter = rageEnter;
        s.onExit = rageExit;
        Vector3 mouse = Input.mousePosition;
        s.showSpell(3, 4);
        yield return new WaitForEndOfFrame();
        s.gameObject.transform.parent.position = (Vector2)Camera.main.ScreenToWorldPoint(mouse);
        float speedChange = 1 + (manaUsed / 85f);
        yield return new WaitForSeconds(5f);
        destroySpell(s);
    }
    public IEnumerator rageEnter(int manaUsed, Spells s, Collider2D c)
    {
        yield return null;
        if (c.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
        {
            ec.setSpeed(1 + (manaUsed / 85f));

        }
    }
    public IEnumerator rageExit(int manaUsed, Spells s, Collider2D c)
    {
        yield return null;
        if (c.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
        {
            ec.setSpeed(1f / (1 + (manaUsed / 85f)));

        }
    }
    public void rageDestroy(int manaUsed, Spells s)
    {

        foreach (Collider2D c in s.inside)
        {
            if (c.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
            {
                ec.setSpeed(1f / (1 + (manaUsed / 85f)));

            }
        }
    }
    public IEnumerator firePipe(int manaUsed, Spells s)
    {
        s.showSpell(0, 5); //change 1 to the animation
        Destroy(s.GetComponent<CircleCollider2D>());
        BoxCollider2D bc = s.gameObject.AddComponent<BoxCollider2D>();
        bc.offset = new Vector2(0, 0);
        bc.size = new Vector2(1, 1.2f);
        bc.isTrigger = true;
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        GridLayout gl = floorCreator.main.transform.parent.GetComponent<GridLayout>();
        Vector3Int cellCoords = gl.WorldToCell(mouse);
        Vector3 finalPos = gl.CellToWorld(cellCoords) + new Vector3(0.5f, 0.5f, 0);
        s.gameObject.transform.parent.position = finalPos;
        yield return new WaitForSeconds(0.2f);
        s.onEnter = fireEnter;
        for (int i = 0; i < 5; i++)
        {
            hitList(s.inside, manaUsed / 1.5f, "fire");
            yield return new WaitForSeconds(1f);
        }
        destroySpell(s);
    }
    public IEnumerator fireEnter(int manaUsed, Spells s, Collider2D other)
    {
        yield return null;
        //Debug.Log("fire");
        if(other.gameObject.TryGetComponent<EntityClass>(out EntityClass ec)) {
            ec.getHit(manaUsed / 2.5f, "fire");
        }
    }
    public IEnumerator arrowRain(int manaUsed, Spells s)
    {
        Vector3 mouse = Input.mousePosition;
        s.gameObject.transform.parent.position = (Vector2)Camera.main.ScreenToWorldPoint(mouse);
        s.showSpell(5, 6);
        yield return new WaitForSeconds(.2f);
        s.onUpdate = arrowRainUpdate;
        yield return new WaitForSeconds(1f);
        destroySpell(s);

    }
    public void arrowRainUpdate(int manaUsed, Spells s)
    {
        hitList(s.inside, (manaUsed * 8f)/(60f * 25f), "projectile");
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
