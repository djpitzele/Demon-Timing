using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public int gold;
    public int totalEnemies;
    public int totalkills;
    public RawImage HitScreen;
    public List<int> orderScenes;
    public System.Random r;
    public GameObject theCanvas;

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
        //GameObject.Find("Spawner").GetComponent<EnemySpawner>().spawnEnemy(pos, typeof(SoldierCommander), theSoldier);
        //spawner.GetComponent<EnemySpawner>().spawnEnemy(prefab);
        Debug.Log("deez");
        r = new System.Random();
        dmg = 5;
        totalEnemies = 0;
        HitScreen.enabled = false;
        int numScenes = SceneManager.sceneCountInBuildSettings;
        List<int> remainingScenes = new List<int>();
        for(int i = 2; i < numScenes; i++)
        {
            remainingScenes.Add(i);
        }
        Debug.Log(remainingScenes.Count + "-----------------");
        orderScenes = new List<int>();
        while(remainingScenes.Count > 0)
        {
            if(r.Next(1000) > 1000 * (remainingScenes.Count / (float)(numScenes - 2)))
            {
                break;
            }
            int sceneIndex = remainingScenes[r.Next(remainingScenes.Count)];
            orderScenes.Add(sceneIndex);
            remainingScenes.Remove(sceneIndex);
        }
        orderScenes.Add(1);
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(HitScreen.gameObject.transform.parent);
        Debug.Log(orderScenes.Count);
    }
    
    public void nextScene()
    {
        //Debug.Log(orderScenes[1] + "nuts" + orderScenes[0]);
        SceneManager.LoadScene(orderScenes[0]);
        orderScenes.RemoveAt(0);
        //Debug.Log(orderScendqes.Count+"nutslength");
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
