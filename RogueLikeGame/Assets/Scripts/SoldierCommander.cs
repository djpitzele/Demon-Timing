using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Reflection;

public class SoldierCommander : MonoBehaviour, MeleeClass
{
    private float curHP;
    private float maxHP = 10;
    private float dmg;
    private float tilAttack = 0;
    //FOR ALL FACINGS: 1 = RIGHT, -1 = LEFT
    public int facing = 1;
    public GameObject player;
    public List<Collider2D> inside;
    /*public SoldierCommander(int theMaxMana, float theMaxHP)
    {
        maxMana = theMaxMana;
        
        maxHP = theMaxHP;
        
    }*/
    // Start is called before the first frame update
    void Start()   
    {
        Rigidbody2D r = this.gameObject.GetComponent<Rigidbody2D>();
        //r.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        dmg = 10;
        curHP = maxHP;
        //Debug.Log(curHP);
    }
    public void OnTriggerStay2D(Collider2D c)
    {
        //Debug.Log("still in collider");
        if (c.gameObject.TryGetComponent(out PlayerClass ec) && tilAttack <= 0)
        {
            StartCoroutine("Attack", ec);
            //Debug.Log("we h it");
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(tilAttack > 0)
        {
            tilAttack -= Time.deltaTime;
        }
        //Debug.Log(tilAttack);
    }
    public IEnumerator Attack(EntityClass ec)
    {
        Debug.Log("attacks");
        tilAttack = 1;
        GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(.2f);
        GetComponent<Animator>().ResetTrigger("Attack");
        if (inside.Contains(ec.ecgetObject().GetComponent<CapsuleCollider2D>()))
        {
            ec.getHit(dmg, "melee");
            ec.ecgetObject().GetComponent<Rigidbody2D>().AddForce((ec.ecgetObject().transform.position - transform.position) * 100, ForceMode2D.Force);
            ec.ecgetObject().GetComponent<MovementScript>().timeTilmovement += .2f;
        }
      
    }
    public void setFacing(int n)
    {
        if(facing == n)
        {
            return;
        }
        else
        {
            Vector3 curScale = transform.localScale;
            curScale.x *= -1;
            transform.localScale = curScale;
            facing = n;
        }
    }

    public void getHit(float dm, string typeHit)
    {
        //Debug.Log("soldier got hit for " + dm + typeHit + "--" + curHP);
        curHP -= dm;
        if (curHP <= 0)
        {
            die();
        }
    }
    public void die()
    {
        //Debug.Log("RIP bozo");
        player.GetComponent<PlayerClass>().totalEnemies--;
        player.GetComponent<PlayerClass>().totalkills++;
        if(TryGetComponent<ItemDropper>(out ItemDropper id))
        {
            id.spawnItem();
        }
        Destroy(this.gameObject);
    }
    public void setPlayer(GameObject go)
    {
        player = go;
    }
    public GameObject ecgetObject()
    {
        return this.gameObject;
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
