using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDragonOneFire : MonoBehaviour
{
    private bool canHit;
    // Start is called before the first frame update
    void Start()
    {
        canHit = true;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass pc) && canHit)
        {
            pc.getHit(20, "fire");
            canHit = false;
            Invoke("ableToHitAgain", 0.2f);
        }
    }

    private void ableToHitAgain()
    {
        canHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
