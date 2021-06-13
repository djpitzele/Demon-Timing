using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierCommander : MonoBehaviour, EntityClass
{
    private int curMana;
    private int maxMana;
    private float curHP;
    private float maxHP;
    int tilAttack = 0;
    public SoldierCommander(int theMaxMana, float theMaxHP)
    {
        maxMana = theMaxMana;
        curMana = maxMana;
        maxHP = theMaxHP;
        curHP = maxHP;
    }
    // Start is called before the first frame update
    void Start()   
    {
        Rigidbody2D r = this.gameObject.GetComponent<Rigidbody2D>();
        r.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }
    public void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject == GameObject.Find("MainChar") && tilAttack == 0)
        {
            c.gameObject.GetComponent<PlayerClass>().getHit(1, "melee");
            tilAttack = 60;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(tilAttack > 0)
        {
            tilAttack--;
        }
    }

    public void getHit(float dmg, string typeHit)
    {
        Debug.Log("soldier got hit for " + dmg);
        curHP -= dmg;
        if (curHP <= 0)
        {
            die();
        }
    }
    public void die()
    {
        Debug.Log("RIP bozo");
    }
}
