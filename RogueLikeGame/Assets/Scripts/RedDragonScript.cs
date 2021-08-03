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
    public Sprite deadDragon;
    public Sprite goldenDoor;
    public GameObject door;
    public GameObject spellPrefab;
    // Start is called before the first frame update
    void Start()
    {
        r = new System.Random();
        foreach(Object o in Object.FindObjectsOfType(typeof(PipeScript)))
        {
            pipes.Add((PipeScript)o);
        }
        door = DoorOut.main.gameObject;
        door.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pipes.Count == 0 || floorScript.waves <= 0)
        {
            //WE WON, DO SOMETHING COOL
            Debug.Log("hello");
            floorScript.waves = -1;
            GameObject[] gms = (GameObject[])FindObjectsOfType(typeof(GameObject));
            pipes.Clear();
            foreach (GameObject g in gms)
            {
                if(g.TryGetComponent<PlayerClass>(out PlayerClass pc))
                {
                    continue;
                }
                else if (g.TryGetComponent<EntityClass>(out EntityClass ec))
                {
                    ec.die();
                }
                else if(g.TryGetComponent<PipeScript>(out PipeScript ps))
                {
                    Destroy(ps.gameObject);
                }
            }
            GameObject s = Instantiate(spellPrefab);
            s.transform.position = new Vector3(11.5f, 10, 0);
            s.GetComponent<SpellItemScript>().spellIndex = 5;
            //Destroy(this.gameObject);
            GetComponent<SpriteRenderer>().sprite = deadDragon;
            door.GetComponent<SpriteRenderer>().enabled = true;
            door.GetComponent<SpriteRenderer>().sprite = goldenDoor;
            Destroy(this);
        }
        else if(timeTilFire <= 0)
        {
            timeTilFire = 8f + r.Next(5);
            pipes[r.Next(pipes.Count)].Fire();
            GetComponent<SpriteRenderer>().sprite = fired;
            //Debug.Log("fire existed");
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
