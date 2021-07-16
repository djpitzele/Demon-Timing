using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public SpellTracker st;
    public int manaUsed;
    public List<Collider2D> inside = new List<Collider2D>();

    public void createSpell(SpellTracker.spell s, int manaUsed, SpellTracker str)
    {
        StartCoroutine(s.Invoke(manaUsed, this));
        st = str;

    }
    private void Start()
    {
        
    }
    public void showSpell(int radius, Animation a)
    {
        if(a != null && TryGetComponent<Animation>(out Animation anim))
        {

        }
        GetComponent<CircleCollider2D>().radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<EntityClass>(out EntityClass ec) && !(collision.isTrigger))
        {
            inside.Add(collision);
            //Debug.Log(collision.name);
        }
        
        //Debug.Log(collision.name);

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
        {
            inside.Remove(other);
            
        }
       
        //Debug.Log(other.name + "exit:'(");

    }
}
