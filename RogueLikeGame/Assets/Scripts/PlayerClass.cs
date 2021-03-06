using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
[System.Serializable]
//TOOK OUT MonoBehaviour from extended, if something breaks this might be it
public class PlayerClass : MonoBehaviour, EntityClass
{
    public static PlayerClass main;
    public bool copy;
    public float curMana2;
    public float maxMana;
    public float maxHP2;
    public float maxHP
    {
        get { return maxHP2; }
        set { maxHP2 = value; Debug.Log("maxHP changed to " + value); }
    }
    public float curHP2;
    public float dmg;
    public int gold2;
    public int totalEnemies;
    public int totalkills;
    public RawImage HitScreen;
    public List<int> orderScenes;
    public int curSceneIndex = 0;
    public System.Random r = new System.Random();
    public GameObject theCanvas;
    public GameObject pauseMenu;
    public GameObject skillTreeMenu;
    public int curShade;
    public bool dead = false;
    public int[] spells2 = new int[3];
    public bool menuOn = false;
    public MovementScript ms;
    public int firstNormalIndex = 7;
    public int lastNormalIndex = 17;//does not include hard rooms
    private int[] bossSceneIndex = { 3, 2, 4 }; // put boss room scene indicies in here
    public delegate IEnumerator ability();
    public ability playerAbility;
    public bool haveAbility = false;
    public float abilityCooldown = 0f;
    public float enemyPerception = 1f;
    public swordMovement sm;
    public int spentShadeSpell2 = 0;
    public int spentShadeSpell 
    {
        get { return spentShadeSpell2; }
        set { spentShadeSpell2 = value; ShadeScript.sh.updateTempShade(); }
    }
    public int[] spells
    {
        get { return spells2; }
        set { spells2 = value; theCanvas.transform.Find("Spells").GetComponent<SpellsUI>().UpdateSpells(this); }
    }
    public BuyMenuScript bms;
    public int gold
    {
        get { return gold2; }
        set { gold2 = value; theCanvas.transform.Find("Gold").GetComponent<GoldUI>().updateGold(this); }
    }
    public float curMana
    {
        get{return curMana2;}
        set { curMana2 = Math.Min(maxMana, Math.Max(value, 0)); theCanvas.transform.Find("Mana").GetComponent<ManaScript>().updateMana(this);  }
    }
    public float curHP 
    {
        get { return curHP2; }
        set { curHP2 = value; theCanvas.transform.Find("Health").GetComponent<HealthCheck>().updateHealth(this); }
    }

