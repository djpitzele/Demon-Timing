using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordMovement : MonoBehaviour
{
    public GameObject thePlayer;
    private float attackTime = 0f;
    private float totalAttackTime = 0.25f;
    private float totalStabTime = 2f;
    private float stabDistance = 3f;
    private Vector3 stabSwordPos;
    public Vector3 attackPosition = new Vector3(0.6f, 0.8f, 0);
    private Vector3 totalChangePosition;
    private Quaternion normalRotation;
    private Quaternion currentRotation;
    private int facing = 1;
    private float kb = 400;
    private bool isStab;
    // Start is called before the first frame update
    void Start()
    {
        normalRotation = transform.rotation;
        totalChangePosition = new Vector3(0, -2 * attackPosition.y, 0);
        GetComponent<PolygonCollider2D>().enabled = false;
        isStab = false;
    }

    // Update is called once per frame
    void Update()
    {
        facing = thePlayer.GetComponent<MovementScript>().getFacing();
        //Debug.Log(thePlayer.GetComponent<PlayerClass>().getFacing());
        if (attackTime <= -0.5f && Input.GetAxis("Attack") > 0)
        {
            isStab = false;
            attackTime = totalAttackTime;
            transform.position = transform.parent.position;
            transform.position += attackPosition;
            currentRotation = new Quaternion(0, 0, 0, 1);
            GetComponent<PolygonCollider2D>().enabled = true;
        }
        else if(attackTime <= -0.5f && Input.GetAxis("Attack") < 0)
        {
            isStab = true;
            attackTime = totalStabTime;
            transform.position = transform.parent.position;
            //transform.rotation = Quaternion.identity;
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 diff = mousePos - transform.position;
            Vector3 normalDiff = diff;
            normalDiff.Normalize();
            stabSwordPos = normalDiff * stabDistance;
            //transform.position += stabSwordPos;
            /*Transform mouseTransform = new GameObject().transform;
            mouseTransform.position = mousePos;
            mouseTransform.rotation = Quaternion.identity;*/
            float rot_z = Mathf.Atan2(normalDiff.y, normalDiff.x) * Mathf.Rad2Deg;
            float offset = thePlayer.GetComponent<MovementScript>().getFacing();
            if(offset == 1)
            {
                offset = -45;
            }
            else
            {
                offset = -135;
            }
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + offset);
            //transform.LookAt(mouseTransform);
            GetComponent<PolygonCollider2D>().enabled = true;
        }
        if (attackTime > 0 && !(isStab))
        {
            transform.position += totalChangePosition / (totalAttackTime / Time.deltaTime);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, endAttackPosition, 90);
            //Debug.Log(facing);
            currentRotation.eulerAngles += new Vector3(0, 0, facing * -1 * 90f * (Time.deltaTime / totalAttackTime));
            transform.rotation = currentRotation;
            attackTime -= Time.deltaTime;
        }
        else if(attackTime > 0 && isStab)
        {
            transform.position = Vector3.MoveTowards(transform.parent.position, transform.parent.position + stabSwordPos, stabDistance * Mathf.Sin((2 - attackTime) * (Mathf.PI / 2f)));
            attackTime -= Time.deltaTime;
        }
        else if (-0.2 <= attackTime && attackTime <= 0)
        {
            transform.position = transform.parent.position;
            transform.rotation = normalRotation;
            attackTime -= Time.deltaTime;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb) && rb.bodyType == RigidbodyType2D.Static && attackTime > totalAttackTime / 2)
        {
            attackTime = -0.1f;
            Invoke("ShortCooldown", .1f);

        }
        else if (c.gameObject.TryGetComponent(out EntityClass ec) && !(c.isTrigger))
        {
            if(isStab)
            {
                attackTime = -0.1f;
            }
            ec.getHit(thePlayer.GetComponent<PlayerClass>().getDmg(), "melee");
            if(ec.ecgetObject().TryGetComponent<Rigidbody2D>(out Rigidbody2D rbb))
            {
                rbb.AddForce((ec.ecgetObject().transform.position - transform.position) * kb, ForceMode2D.Force);
            }
        }
    }
    void ShortCooldown()
    {
        attackTime -= .2f;
    }
}
