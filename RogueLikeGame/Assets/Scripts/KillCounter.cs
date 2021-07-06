using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this is not a kill counter, but a timer
public class KillCounter : MonoBehaviour
{
    public float timeSpent;

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime;
        GetComponent<Text>().text = timeSpent.ToString("F3");
    }
}
