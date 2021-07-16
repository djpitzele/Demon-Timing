using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ManaUsageScript : MonoBehaviour
{
    public void updateMana(MovementScript ms, PlayerClass pc)
    {
        transform.Find("ManaUsage Text").GetComponent<Text>().enabled = true;
        transform.Find("ManaUsage Bar").GetComponent<Image>().enabled = true;
        transform.Find("ManaUsage Text").GetComponent<Text>().text = ms.manaUsed.ToString();
        transform.Find("ManaUsage Bar").GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (Convert.ToSingle(ms.manaUsed) / pc.maxMana) * 200);
        if (!IsInvoking("Disable"))
        {
            Invoke("Disable", 3f);
        }
    }
    public void Disable()
    {
        transform.Find("ManaUsage Text").GetComponent<Text>().enabled = false;
        transform.Find("ManaUsage Bar").GetComponent<Image>().enabled = false;
        
    }
    
    
}
