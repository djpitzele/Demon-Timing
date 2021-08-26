using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometScript : MonoBehaviour
{
    public PlayerClass pc;
    private float speed = 700000f;
    public float timeLoop = 0f;
    public bool dashing = false;
    public int hp = 5;
    public bool stunned = false;
    public float dashCooldown = 7f;
    public GameObject door;
    public float offsetFromYou = 5f;
    // Start is called before the first frame update
    void Start()
    {
        pc = PlayerClass.main;
        //door = DoorOut.main.gameObject;
        door.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(dashCooldown <= 0 && !stunned)
        {
            dashCooldown = 7f;
            StartCoroutine(dash());
        }
        if (!dashing && !stunned)
        {
            Vector3 offset = new Vector3(Mathf.Cos(getDeg(timeLoop)) * offsetFromYou, Mathf.Sin(getDeg(timeLoop)) * offsetFromYou);
            Vector3 newPos = Vector3.MoveTowards(transform.position, pc.transform.position + offset, speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().position = newPos;
            timeLoop += Time.deltaTime;
            if(dashCooldown > 0)
            {
                dashCooldown -= Time.deltaTime;
            }
        }
    }
    private IEnumerator dash()
    {
        dashing = true;
        Debug.Log("dash");
        stunned = true;
        Invoke("endStun", 0.2f);
        yield return new WaitForSeconds(0.2f);
        float tempTime = timeLoop + 2.5f;
        GetComponent<Rigidbody2D>().AddForce(((Vector2)pc.transform.position - (Vector2)transform.position).normalized * 1000f);
        Debug.Log(((Vector2)pc.transform.position + " " + (Vector2)transform.position));
    }
    private float getDeg(float time)
    {
        time = time % 5;
        float pi = Mathf.PI;
        float the = (2f * pi) / 5f;
        return (time * the);
    }
    private void OnTriggerStay2D(Collider2D c)
    {
        Debug.Log("enter trig");
        if (c.TryGetComponent<EntityClass>(out EntityClass ec))
        {
            ec.getHit(1f, "melee");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("enter col");
        if (dashing && collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass pc))
        {
            pc.getHit(20, "melee");
            endDash();
        }
        else if (dashing && collision.collider.attachedRigidbody.bodyType == RigidbodyType2D.Static)
        {
            hp--;
            if(hp <= 0)
            {
                die();
            }
            stunned = true;
            Invoke("notStunned", 2f);
        }
    }
    public void die()
    {
        floorCreator.main.waves = -1;
        GameObject[] gms = (GameObject[])FindObjectsOfType(typeof(GameObject));
        foreach (GameObject g in gms)
        {
            if (g.TryGetComponent<PlayerClass>(out PlayerClass pc))
            {
                continue;
            }
            else if (g.TryGetComponent<EntityClass>(out EntityClass ec))
            {
                ec.die();
            }
        }
        door.GetComponent<SpriteRenderer>().enabled = true;
        //set sprite to dead once its drawn
        Destroy(this);
    }
    private void endDash()
    {
        Debug.Log("dash gets ended");
        dashing = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        Vector3 offset = new Vector3(Mathf.Cos(getDeg(timeLoop)) * offsetFromYou, Mathf.Sin(getDeg(timeLoop)) * offsetFromYou);
        transform.position = pc.transform.position + offset;
    }
    //dont delete endStun cuz of notStunned, they are both needed
    private void endStun()
    {
        stunned = false;
    }
    private void notStunned()
    {
        stunned = false;
        endDash();
    }
}