    public void getHit(float dm, string typeHit)
    {
        if(!dead && GetComponent<MovementScript>().cooldown <= 0.95f)
        {
            StartCoroutine("ActivateHit");
            curHP -= dm;
            if (curHP2 <= 0) { die(); }
            //Debug.Log("we got hit for " + dm + typeHit + curHP);
        }
        else
        {
            //Debug.Log("avoidedhit");
        }
        theCanvas.transform.Find("Health").GetComponent<HealthCheck>().updateHealth(this);
       // Debug.Log(curHP);
    }
    public void setSpeed(float speed)
    {
        ms.speed *= speed;
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
        s.spells = spells;
        s.spentShadeSpell = spentShadeSpell;
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
        //Debug.Log("RIP us");
        PermVar.current.Shade += curShade;
        curShade = 0;
        theCanvas.transform.Find("Death").GetComponent<Image>().enabled = true;
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
        return dmg;
    }
    public GameObject ecgetObject()
    {
        return this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!copy)
        {
            //GameObject.Find("Spawner").GetComponent<EnemySpawner>().spawnEnemy(pos, typeof(SoldierCommander), theSoldier);
            //spawner.GetComponent<EnemySpawner>().spawnEnemy(prefab);
            Debug.Log("deez");

            main = this;
            //PlayerClass.applyEffects(PermVar.current.choices, SkillTreeScript.sts.effects);
            //curHP = maxHP;
            if (curHP <= 0)
            {
                curHP = maxHP;
            }
            //curMana = maxMana;
            if (curMana <= 0)
            {
                curMana = maxMana;
            }
            dmg = 5;
            ms = GetComponent<MovementScript>();
            totalEnemies = 0;
            HitScreen = theCanvas.transform.Find("RedHit").gameObject.GetComponent<RawImage>();
            theCanvas.transform.Find("Gold").gameObject.GetComponent<Text>().text = "Gold: " + gold;
            theCanvas.transform.Find("Mana").GetComponent<ManaScript>().updateMana(this);
            sm = transform.GetComponentsInChildren<swordMovement>()[0];
            skillTreeMenu = theCanvas.transform.Find("Skill Tree").gameObject;
            SkillTreeScript.sts.pc = this;
            skillTreeMenu.SetActive(false);
            HitScreen.enabled = false;
            pauseMenu = theCanvas.transform.Find("Pause Menu").gameObject;
            int numScenes = SceneManager.sceneCountInBuildSettings;
            List<int> remainingScenes = new List<int>();
            List<int> totalRemainingScenes = new List<int>();
            //CHANGE WHEN NEW ROOM: int i = ?
            for (int i = firstNormalIndex; i <= lastNormalIndex; i++)
            {
                totalRemainingScenes.Add(i);
                remainingScenes.Add(i);
            }
            for (int i = lastNormalIndex + 1; i < numScenes; i++)
            {
                totalRemainingScenes.Add(i);
            }
            orderScenes = new List<int>();
            /* while(remainingScenes.Count > 0)
             {
                 //CHANGE WHEN NEW ROOM: int i = ?
                 if (r.Next(1000) > 1000 * (remainingScenes.Count / (float)(numScenes - 5)))
                 {
                     break;
                 }
                 int sceneIndex = remainingScenes[r.Next(remainingScenes.Count)];
                 orderScenes.Add(sceneIndex);
                 remainingScenes.Remove(sceneIndex);
             }*/
            int length1 = 4 + r.Next(6);
            for (int i = 0; i < length1; i++)
            {
                //   Debug.Log(remainingScenes.Count);
                int sceneIndex = remainingScenes[r.Next(remainingScenes.Count)];
                orderScenes.Add(sceneIndex);
                remainingScenes.Remove(sceneIndex);
            }
            orderScenes.Add(bossSceneIndex[0]);
            orderScenes.Insert(orderScenes.Count / 2, npcRoom());
            remainingScenes.Clear();
            foreach (int i in totalRemainingScenes)
            {
                remainingScenes.Add(i);
            }
            int length2 = 5 + r.Next(6);
            for (int i = 0; i < length2; i++)
            {
                //   Debug.Log(remainingScenes.Count);
                int sceneIndex = remainingScenes[r.Next(remainingScenes.Count)];
                orderScenes.Add(sceneIndex);
                remainingScenes.Remove(sceneIndex);
            }
            orderScenes.Add(bossSceneIndex[1]);
            orderScenes.Insert(orderScenes.Count - (length2 / 2), npcRoom());
            foreach (int i in totalRemainingScenes)
            {
                remainingScenes.Add(i);
            }
            int length3 = 6 + r.Next(6);
            for (int i = 0; i < length3; i++)
            {
                //   Debug.Log(remainingScenes.Count);
                int sceneIndex = remainingScenes[r.Next(remainingScenes.Count)];
                orderScenes.Add(sceneIndex);
                remainingScenes.Remove(sceneIndex);
            }
            orderScenes.Add(bossSceneIndex[2]);
            orderScenes.Insert(orderScenes.Count - (length3 / 2), npcRoom());
            DontDestroyOnLoad(transform.gameObject);
            DontDestroyOnLoad(theCanvas);
            SceneManager.LoadScene(curSceneIndex);
            string s = "";
            foreach(int i in orderScenes)
            {
                s += (i + " ");
            }
            //Debug.Log(s);
            Debug.Log("deez2");
            //Debug.Log(theCanvas.GetComponentsInChildren<ShadeScript>()[0] == null);
            theCanvas.GetComponentsInChildren<ShadeScript>()[0].updateShadeLobby();
            //Debug.Log(orderScenes.Count);
        }
    }
    
    public void nextScene()
    {
        //Debug.Log(orderScenes[1] + "nuts" + orderScenes[0]);
        sm.sheithe();
        sm.attackTime = -10f;
        abilityCooldown = 0f;
        if(SceneManager.GetActiveScene().name == "Lobby")
        {
            applyEffects(PermVar.current.choices, SkillTreeScript.sts.effects);
        }
        curSceneIndex = orderScenes[0];
        SceneManager.LoadScene(orderScenes[0]);
        orderScenes.RemoveAt(0);
        
        //Debug.Log(orderScendqes.Count+"nutslength");
    }
    public static void applyEffects(bool[,] bs, UnityEngine.Events.UnityAction[,] uas)
    {
        bool haveDoneSkill1 = false;
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(bs[i, j])
                {
                    if (i == 0 && !(haveDoneSkill1))
                    {
                        uas[i, j].Invoke();
                        haveDoneSkill1 = true;
                    }
                    else if (i != 0)
                    {
                        uas[i, j].Invoke();
                    }
                }
            }
        }
    }
    public int npcRoom()
    {
        return r.Next(5, 7);//add npc rooms
    }
    // Update is called once per frame
    void Update()
    {
        if(abilityCooldown > 0)
        {
            abilityCooldown -= Time.deltaTime;
        }
        //DIE SOMETIME
        //Debug.Log(this.getCurHP());
    }

    public void updateChoices()
    {
        bool[,] choices = PermVar.current.choices;
    }

     public void setPlayer(GameObject g)
    {
        Debug.Log("deez");
    }
    private IEnumerator ResetColor(SpriteRenderer sr)
    {
        sr.color = new Color(1, .5f, .5f, 1);
        yield return new WaitForSeconds(.5f);
        sr.color = Color.white;
    }
    public void bossroomdoor()
    {
        if (orderScenes.Count > 0 && (orderScenes[0] == bossSceneIndex[0] || orderScenes[0] == bossSceneIndex[1] || orderScenes[0] == bossSceneIndex[2]))
        {
            DoorOut.main.bossdoor();
        }
    }
}
