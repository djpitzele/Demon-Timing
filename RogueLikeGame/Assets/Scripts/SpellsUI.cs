using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpellsUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateSpells(PlayerClass pc)
    {
        int j = 1;
        Image[] imgs = GetComponentsInChildren<Image>();
        //Debug.Log(imgs.Length);
        foreach(int i in pc.spells)
        {
            if(i != 0)
            {
                imgs[j].enabled = true;
                imgs[j].sprite = SpellTracker.main.spellSprites[i];
                j++;
            }
            else
            {
                imgs[j].enabled = false;
                j++;
            }
           
        }
    }
}
