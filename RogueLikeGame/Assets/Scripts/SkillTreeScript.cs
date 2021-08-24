using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class SkillTreeScript : MonoBehaviour
{
    public static SkillTreeScript sts;
    public Transform st;
    public PlayerClass pc;
    public int tempShade;
    public int spentShade2;
    public GameObject resetButton;
    public bool[,] stuffs;
    public UnityAction[,] effects = new UnityAction[5,3]; 
    public int spentShade
    {
        get { return spentShade2; }
        set { spentShade2 = value; ShadeScript.sh.updateTempShade(); PermVar.current.spentShade = value; }
    }
    public int[,] prices = new int[5, 3] { { 1 , -1, -1} , { 5, 5, 5 }, { 15, 15, 15 }, { 45, 45, 45 }, { 135, 135, 135 } };
    void Awake()
    {
        effects = new UnityAction[5, 3] { { Skill1Effect, Skill1Effect, Skill1Effect }, { MeleeAbilityEffect, SpeedAbilityEffect, ManaAbilityEffect }, { Melee1Effect, Speed1Effect, Mana1Effect }, { Melee2Effect, Speed2Effect, Mana2Effect }, { Melee3Effect, Speed3Effect, Mana3Effect } };
        stuffs = PermVar.current.choices;
        sts = this;
        //resetButton = transform.parent.Find("Reset Skill Tree").gameObject;
        //prices[0] = { { 1 } };//, { 5, 5, 5 }, { 15, 15, 15 }, { 45, 45, 45 }, { 135, 135, 135 } };
        //tempShade = PermVar.current.Shade + PlayerClass.main.curShade;
        st = transform;
        transform.Find("Skill 1").GetComponent<Button>().onClick.AddListener(Skill1);
        transform.Find("Melee Ability").GetComponent<Button>().onClick.AddListener(MeleeAbilityClick);
        transform.Find("Melee 1").GetComponent<Button>().onClick.AddListener(Melee1);
        transform.Find("Melee 2").GetComponent<Button>().onClick.AddListener(Melee2);
        transform.Find("Melee 3").GetComponent<Button>().onClick.AddListener(Melee3);
        transform.Find("Speed Ability").GetComponent<Button>().onClick.AddListener(SpeedAbilityClick);
        transform.Find("Speed 1").GetComponent<Button>().onClick.AddListener(Speed1);
        transform.Find("Speed 2").GetComponent<Button>().onClick.AddListener(Speed2);
        transform.Find("Speed 3").GetComponent<Button>().onClick.AddListener(Speed3);
        transform.Find("Mana Ability").GetComponent<Button>().onClick.AddListener(ManaAbilityClick);
        transform.Find("Mana 1").GetComponent<Button>().onClick.AddListener(Mana1);
        transform.Find("Mana 2").GetComponent<Button>().onClick.AddListener(Mana2);
        transform.Find("Mana 3").GetComponent<Button>().onClick.AddListener(Mana3);
        //Debug.Log("this happened");
        resetButton = transform.parent.Find("Reset Skill Tree").gameObject;
        resetButton.SetActive(false);
        //Debug.Log("it was disabled");

        resetButton.GetComponent<Button>().onClick.AddListener(resetChoices);
    }
    private void Start()
    {
        pc = PlayerClass.main;
        tempShade = PermVar.current.Shade + SaveGame.current.curShade;
    }
    public void OnEnable()
    {
     
        try
        {
            if (!PermVar.current.choices[0, 0] && !PermVar.current.choices[0, 1] && !PermVar.current.choices[0, 2])
            {
                PermVar.current.choices = new bool[5, 3] { { false, false, false }, { false, false, false }, { false, false, false }, { false, false, false }, { false, false, false } };
            }
        }
        catch (NullReferenceException n)
        {
            PermVar.current.choices = new bool[5, 3] { { false, false, false }, { false, false, false }, { false, false, false }, { false, false, false }, { false, false, false } };
            //Debug.Log("caught error");
        }
        setColors(PermVar.current.choices);
        //pc = PlayerClass.main;
        resetButton.SetActive(true);
        //Debug.Log("happens" + resetButton == null);
    }

    public void setColors(bool[,] choices)
    {
        if(choices[0, 0] || choices[0, 1] || choices[0, 2])
        {
            updateColors(transform.Find("Skill 1"));
            if(choices[1, 0])
            {
                updateColors(transform.Find("Melee Ability"));
                for(int i = 2; i < 5; i++)
                {
                    if(choices[i, 0])
                    {
                        updateColors(transform.Find(makeString(new Vector2Int(i, 0))));
                    }
                }
            }
            else if (choices[1, 1])
            {
                updateColors(transform.Find("Speed Ability"));
                for (int i = 2; i < 5; i++)
                {
                    if (choices[i, 1])
                    {
                        updateColors(transform.Find(makeString(new Vector2Int(i, 1))));
                    }
                }
            }
            else if (choices[1, 2])
            {
                updateColors(transform.Find("Mana Ability"));
                for (int i = 2; i < 5; i++)
                {
                    if (choices[i, 2])
                    {
                        updateColors(transform.Find(makeString(new Vector2Int(i, 2))));
                    }
                }
            }
        }
    }
    public void OnDisable()
    {
        resetButton.SetActive(false);
    }
    public void resetChoices()
    {
        spentShade = 0;
        transform.parent.Find("Shade").Find("TempShade").GetComponent<Text>().enabled = false;
        PermVar.current.choices = new bool[5, 3] { { false, false, false }, { false, false, false }, { false, false, false }, { false, false, false }, { false, false, false } };
        Image[] imgs = GetComponentsInChildren<Image>();
        foreach(Image i in imgs)
        {
            i.color = Color.grey;
        }
        transform.Find("Skill 1").GetComponent<Image>().color = Color.white;
    }
    public bool possible(Vector2Int pos)
    {
        tempShade = PermVar.current.Shade + PlayerClass.main.curShade;
        if (tempShade - spentShade >= prices[pos.x, pos.y] && (pos.x == 0 || PermVar.current.choices[pos.x - 1, pos.y]) && !(PermVar.current.choices[pos.x, pos.y]))
        {
            spentShade += prices[pos.x, pos.y];
            //ShadeScript.sh.updateTempShade(tempShade - spentShade);
            PermVar.current.choices[pos.x, pos.y] = true;
            //Debug.Log("jeez" + true);
            return true;
        }
        //Debug.Log("jeez" + false);
        return false;
    }
    public void updateTemp()
    {
        tempShade = PermVar.current.Shade + PlayerClass.main.curShade;
        ShadeScript.sh.updateTempShade();
    }
    public void updateColors(Transform green)
    {
        green.GetComponent<Image>().color = Color.green;
    }
    public void updateColors(Transform green, Transform white)
    {
        green.GetComponent<Image>().color = Color.green;
        white.GetComponent<Image>().color = Color.white;
    }
    public void nothing()
    {


    }
    public void Skill1()
    {
        Vector2Int v = new Vector2Int(0, 0);
        if (possible(v))
        {
            updateColors(transform.Find("Skill 1"), transform.Find("Melee Ability"));
            updateColors(transform.Find("Skill 1"), transform.Find("Speed Ability"));
            updateColors(transform.Find("Skill 1"), transform.Find("Mana Ability"));
            
            //all 3 elements in the first row of choices are true for this purchase
            PermVar.current.choices[0, 1] = true;
            PermVar.current.choices[0, 2] = true;
            /*for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }*/
            //SaveGame.current.choices[0, 0] = true;
        }
    }
    public void Skill1Effect()
    {
        pc = PlayerClass.main;
        pc.maxHP += 25;
        pc.curHP += 25;
    }
    public void MeleeAbilityClick()
    {
        Vector2Int v = new Vector2Int(1, 0);
        if (possible(v))
        {
            updateColors(transform.Find("Melee Ability"), transform.Find("Melee 1"));

            PermVar.current.choices[0, 1] = false;
            PermVar.current.choices[0, 2] = false;
            transform.Find("Speed Ability").GetComponent<Image>().color = Color.gray;
            transform.Find("Mana Ability").GetComponent<Image>().color = Color.gray;
            //st.Find("Melee 1").gameObject.SetActive(true);
            //SaveGame.current.choices[1, 0] = true;
        }
    }
    public void MeleeAbilityEffect()
    {
        pc.haveAbility = true;
        pc.playerAbility = MeleeAbility;
    }
    public IEnumerator MeleeAbility()
    {
        pc.abilityCooldown = 25f;
        pc.dmg *= 2;
        yield return new WaitForSeconds(5f);
        pc.dmg *= 0.5f;
    }
    public void Melee1()
    {
        Vector2Int v = new Vector2Int(2, 0);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));

        }
    }
    public void Melee1Effect()
    {
        pc.dmg *= 1.1f;
    }
    public void Melee2()
    {
        Vector2Int v = new Vector2Int(3, 0);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
           
        }
    }
    public void Melee2Effect()
    {
        pc.sm.totalAttackCooldown *= 0.8f;
    }
    public void Melee3()
    {
        //Debug.Log("oh yeayuh");
        Vector2Int v = new Vector2Int(4, 0);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)));
            pc.sm.kb *= 2f;
        }
    }
    public void Melee3Effect()
    {
        pc.sm.kb *= 2f;
    }
    public void SpeedAbilityClick()
    {
        Vector2Int v = new Vector2Int(1, 1);
        if (possible(v))
        {
            updateColors(transform.Find("Speed Ability"), transform.Find("Speed 1"));
            //pc.playerAbility = SpeedAbility;
            
            PermVar.current.choices[0, 0] = false;
            PermVar.current.choices[0, 2] = false;
            transform.Find("Melee Ability").GetComponent<Image>().color = Color.gray;
            transform.Find("Mana Ability").GetComponent<Image>().color = Color.gray;
            //st.Find("Speed Ability").Find("Speed 1").gameObject.SetActive(true);
            PermVar.current.choices[1, 1] = true;
            //pc.haveAbility = true;
        }
    }
    public void SpeedAbilityEffect()
    {
        pc.ms.speedAbility = true;
    }
    /*public IEnumerator SpeedAbility()
    {
        Rigidbody2D rb = PlayerClass.main.GetComponent<Rigidbody2D>();
        Vector3 mousePos = Input.mousePosition;
        mousePos = (Vector2)Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log(mousePos.ToString() + rb.ToString() + pc.transform.position.ToString() + pc.ms.dashDistance);
        rb.MovePosition(Vector3.MoveTowards(pc.transform.position, mousePos, pc.ms.dashDistance));
        pc.ms.dash();
        pc.ms.hasSecondDash = true;
        pc.abilityCooldown = 5f;
        yield return new WaitForFixedUpdate();

    }*/
    public void Speed1()
    {
        Vector2Int v = new Vector2Int(2, 1);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
            
        }
    }
    public void Speed1Effect()
    {
        pc.setSpeed(1.2f);
    }
    public void Speed2()
    {
        Vector2Int v = new Vector2Int(3, 1);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
           
        }
    }
    public void Speed2Effect()
    {
        pc.ms.dashDistance *= 1.5f;
    }
    public void Speed3()
    {
        Vector2Int v = new Vector2Int(4, 1);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)));
            
        }
    }
    public void Speed3Effect()
    {
        pc.enemyPerception *= 2f;
    }
    public void ManaAbilityClick()
    {
        Vector2Int v = new Vector2Int(1, 2);
        if (possible(v))
        {
            updateColors(transform.Find("Mana Ability"), transform.Find("Mana 1"));
            transform.Find("Speed Ability").GetComponent<Image>().color = Color.gray;
            transform.Find("Melee Ability").GetComponent<Image>().color = Color.gray;
            PermVar.current.choices[0, 0] = false;
            PermVar.current.choices[0, 1] = false;
            //skill1.Find("Mana Ability").Find("Mana 1").gameObject.SetActive(true);
            PermVar.current.choices[1, 2] = true;
        }
    }
    public void ManaAbilityEffect()
    {
        pc.haveAbility = true;
        pc.playerAbility = ManaAbility;
    }
    public IEnumerator ManaAbility()
    {
        pc.curMana = pc.maxMana;
        pc.abilityCooldown = 20f;
        yield return null;
    }
    public void Mana1()
    {
        Vector2Int v = new Vector2Int(2, 2);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
           
        }
    }
    public void Mana1Effect()
    {
        pc.maxMana += 50;
        pc.curMana += 50;
    }
    public void Mana2()
    {
        Vector2Int v = new Vector2Int(3, 2);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
            pc.sm.manaRegen *= 1.5f;
        }
    }
    public void Mana2Effect()
    {
        pc.sm.manaRegen *= 1.5f;
    }
    public void Mana3()
    {//njot finished
        Vector2Int v = new Vector2Int(4, 2);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)));
           
        }
    }
    public void Mana3Effect()
    {
        pc.ms.manaEfficiency *= .8f;
    }
    public string firstWord(Vector2Int v)
    {
        if(v.x == 0)
        {
            return "Skill";
        }
        if (v.x >= 1)
        {
            if (v.y == 0)
            {
                return "Melee";
            }
            else if (v.y == 1)
            {
                return "Speed";
            }
            else if (v.y == 2)
            {
                return "Mana";
            }
        }
        Debug.Log("firstdeezbroke");
        return null;
    }
    public string lastWord(Vector2Int v)
    {
        if (v.x == 0)
        {
            return "1";
        }
        if (v.x == 1)
        {
            return "Ability";
        }
        if(v.x >= 2)
        {
            return (v.x - 1).ToString();
        }
        else
        {
            Debug.Log("firstdeezbroke");
            return null;
        }

    }
    public string makeString(Vector2Int v)
    {
        return (firstWord(v) + " " + lastWord(v));
    }
}
    
