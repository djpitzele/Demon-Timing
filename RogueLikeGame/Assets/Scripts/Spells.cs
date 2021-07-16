using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public PlayerClass pc;
    public delegate IEnumerator spell(int manaUsed);
    public List<spell> allSpells = new List<spell>();
    public List<Collider2D> inside = new List<Collider2D>();
    public void createSpell(int index, int manaUsed)
    {
        //Debug.Log(index + "nuts");
        StartCoroutine(allSpells[index].Invoke(manaUsed));
        
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
        allSpells.Add(nothing);
        allSpells.Add(lightning);
        //Debug.Log("balls" + allSpells.Count);
    }

    public IEnumerator lightning(int manaUsed)
    {
        Vector3 mouse = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(mouse);
        //Debug.Log("deez?");
        yield return new WaitForSeconds(.2f);
        
        
        
        //Debug.Log("deez!");
        showSpell(3, null);
        //spells are not in right spot
        EntityClass[] hits = new EntityClass[inside.Count];
        int i = 0;
        //Debug.Log(Camera.main.ScreenToWorldPoint(mouse) + " " + mouse);
        foreach (Collider2D c in inside)
        {
            hits[i] = c.gameObject.GetComponent<EntityClass>();
            Debug.Log("boomed");
            i++;
            

        }
        foreach(EntityClass ec in hits)
        {
            if(ec != null)
            {
                ec.getHit((int)(.5 * manaUsed), "lightning");
            }
        }
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);

    }
    public IEnumerator nothing(int manaUsed)
    {
        yield return new WaitForSeconds(1f);
    }
    public IEnumerator meteor(int manaUsed)
    {
        Vector3 mouse = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(mouse);
        //Debug.Log("deez?");
        yield return new WaitForSeconds(.2f);



        //Debug.Log("deez!");
        showSpell(3, null);
        //spells are not in right spot
        EntityClass[] hits = new EntityClass[inside.Count];
        int i = 0;
        //Debug.Log(Camera.main.ScreenToWorldPoint(mouse) + " " + mouse);
        foreach (Collider2D c in inside)
        {
            hits[i] = c.gameObject.GetComponent<EntityClass>();
            Debug.Log("boomed");
            i++;


        }
        foreach (EntityClass ec in hits)
        {
            if (ec != null)
            {
                ec.getHit((int)(.5 * manaUsed), "lightning");
            }
        }
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<EntityClass>(out EntityClass ec) && !(collision.isTrigger))
        {
            inside.Add(collision);
            Debug.Log(collision.name);
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
