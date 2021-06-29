using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

//TOOK OUT MonoBehaviour from extended, if something breaks this might be it
public class PlayerClass : MonoBehaviour, EntityClass
{
    public Sprite theSoldier;
    public GameObject spawner;
    public GameObject prefab;
    public int curMana;
    public int maxMana;
    public float curHP;
    public float maxHP;
    private float dmg;
    public int totalEnemies;
    public int totalkills;
    public RawImage HitScreen;
    public PlayerClass(int theMaxMana, float theMaxHP)
    {
        maxMana = theMaxMana;
        curMana = maxMana;
        maxHP = theMaxHP;
        curHP = maxHP;
    }
    public void getHit(float dm, string typeHit)
    {
        StartCoroutine("ActivateHit");
        if(GetComponent<MovementScript>().cooldown <= 0.95f)
        {
            curHP -= dm;
            Debug.Log("we got hit for " + dm + typeHit + curHP);
            if (curHP <= 0)
            {
                die();
            }
        }
    }
    private IEnumerator ActivateHit()
    {
        HitScreen.enabled = true;
        yield return new WaitForSecondsRealtime(.3f);
        HitScreen.enabled = false;
    }
    public void die()
    {
        Debug.Log("RIP us");
        //UI to die
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
    public GameObject ecgetObject()
    {
        return this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = new Vector3(6, 5, 0);
        //GameObject.Find("Spawner").GetComponent<EnemySpawner>().spawnEnemy(pos, typeof(SoldierCommander), theSoldier);
        //spawner.GetComponent<EnemySpawner>().spawnEnemy(prefab);
        Debug.Log("deez");
        dmg = 5;
        totalEnemies = 0;
        HitScreen.enabled = false;
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(HitScreen.gameObject.transform.parent);
    }
    
    // Update is called once per frame
    void Update()
    {
        //DIE SOMETIME
        //Debug.Log(this.getCurHP());
    }
     public void setPlayer(GameObject g)
    {
        Debug.Log("deez");
    }
}
