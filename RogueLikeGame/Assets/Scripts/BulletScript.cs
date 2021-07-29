using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour, EntityClass
{
    public float dmg;
    public Vector3 targetTransform;
    public void getHit(float dmg, string type)
    {
        die();
    }
    public void setSpeed(float speed)
    {
        GetComponent<Rigidbody2D>().mass *= (1f / speed);
    }
    public void die()
    {
        Destroy(this.gameObject);
    }
    public float checkPlayer(Collider2D collider)
    {
        if(collider.TryGetComponent<PlayerClass>(out PlayerClass pc))
        {
            return 1;
        }
        return 0;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.transform != transform.parent && collision.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
        {
            Debug.Log(collision.gameObject.name + "---" + this.gameObject.layer + (collision.transform != transform.parent));
            ec.getHit(dmg * checkPlayer(collision), "projectile");
            die();
        }
        else if(collision.transform != transform.parent)
        {
            Debug.Log("hitwall" + collision.gameObject.name);
            die();
        }
       
    }

    //public GameObject myPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 vectorToTarget = targetTransform - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = q;
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
