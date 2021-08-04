using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeScript : MonoBehaviour
{
    public Transform st;
    public PlayerClass pc;
    void Start()
    {
        st = transform;
        transform.Find("Skill 1").GetComponent<Button>().onClick.AddListener(Skill1);
        transform.Find("Skill 1").Find("Melee Ability").GetComponent<Button>().onClick.AddListener(MeleeAbilityClick);
    }
    public void Skill1()
    {
        pc.dmg *= 1.05f;
        SpellTracker.main.spellDmg *= 1.05f;
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        SaveGame.current.st.choices[0] = 1;
    }
    public void MeleeAbilityClick()
    {
        pc.playerAbility = MeleeAbility;
        //st.Find("Melee 1").gameObject.SetActive(true);
        SaveGame.current.st.choices[1] = 0;
    }
    public IEnumerator MeleeAbility()
    {
        pc.abilityCooldown = 25f;
        pc.dmg *= 2;
        yield return new WaitForSeconds(5f);
        pc.dmg *= 0.5f;
    }
    public void SpeedAbilityClick()
    {
        pc.playerAbility = SpeedAbility;
        //st.Find("Speed Ability").Find("Speed 1").gameObject.SetActive(true);
        SaveGame.current.st.choices[1] = 1;
    }
    public IEnumerator SpeedAbility()
    {
        pc.GetComponent<MovementScript>().dash();
        yield return null;
    }
    public void ManaAbilityClick()
    {
        pc.playerAbility = ManaAbility;
        //skill1.Find("Mana Ability").Find("Mana 1").gameObject.SetActive(true);
        SaveGame.current.st.choices[1] = 2;
    }
    public IEnumerator ManaAbility()
    {
        pc.curMana = pc.maxMana;
        pc.abilityCooldown = 20f;
        yield return null;
    }
}
