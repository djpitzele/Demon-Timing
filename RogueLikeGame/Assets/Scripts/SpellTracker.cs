using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpellTracker : MonoBehaviour
{
    public PlayerClass pc;
    public delegate IEnumerator spell(int manaUsed, Spells s);
    public List<spell> allSpells = new List<spell>();
    public List<string> spellNames = new List<string>();
    public List<Sprite> spellSprites = new List<Sprite>();
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
        allSpells.Add(s);
        spellNames.Add(n);
        spellSprites.Add(theS);
    }
    public void CreateSpell(int index, int manaUsed)
    {
        GameObject g = Instantiate(spellPrefab);
        //SPELLS STILL AT 0, 0 FOR SOME REASON
        g.GetComponent<Spells>().createSpell(allSpells[index], manaUsed, this);
        
    }
    
    // Start is called before the first frame update
    //Keep spells in alphabetical order
    public void Start2()
    {
        allSpells.Clear();
        spellNames.Clear();
        spellSprites.Clear();
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
        yield return new WaitForSeconds(3f);
        s.showSpell(3, 1);
        EntityClass[] hits = new EntityClass[s.inside.Count];
        int i = 0;
        
        foreach (Collider2D c in s.inside)
        {
            hits[i] = c.gameObject.GetComponent<EntityClass>();
            Debug.Log("boomed");
            i++;
        }
        foreach (EntityClass ec in hits)
        {
            if (ec != null)
            {
                ec.getHit(Convert.ToSingle(2 * manaUsed), "explosion");
            }
        }
        Destroy(s.gameObject);
    }

    public IEnumerator nothing(int manaUsed, Spells s)
    {
        yield return new WaitForSeconds(1f);
        Destroy(s.gameObject);
    }
    
}
