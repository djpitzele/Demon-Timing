using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeScript : MonoBehaviour
{
    public Transform st;
    public PlayerClass pc;
    public int tempShade;
    public int[,] prices = new int[5, 3] { { 1 , -1, -1} , { 5, 5, 5 }, { 15, 15, 15 }, { 45, 45, 45 }, { 135, 135, 135 } };
    void Start()
    {
        //prices[0] = { { 1 } };//, { 5, 5, 5 }, { 15, 15, 15 }, { 45, 45, 45 }, { 135, 135, 135 } };
        tempShade = PermVar.current.Shade + PlayerClass.main.curShade;
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
    }

    public bool possible(Vector2Int pos)
    {
        if(tempShade >= prices[pos.x, pos.y] && SaveGame.current.choices[pos.x - 1][pos.y] && !(SaveGame.current.choices[pos.x][pos.y]))
        {
            tempShade -= prices[pos.x, pos.y];
            return true;
        }
        return false;
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
            /*for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }*/
            SaveGame.current.choices[0][0] = true;
        }
    }
    public void MeleeAbilityClick()
    {
        Vector2Int v = new Vector2Int(1, 0);
        if (possible(v))
        {
            updateColors(transform.Find("Melee Ability"), transform.Find("Melee 1"));
            pc.playerAbility = MeleeAbility;
            //st.Find("Melee 1").gameObject.SetActive(true);
            SaveGame.current.choices[1][0] = true;
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
    {//njot finished
        Vector2Int v = new Vector2Int(3, 0);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
            pc.sm.totalAttackCooldown *= 0.8f;
        }
    }
    public void Melee3()
    {//njot finished
        Vector2Int v = new Vector2Int(4, 0);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
            pc.sm.kb *= 2f;
        }
    }
    public void SpeedAbilityClick()
    {
        Vector2Int v = new Vector2Int(1, 1);
        if (possible(v))
        {
            updateColors(transform.Find("Speed Ability"), transform.Find("Speed 1"));
            pc.playerAbility = SpeedAbility;
            //st.Find("Speed Ability").Find("Speed 1").gameObject.SetActive(true);
            SaveGame.current.choices[1][1] = true;
        }
    }
    public IEnumerator SpeedAbility()
    {
        pc.GetComponent<MovementScript>().dash();
        yield return null;
    }
    public void Speed1()
    {//njot finished
        Vector2Int v = new Vector2Int(2, 1);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
        }
    }
    public void Speed2()
    {//njot finished
        Vector2Int v = new Vector2Int(3, 1);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
        }
    }
    public void Speed3()
    {//njot finished
        Vector2Int v = new Vector2Int(4, 1);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
        }
    }
    public void ManaAbilityClick()
    {
        Vector2Int v = new Vector2Int(1, 2);
        if (possible(v))
        {
            updateColors(transform.Find("Mana Ability"), transform.Find("Mana 1"));
            pc.playerAbility = ManaAbility;
            //skill1.Find("Mana Ability").Find("Mana 1").gameObject.SetActive(true);
            SaveGame.current.choices[1][2] = true;
        }
    }
    public IEnumerator ManaAbility()
    {
        pc.curMana = pc.maxMana;
        pc.abilityCooldown = 20f;
        yield return null;
    }
    public void Mana1()
    {//njot finished
        Vector2Int v = new Vector2Int(2, 2);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
        }
    }
    public void Mana2()
    {//njot finished
        Vector2Int v = new Vector2Int(3, 2);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
        }
    }
    public void Mana3()
    {//njot finished
        Vector2Int v = new Vector2Int(4, 2);
        if (possible(v))
        {
            updateColors(transform.Find(makeString(v)), transform.Find(makeString(new Vector2Int(v.x + 1, v.y))));
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
    
