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
    public float speed;
    public Rigidbody2D rb;
    public float cooldown = 0;
    public float dashCooldown = 0f;
    public bool stunned;
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
        if (cooldown > 0 && !stunned)
        {
            cooldown -= Time.deltaTime;
        }
        if(dashCooldown > 0 && !stunned && !dashing)
        {
            dashCooldown -= Time.deltaTime;
        }
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
            PlayerClass.main.totalEnemies--;
            die();
        }
       else { StartCoroutine(ResetColor(GetComponent<SpriteRenderer>(), dm)); }
    }
    private IEnumerator ResetColor(SpriteRenderer sr, float dm)
    {
        float n = GetComponent<RangedAttacker>().meleeSpeed;
        stunned = true;
        GetComponent<RangedAttacker>().meleeSpeed = 0;
        sr.color = new Color(1, .5f, .5f, 1);
        yield return new WaitForSeconds(dm / 10f);
        sr.color = Color.white;
        stunned = false;
        GetComponent<RangedAttacker>().meleeSpeed = n;
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

        if (!(dashing) && dashCooldown <= 0)
        {
            //Debug.Log("samurai attack");
            dashing = true;
            //Debug.Log("asdf");
            dashCooldown = 0.5f;
            Vector3 formerPos = player.transform.position;
            yield return new WaitForSeconds(.1f);
            Vector3 vectorToTarget = formerPos - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle + 180, Vector3.forward);
            transform.localRotation = q;
            //DASH IS SO SLOW FOR SOM  E REASON
            Vector3 temp = formerPos - transform.position;
            temp = Vector3.Normalize(temp);
            //Debug.Log(speed.ToString() + temp.ToString());
            temp *= speed;
            GetComponent<Rigidbody2D>().AddForce(temp);
            yield return new WaitForFixedUpdate();
            //Debug.Log("we rly attack");
            GetComponent<Animator>().SetInteger("state", 1);
            //Debug.Log((formerPos - transform.position).magnitude);
            //Debug.Log("vel mag" + rb.velocity.magnitude);
            if(rb.velocity.magnitude != 0)
            {
                yield return new WaitForSeconds(((formerPos - transform.position).magnitude) / rb.velocity.magnitude);
            }
            else
            {
                yield return new WaitForFixedUpdate();
            }
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
    public void setFacing(int n)
    {

    }
}
