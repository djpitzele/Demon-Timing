using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardRoomScript : MonoBehaviour
{
    public GameObject spell;
    public int spellind =0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public  void spawnSpell()
    {
        if(spellind != 0)
        {
            GameObject g = Instantiate(spell);
            g.transform.position = new Vector3(Camera.main.gameObject.transform.position.x, Camera.main.gameObject.transform.position.y, 0);
            g.GetComponent<SpellItemScript>().spellIndex = spellind;
        }
    }
}
