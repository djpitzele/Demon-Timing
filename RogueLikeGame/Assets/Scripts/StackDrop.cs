using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackDrop : MonoBehaviour
{
    public int min;
    public int max;
    public int value;
    public System.Random r;
    public PlayerClass player;
    // Start is called before the first frame update
    void Start()
    {
        r = new System.Random();
        value = r.Next(min, max);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass pc) && gameObject.CompareTag("GoldStack"))
        {
            player = pc;
            pc.theCanvas.transform.Find("Gold").gameObject.GetComponent<UnityEngine.UI.Text>().enabled = true;
            pc.theCanvas.GetComponentsInChildren<ShadeScript>()[0].DisableText();
            pc.gold += value;
            pc.theCanvas.GetComponentsInChildren<GoldUI>()[0].Invoke("DisableGold", 3f);
            Destroy(this.gameObject);
        }
    }
    private void DisableGold()
    {
        player.theCanvas.transform.Find("Gold").gameObject.GetComponent<UnityEngine.UI.Text>().enabled = false;
    }
}
