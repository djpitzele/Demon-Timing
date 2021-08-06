using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordMovement : MonoBehaviour
{
    public GameObject thePlayer;
    public Rigidbody2D rb;
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
    public float manaRegen = 5;
    public PlayerClass pc;
    private float swordVelocity = 2f;
    // Start is called before the first frame update
    void Start()
    {
        normalRotation = transform.rotation;
        totalChangePosition = new Vector3(0, -2 * attackPosition.y, 0);
        GetComponent<PolygonCollider2D>().enabled = false;
        isStab = false;
        pc = thePlayer.GetComponent<PlayerClass>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        facing = thePlayer.GetComponent<MovementScript>().getFacing();
        //Debug.Log(thePlayer.GetComponent<PlayerClass>().getFacing());
        if (attackTime <= -0.5f && Input.GetAxis("Attack") > 0 && !isStab)
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
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 diff = mousePos - transform.position;
            Vector3 normalDiff = diff;
            normalDiff.Normalize();
            stabSwordPos = transform.parent.position + (normalDiff * 10);
            isStab = true;
            attackTime = totalStabTime;
            transform.position = transform.parent.position + normalDiff;
            GetComponent<PolygonCollider2D>().isTrigger = false;
            GetComponent<PolygonCollider2D>().enabled = true;
            this.gameObject.transform.SetParent(null, true);
            rb.AddForce(stabSwordPos);
            //transform.rotation = Quaternion.identity;



            /* Debug.Log(stabSwordPos.magnitude + " " + diff.normalized.magnitude + transform.parent.position + stabSwordPos);
             //transform.position += stabSwordPos;
             /*Transform mouseTransform = new GameObject().transform;
             mouseTransform.position = mousePos;
             mouseTransform.rotation = Quaternion.identity;
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
             transform.rotation = Quaternion.Euler(0f, 0f, rot_z + offset);*/
            //transform.LookAt(mouseTransform);

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
            // transform.position = Vector3.MoveTowards(transform.parent.position, transform.parent.position + stabSwordPos, stabDistance * Mathf.Sin((2 - attackTime) * (Mathf.PI / 2f)));
            //rb.position = Vector3.MoveTowards(transform.position, stabSwordPos, swordVelocity);

            rb.rotation += 1f;
            attackTime -= Time.deltaTime;
        }
        else if (-0.2 <= attackTime && attackTime <= 0)
        {
            transform.position = transform.parent.position;
            transform.rotation = normalRotation;
            attackTime -= Time.deltaTime;
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<PolygonCollider2D>().isTrigger = true;
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.TryGetComponent(out PlayerClass pc))
        {
            transform.SetParent(pc.gameObject.transform, false);
            transform.position = transform.parent.position;
            transform.rotation = normalRotation;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
        else if (c.gameObject.TryGetComponent(out EntityClass ec) && !(c.collider.isTrigger))
        {
            ec.getHit(thePlayer.GetComponent<PlayerClass>().getDmg(), "melee");
            pc.curMana += manaRegen;
            if (ec.ecgetObject().TryGetComponent<Rigidbody2D>(out Rigidbody2D rbb))
            {
                rbb.AddForce((ec.ecgetObject().transform.position - transform.position) * kb, ForceMode2D.Force);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb) && rb.bodyType == RigidbodyType2D.Static && isStab)
        {
            //swordVelocity *= -1;
        }
        /*if(c.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb) && rb.bodyType == RigidbodyType2D.Static && attackTime > totalAttackTime / 2 && !isStab)
        {
            attackTime = -0.1f;
            Invoke("ShortCooldown", .1f);

        }
        else if (c.gameObject.TryGetComponent(out EntityClass ec) && !(c.isTrigger))
        {
    
            if (isStab)
            {
                attackTime = -0.1f;
            }
            ec.getHit(thePlayer.GetComponent<PlayerClass>().getDmg(), "melee");
            pc.curMana += manaRegen;
            if(ec.ecgetObject().TryGetComponent<Rigidbody2D>(out Rigidbody2D rbb))
            {
                rbb.AddForce((ec.ecgetObject().transform.position - transform.position) * kb, ForceMode2D.Force);
            }
         
            
            
        }*/
    }
    void ShortCooldown()
    {
        attackTime -= .2f;
    }
 
}
