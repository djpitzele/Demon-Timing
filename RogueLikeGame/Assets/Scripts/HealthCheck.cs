using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthCheck : MonoBehaviour
{
    public void updateHealth(PlayerClass pc)
    {
        transform.Find("HealthText").GetComponent<Text>().text = ((int)pc.curHP).ToString();
        transform.Find("HealthBar").GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (Convert.ToSingle(pc.curHP) / pc.maxHP) * 200);
        
    }
}
