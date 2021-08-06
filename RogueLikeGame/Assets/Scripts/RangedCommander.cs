using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedCommander : MonoBehaviour, RangedClass
{
    public GameObject player;
    public float cooldown;
    public GameObject projectile;
    public float projSpeed;
    private float curHP;
    public float maxHP = 10;
    public float dmg = 5;
    private int facing = 1;
    public bool stunned;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0f;
        curHP = maxHP;
      //  Debug.Log(facing);
    }

    public void setFacing(int n)
    {
        if (facing == n)
        {
          
            return;
        }
        else
        {
            Vector3 curScale = transform.localScale;
            curScale.x *= -1;
            transform.localScale = curScale;
            facing = n;
        }
    }

    public void setSpeed(float s)
    {
        GetComponent<RangedAttacker>().meleeSpeed *= s;
    }
    public void getHit(float dm, string type)
    {
      
        curHP -= dm;
        if (curHP <= 0)
        {
            die();
            PlayerClass.main.totalEnemies--;
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

    // Update is called once per frame
    void Update()
    {
        if(cooldown > 0 && !stunned)
        {
            cooldown -= Time.deltaTime;
        }
    }

    public IEnumerator attack()
    {
        
        if(cooldown <= 0)
        {
            cooldown = 4f;
            GetComponent<Animator>().SetBool("Attacking", true);
            Vector3 formerPos = player.transform.position;
            yield return new WaitForSeconds(1 / 3f);

            //Debug.Log("we rly attack");
            GameObject theBullet = Instantiate(projectile, this.transform);
            theBullet.layer = 8;
            theBullet.GetComponent<BulletScript>().targetTransform = formerPos;
            theBullet.GetComponent<Rigidbody2D>().AddForce((formerPos - transform.position) * projSpeed);
            theBullet.GetComponent<BulletScript>().dmg= dmg;
            //ROTATE THE BULLET TOWARDS THE PLAYER
            
            GetComponent<Animator>().SetBool("Attacking", false);
        }
    }
    public MonoBehaviour returnMB()
    {
        return this;
    }
}
