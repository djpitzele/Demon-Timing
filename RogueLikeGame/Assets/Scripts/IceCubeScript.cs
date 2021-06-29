using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCubeScript : MonoBehaviour, EntityClass
{
    public float maxSpeed = 60f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    public void getHit(float dmg, string type)
    {

    }

    public void die()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!(collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass pc)) && rb.velocity.magnitude > 3)
        {
            if(collision.gameObject.TryGetComponent<EntityClass>(out EntityClass ec))
            {
                ec.getHit(20, "ice");
            }
            die();
            Debug.Log("the if procced");
        }
        Debug.Log(collision.gameObject.name + rb.velocity.magnitude);
    }

    public GameObject ecgetObject()
    {
        return this.gameObject;
    }

    public void setPlayer(GameObject p)
    {

    }
}
