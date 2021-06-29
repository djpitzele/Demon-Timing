using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour, EntityClass
{
    public RedDragonScript dragonScript;
    float hp = 60;
    bool fireon = false;
    //index 0 is the fire, index 1 is itself
    BoxCollider2D[] boxCollider2Ds;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2Ds = GetComponents<BoxCollider2D>();
        boxCollider2Ds[0].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
    public void setPlayer(GameObject player)
    {

    }
    public GameObject ecgetObject()
    {
        return this.gameObject;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(TryGetComponent<PlayerClass>(out PlayerClass pc) && fireon)
        {
            pc.getHit(20 ,"fire");
        }
    }
    public void Fire()
    {
       fireon = true;
        Invoke("DisableFire", 3f );
    }
    public void DisableFire()
    {
        fireon = false;
    }

}
