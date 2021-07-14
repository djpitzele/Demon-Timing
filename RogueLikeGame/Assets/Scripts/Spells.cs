using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public PlayerClass pc;
    public delegate IEnumerator spell(int manaUsed);
    public List<spell> allSpells = new List<spell>();
    public void createSpell(int index, int manaUsed)
    {
        Debug.Log(index + "nuts");
        StartCoroutine(lightning(manaUsed));
        
    }

    public void showSpell(int radius, Animation a)
    {
        if(a != null && TryGetComponent<Animation>(out Animation anim))
        {

        }
        GetComponent<CircleCollider2D>().radius = radius;
    }

    // Start is called before the first frame update
    public void Start2()
    {
        
        allSpells.Add(lightning);
        Debug.Log("balls" + allSpells.Count);
    }

    public IEnumerator lightning(int manaUsed)
    {
        Debug.Log("deez?");
        yield return new WaitForSeconds(2f);
        Debug.Log("deez!")
        ; showSpell(3, null);
        //spells are not in right spot
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] cs = new Collider2D[50];
        GetComponent<Rigidbody2D>().GetContacts(cs);
        foreach(Collider2D c in cs)
        {
            //not doing dmaage
            if(c.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
            {
                ec.getHit((int).5 * manaUsed, "lightning");
            }

        }
        Destroy(this.gameObject);
    }
    public IEnumerator nothing(int manaUsed)
    {
        yield return new WaitForSeconds(1f);
    }
}
