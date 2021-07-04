using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedDragonScript : MonoBehaviour
{
    public List<PipeScript> pipes = new List<PipeScript>();
    private float timeTilFire = 1;
    private System.Random r;
    public Sprite fired;
    public Sprite Notfired;
    public floorCreator floorScript;
    // Start is called before the first frame update
    void Start()
    {
        r = new System.Random();
        foreach(Object o in Object.FindObjectsOfType(typeof(PipeScript)))
        {
            pipes.Add((PipeScript)o);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pipes.Count == 0 || floorScript.waves <= 0)
        {
            //WE WON, DO SOMETHING COOL
            GameObject f = GameObject.Find("Canvas").transform.GetChild(5).gameObject;
            f.GetComponent<Image>().enabled = true;
            f.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = true;
            f.GetComponentsInChildren<Text>()[0].text = "GGs\n" + f.transform.parent.GetChild(2).GetComponent<KillCounter>().timeSpent.ToString();
            floorScript.waves = -1;
            GameObject[] gms = (GameObject[])FindObjectsOfType(typeof(GameObject));
            foreach (GameObject g in gms)
            {
                if (g.TryGetComponent<EntityClass>(out EntityClass ec))
                {
                    ec.die();
                }
            }
            Destroy(this.gameObject);
        }
        else if(timeTilFire <= 0)
        {
            timeTilFire = 8f + r.Next(5);
            pipes[r.Next(pipes.Count)].Fire();
            GetComponent<SpriteRenderer>().sprite = fired;
            Debug.Log("fire existed");
            Invoke("ResetFire", 3f);
        }
        else
        {
            timeTilFire -= Time.deltaTime;
        }
    }
    private void ResetFire()
    {
        GetComponent<SpriteRenderer>().sprite = Notfired;
    }
}
