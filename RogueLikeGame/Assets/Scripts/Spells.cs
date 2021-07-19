using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public int manaUsed;
    public List<Collider2D> inside = new List<Collider2D>();

    public void createSpell(SpellTracker.spell s, int manaUsed)
    {
        StartCoroutine(s.Invoke(manaUsed, this));
    }
    private void Start()
    {
        //Debug.Log("deezClicked");
    }
    public void showSpell(int radius, int index)
    {
        if(index != null && TryGetComponent<Animator>(out Animator anim))
        {
            //anim.AddClip(a, "Spell");
            //anim["Spell"].layer = 123;

            anim.SetInteger("Index", index);
        }
        GetComponent<CircleCollider2D>().radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EntityClass>(out EntityClass ec) && !(collision.isTrigger))
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

        //  Debug.Log(other.name + "exit:'(");

    }
}
