using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCheck : MonoBehaviour
{
    public GameObject player;
    public GameObject deathscreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = player.GetComponent<PlayerClass>().curHP.ToString();
        if(player.GetComponent<PlayerClass>().curHP <= 0)
        {
            deathscreen.GetComponent<Image>().enabled = true;
        }
    }
}
