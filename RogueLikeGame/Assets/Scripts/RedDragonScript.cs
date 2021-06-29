using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDragonScript : MonoBehaviour
{
    public List<PipeScript> pipes = new List<PipeScript>();
    private float timeTilFire = 1;
    private System.Random r;
    public Sprite fired;
    public Sprite Notfired;
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
        if (pipes.Count == 0)
        {
            //WE WON, DO SOMETHING COOL
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
