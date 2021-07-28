using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour, EntityClass
{
    public float dmg;
    public void getHit(float dmg, string type)
    {
        die();
    }
    public void setSpeed(float speed)
    {

    }
    public void die()
    {
        Destroy(this.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + "---" + this.gameObject.layer);
        if(collision.transform == transform.parent)
        {
            return;
        }
        if(collision.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
        {
            ec.getHit(dmg, "projectile");
        }
        die();
    }

    //public GameObject myPlayer;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void setPlayer(GameObject g)
    {

    }

    public GameObject ecgetObject()
    {
        return this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
