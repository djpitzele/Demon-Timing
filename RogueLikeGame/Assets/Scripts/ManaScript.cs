using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ManaScript : MonoBehaviour
{
    public void updateMana(PlayerClass pc)
    {
        transform.Find("ManaText").GetComponent<Text>().text = pc.curMana.ToString();
        transform.Find("ManaBar").GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (Convert.ToSingle(pc.curMana) / pc.maxMana) * 200);
    }
}
