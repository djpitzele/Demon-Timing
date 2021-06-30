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
    private float dmg;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0f;
        curHP = maxHP;
    }

    public void getHit(float dm, string type)
    {
        curHP -= dm;
        if (curHP <= 0)
        {
            die();
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

    // Update is called once per frame
    void Update()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    public void attack()
    {
        if(cooldown <= 0)
        {
            Debug.Log("we rly attack");
            GameObject theBullet = Instantiate(projectile, this.transform);
            theBullet.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position) * projSpeed);
            theBullet.GetComponent<BulletScript>().dmg= 5;
            //ROTATE THE BULLET TOWARDS THE PLAYER
            cooldown = 1f;
        }
    }
}
