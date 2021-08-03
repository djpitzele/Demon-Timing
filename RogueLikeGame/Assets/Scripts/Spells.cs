using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public int manaUsed;
    public List<Collider2D> inside = new List<Collider2D>();
    public delegate void UpdateFunction(int manaUsed, Spells s);
    public delegate IEnumerator ColliderFunction(int manaUsed, Spells s, Collider2D c);
    public UpdateFunction onUpdate2;
    public UpdateFunction onUpdate
    {
        get { return onUpdate2; }
        set { onUpdate2 = value;  runFunction[0] = true; }
    }
    public ColliderFunction onEnter2;
    public ColliderFunction onEnter
    {
        get { return onEnter2; }
        set { onEnter2 = value; runFunction[1] = true; }
    }
    public ColliderFunction onExit2;
    public ColliderFunction onExit
    {
        get { return onExit2; }
        set { onExit2 = value; runFunction[2] = true; }
    }
    public UpdateFunction onDestroy2;
    public UpdateFunction onDestroy
    {
        get { return onDestroy2; }
        set { onDestroy2 = value; runFunction[3] = true; }
    }
    public bool[] runFunction = { false, false, false, false };
    public void createSpell(SpellTracker.spell s, int manaUsed)
    {
        StartCoroutine(s.Invoke(manaUsed, this));
        this.manaUsed = manaUsed;   
    }
    void Start()
    {
        
    }
    public void showSpell(float radius, int index)
    {
        if(TryGetComponent<Animator>(out Animator anim))
        {
            //anim.AddClip(a, "Spell");
            //anim["Spell"].layer = 123;

            anim.SetInteger("Index", index);
        }
        GetComponent<CircleCollider2D>().radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EntityClass>(out EntityClass ec) && !(collision.isTrigger))
        {
            inside.Add(collision);
            if (runFunction[1])
            {
                StartCoroutine(onEnter.Invoke(manaUsed, this, collision));
            }
            //Debug.Log(collision.name);
        }
        //Debug.Log("firebutbetter" + collision.name);
        //Debug.Log(collision.name);

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<EntityClass>(out EntityClass ec) && !(other.isTrigger))
        {
            inside.Remove(other);
            if (runFunction[2])
            {
                StartCoroutine(onExit.Invoke(manaUsed, this, other));
            }
        }
        //  Debug.Log(other.name + "exit:'(");

    }
    public void Update()
    {
        if(runFunction[0])
        {
            onUpdate.Invoke(manaUsed, this);
        }
    }
}
