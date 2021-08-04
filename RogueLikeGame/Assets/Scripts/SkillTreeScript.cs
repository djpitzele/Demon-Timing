using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeScript : MonoBehaviour
{
    void Start()
    {
        transform.Find("Skill 1").GetComponent<Button>().onClick.AddListener(Skill1);
        transform.Find("Skill 1").Find("Melee Ability").GetComponent<Button>().onClick.AddListener(MeleeAbilityClick);
    }
    public void Skill1()
    {
        PlayerClass.main.dmg *= 1.05f;
        SpellTracker.main.spellDmg *= 1.05f;
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public void MeleeAbilityClick()
    {
        PlayerClass.main.playerAbility = MeleeAbility;
    }
    public IEnumerator MeleeAbility()
    {
        yield return null; //HELLO FINISH THIS
    }
}
