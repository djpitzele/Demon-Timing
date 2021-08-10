using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeScript : MonoBehaviour
{
    public static SkillTreeScript sts;
    public Transform st;
    public PlayerClass pc;
    public int tempShade;
    public int spentShade2;
    public GameObject resetButton;
    public int spentShade
    {
        get { return spentShade2; }
        set { spentShade2 = value; ShadeScript.sh.updateTempShade(tempShade - spentShade2); }
    }
    public int[,] prices = new int[5, 3] { { 1 , -1, -1} , { 5, 5, 5 }, { 15, 15, 15 }, { 45, 45, 45 }, { 135, 135, 135 } };
    void Awake()
    {
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
    public void OnEnable()
    {
        //pc = PlayerClass.main;
        resetButton.SetActive(true);
        //Debug.Log("happens" + resetButton == null);
    }
    public void OnDisable()
    {
        resetButton.SetActive(false);
    }
    public void resetChoices()
    {
        spentShade = 0;
        SaveGame.current.choices = new bool[5, 3] { { false, false, false }, { false, false, false }, { false, false, false }, { false, false, false }, { false, false, false } };
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
        if (tempShade - spentShade >= prices[pos.x, pos.y] && (pos.x == 0 || SaveGame.current.choices[pos.x - 1, pos.y]) && !(SaveGame.current.choices[pos.x, pos.y]))
        {
            spentShade += prices[pos.x, pos.y];
            //ShadeScript.sh.updateTempShade(tempShade - spentShade);
            SaveGame.current.choices[pos.x, pos.y] = true;
            //Debug.Log("jeez" + true);
            return true;
        }
        //Debug.Log("jeez" + false);
        return false;
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
    public void Skill1()
    {
        Vector2Int v = new Vector2Int(0, 0);
        if (possible(v))
        {
            updateColors(transform.Find("Skill 1"), transform.Find("Melee Ability"));
            updateColors(transform.Find("Skill 1"), transform.Find("Speed Ability"));
            updateColors(transform.Find("Skill 1"), transform.Find("Mana Ability"));
            pc.dmg *= 1.05f;
            SpellTracker.main.spellDmg *= 1.05f;
            //all 3 elements in the first row of choices are true for this purchase
            SaveGame.current.choices[0, 1] = true;
            SaveGame.current.choices[0, 2] = true;
            /*for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }*/
            //SaveGame.current.choices[0, 0] = true;
        }
    }
    public void MeleeAbilityClick()
    {
        Vector2Int v = new Vector2Int(1, 0);
        if (possible(v))
        {
            updateColors(transform.Find("Melee Ability"), transform.Find("Melee 1"));
            pc.playerAbility = MeleeAbility;
            SaveGame.current.choices[0, 1] = false;
            SaveGame.current.choices[0, 2] = false;
            pc.haveAbility = true;
            //st.Find("Melee 1").gameObject.SetActive(true);
            //SaveGame.current.choices[1, 0] = true;
            SaveGame.current.choices[1, 0] = false;
        }
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
            pc.dmg *= 1.1f;
        }
    }
    public void Melee2()
    {
        Vector2Int v = new Vector2Int(3, 0);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
            pc.sm.totalAttackCooldown *= 0.8f;
        }
    }
    public void Melee3()
    {
        Debug.Log("oh yeayuh");
        Vector2Int v = new Vector2Int(4, 0);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)));
            pc.sm.kb *= 2f;
        }
    }
    public void SpeedAbilityClick()
    {
        Vector2Int v = new Vector2Int(1, 1);
        if (possible(v))
        {
            updateColors(transform.Find("Speed Ability"), transform.Find("Speed 1"));
            //pc.playerAbility = SpeedAbility;
            pc.ms.speedAbility = true;
            SaveGame.current.choices[0, 0] = false;
            SaveGame.current.choices[0, 2] = false;
            //st.Find("Speed Ability").Find("Speed 1").gameObject.SetActive(true);
            SaveGame.current.choices[1, 1] = true;
            //pc.haveAbility = true;
        }
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
            pc.setSpeed(1.2f);
        }
    }
    public void Speed2()
    {
        Vector2Int v = new Vector2Int(3, 1);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
            pc.ms.dashDistance *= 1.5f;
        }
    }
    public void Speed3()
    {
        Vector2Int v = new Vector2Int(4, 1);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)));
            pc.enemyPerception *= 2f;
        }
    }
    public void ManaAbilityClick()
    {
        Vector2Int v = new Vector2Int(1, 2);
        if (possible(v))
        {
            updateColors(transform.Find("Mana Ability"), transform.Find("Mana 1"));
            pc.playerAbility = ManaAbility;
            SaveGame.current.choices[0, 0] = false;
            SaveGame.current.choices[0, 1] = false;
            //skill1.Find("Mana Ability").Find("Mana 1").gameObject.SetActive(true);
            SaveGame.current.choices[1, 2] = true;
            pc.haveAbility = true;
        }
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
            pc.maxMana += 50;
            pc.curMana += 50;
        }
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
    public void Mana3()
    {//njot finished
        Vector2Int v = new Vector2Int(4, 2);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)));
            pc.ms.manaEfficiency *= .8f;
        }
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
    
