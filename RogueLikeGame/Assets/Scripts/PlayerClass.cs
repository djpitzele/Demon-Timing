using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]
//TOOK OUT MonoBehaviour from extended, if something breaks this might be it
public class PlayerClass : MonoBehaviour, EntityClass
{
    public float curMana;
    public float maxMana;
    public float maxHP;
    public float curHP;
    private float dmg;
    public int gold;
    public int totalEnemies;
    public int totalkills;
    public RawImage HitScreen;
    public List<int> orderScenes;
    public int curSceneIndex = 0;
    public System.Random r = new System.Random();
    public GameObject theCanvas;
    public GameObject pauseMenu;
    public int curShade;
    public bool dead = false;

    public void getHit(float dm, string typeHit)
    {
        if(!dead && GetComponent<MovementScript>().cooldown <= 0.95f)
        {
            StartCoroutine("ActivateHit");
            curHP -= dm;
            //Debug.Log("we got hit for " + dm + typeHit + curHP);
            if (curHP <= 0)
            {
                die();
            }
        }
        theCanvas.transform.GetChild(0).GetComponent<HealthCheck>().updateHealth(this);
    }

    public SaveGame makeSaveGame()
    {
        SaveGame s = new SaveGame();
        s.curHP = curHP;
        s.curMana = curMana;
        s.gold = gold;
        s.totalKills = totalkills;
        s.orderScenes = orderScenes;
        s.curSceneIndex = curSceneIndex;
        s.time = theCanvas.GetComponentsInChildren<KillCounter>()[0].timeSpent;
        s.curShade = curShade;
        return s;
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
        PermVar.current.Shade += curShade;
        curShade = 0;
        dead = true;
        Invoke("goToLobby", 3f);
        //UI to die
    }
    public void goToLobby()
    {
        pauseMenu.SetActive(true);
        theCanvas.transform.Find("Death").GetComponent<Image>().enabled = false;
        dead = false;
        theCanvas.GetComponentsInChildren<RestartScript>()[0].doClick();
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
        return dmg * (1 + PermVar.current.meleeBuff);
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
        //curHP = maxHP;
        if(curHP == 0)
        {
            curHP = maxHP;
        }
        //curMana = maxMana;
        if (curMana == 0)
        {
            curMana = maxMana;
        }
        dmg = 5;
        totalEnemies = 0;
        HitScreen = theCanvas.transform.GetChild(3).gameObject.GetComponent<RawImage>();
        theCanvas.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Gold: " + gold;
        HitScreen.enabled = false;
        pauseMenu = theCanvas.transform.Find("Pause Menu").gameObject;
        int numScenes = SceneManager.sceneCountInBuildSettings;
        List<int> remainingScenes = new List<int>();
        for(int i = 3; i < numScenes; i++)
        {
            remainingScenes.Add(i);
        }
        //Debug.Log(remainingScenes.Count + "-----------------");
        orderScenes = new List<int>();
        while(remainingScenes.Count > 0)
        {
            if(r.Next(1000) > 1000 * (remainingScenes.Count / (float)(numScenes - 3)))
            {
                break;
            }
            int sceneIndex = remainingScenes[r.Next(remainingScenes.Count)];
            orderScenes.Add(sceneIndex);
            remainingScenes.Remove(sceneIndex);
        }
        orderScenes.Add(2);
        theCanvas.transform.GetChild(0).GetComponent<Text>().text = curHP.ToString();
        
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(theCanvas);
        SceneManager.LoadScene(curSceneIndex);
        Debug.Log("deez2");
        //Debug.Log(theCanvas.GetComponentsInChildren<ShadeScript>()[0] == null);
        theCanvas.GetComponentsInChildren<ShadeScript>()[0].updateShadeLobby();
        //Debug.Log(orderScenes.Count);
    }
    
    public void nextScene()
    {
        //Debug.Log(orderScenes[1] + "nuts" + orderScenes[0]);
        curSceneIndex = orderScenes[0];
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
