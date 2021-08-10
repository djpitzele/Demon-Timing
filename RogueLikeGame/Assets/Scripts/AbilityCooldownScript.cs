using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownScript : MonoBehaviour
{
    private float elapsed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            updateAbilityCooldown();
        }
    }
    public void updateAbilityCooldown()
    {
        GetComponent<Text>().text = ((int)PlayerClass.main.abilityCooldown).ToString();
    }
}
