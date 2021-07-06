using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCheck : MonoBehaviour
{
    public void updateHealth(PlayerClass pc)
    {
        GetComponent<Text>().text = pc.curHP.ToString();
        if (pc.curHP <= 0)
        {
            transform.parent.GetChild(1).GetComponent<Image>().enabled = true;
        }
    }
}
