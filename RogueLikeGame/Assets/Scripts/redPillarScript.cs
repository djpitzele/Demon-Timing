using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redPillarScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out EntityClass ec))
        {
            collision.collider.attachedRigidbody.AddForce((collision.gameObject.transform.position - transform.position).normalized * 50);
            ec.getHit(5, "melee");
        }
    }
}
