using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arachnus : MonoBehaviour, MeleeClass
{
    private int curMana;
    private int maxMana = 10;
    private float curHP;
    private float maxHP = 10;
    private float dmg;
    private float tilAttack = 0;
    private GameObject player;
    /*public SoldierCommander(int theMaxMana, float theMaxHP)
    {
        maxMana = theMaxMana;
        
        maxHP = theMaxHP;
        
    }*/
    // Start is called before the first frame update
    public void setFacing(int n)
    {

    }
    void Start()
    {
        Rigidbody2D r = this.gameObject.GetComponent<Rigidbody2D>();
        //r.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        dmg = 10;
        curMana = maxMana;
        curHP = maxHP;
        //Debug.Log(curHP);
    }
    public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.TryGetComponent(out PlayerClass ec) && tilAttack <= 0)
        {
            ec.getHit(dmg, "melee");
            c.gameObject.GetComponent<Rigidbody2D>().AddForce((c.gameObject.transform.position - transform.position) * 100, ForceMode2D.Force);
            c.gameObject.GetComponent<MovementScript>().timeTilmovement += .2f;
            tilAttack = 1;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (tilAttack > 0)
        {
            tilAttack -= Time.deltaTime;
        }
    }

    public void getHit(float dm, string typeHit)
    {
        Debug.Log("soldier got hit for " + dm + typeHit + "--" + curHP);
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
}
