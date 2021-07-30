using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samurai : MonoBehaviour, RangedClass
{
    public GameObject player;
    public float curHP;
    public float maxHP;
    public float dmg;
    private int facing2;
    public bool dashing;
    public float speed = 5000;
    public Rigidbody2D rb;
    public float cooldown = 0;
    public int facing
    {
        get { return facing2; }
        set { UpdateFacing(facing2, value); facing2 = value;  }
    }
    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
    }
    public void setSpeed(float s)
    {
        GetComponent<Rigidbody2D>().mass *= s;
    }
    public void getHit(float dm, string type)
    {
        curHP -= dm;
        if (curHP <= 0)
        {
            die();
            PlayerClass.main.totalEnemies--;
        }
    }
    public void UpdateFacing(int oldFacing, int newFacing)
    {
        if(oldFacing != newFacing)
        {
            Vector3 curScale = transform.localScale;
            curScale.x *= -1;
            transform.localScale = curScale;
        }
    }
    public GameObject ecgetObject()
    {
        return this.gameObject;
    }

    public void setPlayer(GameObject g)
    {
        player = g;
    }

    public void die()
    {
        Destroy(this.gameObject);
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if(cooldown <= 0 && other.TryGetComponent<PlayerClass>(out PlayerClass pc))
        {
            pc.getHit(dmg, "melee");
            pc.GetComponent<Rigidbody2D>().AddForce((pc.transform.position - transform.position) * 100, ForceMode2D.Force);
            cooldown = 1f;
        }
    }

    public IEnumerator attack()
    {

        if (!(dashing))
        {
            dashing = true;
            //Debug.Log("asdf");
            Vector3 formerPos = player.transform.position;
            yield return new WaitForSeconds(.1f);
            Vector3 vectorToTarget = formerPos - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle + 180, Vector3.forward);
            transform.localRotation = q;
            //DASH IS SO SLOW FOR SOM  E REASON
            GetComponent<Rigidbody2D>().AddForce((Vector3.Normalize(formerPos - transform.position)) * speed);
            yield return new WaitForFixedUpdate();
            //Debug.Log("we rly attack");
            GetComponent<Animator>().SetInteger("state", 1);
            Debug.Log((formerPos - transform.position).magnitude);
            Debug.Log("vel mag" + rb.velocity.magnitude);
            yield return new WaitForSeconds(((formerPos - transform.position).magnitude) / rb.velocity.magnitude);
            dashing = false;
            transform.rotation = Quaternion.identity;
            rb.velocity = Vector2.zero;
            GetComponent<Animator>().SetInteger("state", 2);
            //ROTATE THE BULLET TOWARDS THE PLAYER

        }
    }
    public MonoBehaviour returnMB()
    {
        return this;
    }
}
