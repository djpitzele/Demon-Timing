using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordMovement : MonoBehaviour
{
    public GameObject thePlayer;
    private float attackTime = 0f;
    private float totalAttackTime = 0.25f;
    public Vector3 attackPosition = new Vector3(0.6f, 0.8f, 0);
    private Vector3 totalChangePosition;
    private Quaternion normalRotation;
    private Quaternion currentRotation;
    private int facing = 1;
    // Start is called before the first frame update
    void Start()
    {
        normalRotation = transform.rotation;
        totalChangePosition = new Vector3(0, -2 * attackPosition.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        facing = thePlayer.GetComponent<MovementScript>().getFacing();
        //Debug.Log(thePlayer.GetComponent<PlayerClass>().getFacing());
        if (attackTime <= -0.5f && Input.GetAxis("Attack") != 0)
        {
            attackTime = totalAttackTime;
            transform.position = transform.parent.position;
            transform.position += attackPosition;
            currentRotation = new Quaternion(0, 0, 0, 1);
        }
        if (attackTime > 0)
        {
            transform.position += totalChangePosition / (totalAttackTime / Time.deltaTime);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, endAttackPosition, 90);
            //Debug.Log(facing);
            currentRotation.eulerAngles += new Vector3(0, 0, facing * -1 * 90f * (Time.deltaTime / totalAttackTime));
            transform.rotation = currentRotation;
            attackTime -= Time.deltaTime;
        }
        else if (-0.2 <= attackTime && attackTime <= 0)
        {
            transform.position = transform.parent.position;
            transform.rotation = normalRotation;
            attackTime -= Time.deltaTime;
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.TryGetComponent(out EntityClass ec))
        {
            ec.getHit(thePlayer.GetComponent<PlayerClass>().getDmg(), "melee");
        }
    }
}
