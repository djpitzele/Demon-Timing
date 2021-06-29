using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour, EntityClass
{
    public RedDragonScript dragonScript;
    float hp = 60;
    bool fireon = false;
    public floorCreator floor;
    public GameObject fire;
    private int index;
    public GameObject[] flames = new GameObject[24];
    //index 0 is the fire, index 1 is itself
    BoxCollider2D[] boxCollider2Ds;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2Ds = GetComponents<BoxCollider2D>();
        for(int i = 1; i <= 24; i++)
        {
            GameObject f = Instantiate(fire, this.transform);
            f.SetActive(false);
            f.transform.position += new Vector3(-1 * i, 0, 0);
            flames[i - 1] = f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireon && index % 5 == 0)
        {
            flames[index / 5].SetActive(true);
            index++;
        }
        else if (fireon)
        {
            index++;
        }
    }
    public void getHit(float dm, string typeHit)
    {
        if(typeHit == "ice")
        {
            hp -= dm;
        }
        if(hp == 0)
        {
            die();
        }
    }
    public void die()
    {
        dragonScript.pipes.Remove(this);
        Debug.Log("good job");
        Destroy(this.gameObject);
        floor.waves -= 100;
    }
    public void setPlayer(GameObject player)
    {

    }
    public GameObject ecgetObject()
    {
        return this.gameObject;
    }
    public void Fire()
    {
       fireon = true;
        //transform.localScale *= 2;
        Invoke("DisableFire", 3f );
    }
    public void DisableFire()
    {
        fireon = false;
        index = 0;
        foreach(GameObject g in flames)
        {
            g.SetActive(false);
        }
    }

}
