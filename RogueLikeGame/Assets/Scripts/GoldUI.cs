using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    void DisableGold()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().enabled = false;
    }

    public void updateGold(PlayerClass pc)
    {
        transform.parent.Find("Shade").GetComponent<ShadeScript>().DisableText();
        gameObject.GetComponent<UnityEngine.UI.Text>().enabled = true;
        GetComponent<UnityEngine.UI.Text>().text = "Gold: " + pc.gold;
        Invoke("DisableGold", 3f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
