using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public PlayerClass pc;
    public delegate IEnumerator spell(int manaUsed);
    public List<spell> allSpells = new List<spell>();
    public void createSpell(int index)
    {

    }

    public void showSpell(int radius, Animation a)
    {
        if(a != null && TryGetComponent<Animation>(out Animation anim))
        {

        }
        GetComponent<CircleCollider2D>().radius = radius;
    }

    // Start is called before the first frame update
    void Start()
    {
        allSpells.Add(lightning);
    }

    public IEnumerator lightning(int manaUsed)
    {
        yield return new WaitForSeconds(2f);
        showSpell(3, null);
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] cs = new Collider2D[50];
        GetComponent<Rigidbody2D>().GetContacts(cs);
        foreach(Collider2D c in cs)
        {
            if(c.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
            {
                ec.getHit((int).5 * manaUsed, "lightning");
            }

        }
    }
}
