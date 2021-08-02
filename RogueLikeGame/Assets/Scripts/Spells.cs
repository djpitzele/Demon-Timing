using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public int manaUsed;
    public List<Collider2D> inside = new List<Collider2D>();
    public delegate void UpdateFunction(int manaUsed, Spells s);
    public delegate IEnumerator ColliderFunction(int manaUsed, Spells s, Collider2D c);
    public UpdateFunction onUpdate;
    public ColliderFunction onEnter;
    public ColliderFunction onExit;
    public UpdateFunction onDestroy;
    public void createSpell(SpellTracker.spell s, int manaUsed)
    {
        StartCoroutine(s.Invoke(manaUsed, this));
        this.manaUsed = manaUsed;   
    }
    public  void EmptyUpdate(int manaUsed, Spells s)
    {

    }
    public IEnumerator ColliderEmpty(int manaUsed, Spells s, Collider2D c)
    {
        yield return null;
    }
    private void Start()
    {
        onUpdate = EmptyUpdate;
        onEnter = ColliderEmpty;
        onExit = ColliderEmpty;
        onDestroy = EmptyUpdate;
        //Debug.Log("deezClicked");
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
        if (collision.gameObject.TryGetComponent<EntityClass>(out EntityClass ec) && !(collision.isTrigger))
        {
            inside.Add(collision);
            //Debug.Log(collision.name);
        }
        if (onEnter != null)
        {
            StartCoroutine(onEnter.Invoke(manaUsed, this, collision));
        }
        //Debug.Log(collision.name);

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
        {
            inside.Remove(other);

        }
            StartCoroutine(onExit.Invoke(manaUsed, this, other));
        //  Debug.Log(other.name + "exit:'(");

    }
    public void Update()
    {
      onUpdate.Invoke(manaUsed, this);
       //Debug.Log(transform.position);
    }
}
