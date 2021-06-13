using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//TOOK OUT MonoBehaviour from extended, if something breaks this might be it
public class PlayerClass : MonoBehaviour, EntityClass
{
    public Sprite theSoldier;
    private int curMana;
    private int maxMana;
    private float curHP;
    private float maxHP;
    private float dmg;
    public PlayerClass(int theMaxMana, float theMaxHP)
    {
        maxMana = theMaxMana;
        curMana = maxMana;
        maxHP = theMaxHP;
        curHP = maxHP;
    }
    public void getHit(float dmg, string typeHit)
    {
        Debug.Log("we got hit for " + dmg);
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
    /*public void melee()
    {
        EntityClass[] GOs = new EntityClass[GameObject.FindObjectsOfType(typeof(EntityClass)).Length];
        UnityEngine.Object[] objs = GameObject.FindObjectsOfType(typeof(EntityClass));
        for (int i = 0; i < GOs.Length; i++)
        {
            GOs[i] = (EntityClass)objs[i];
        }
        float xCoord = this.getX();
        float yCoord = this.getY();
        foreach (EntityClass e in GOs)
        {
            if(dist(xCoord, e.getX(), yCoord, e.getY()) <= 30)
            {
                e.getHit(dmg, "melee");
            }
        }
    }*/
    /*public void OnCollisionEnter2D(Collision2D c)
    {
        getHit(1, "melee");
    }*/
    public float dist(double x1, double y1, double x2, double y2)
    {
        return Convert.ToSingle(Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));
    }

    public float getDmg()
    {
        return dmg;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = new Vector3(6, 5, 0);
        GameObject.Find("Spawner").GetComponent<EnemySpawner>().spawnEnemy(pos, typeof(SoldierCommander), theSoldier);
    }
    
    // Update is called once per frame
    void Update()
    {
        //DIE SOMETIME
        //Debug.Log(this.getCurHP());
    }
}
