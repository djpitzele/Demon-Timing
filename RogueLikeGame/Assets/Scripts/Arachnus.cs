using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arachnus : MonoBehaviour, MeleeClass
{
    private int curMana;
    private int maxMana = 10;
    private float curHP;
    private float maxHP = 10;
    private float dmg;
    private float tilAttack = 0;
    private GameObject player;
    public bool stunned;
    public float speed;
    /*public SoldierCommander(int theMaxMana, float theMaxHP)
    {
        maxMana = theMaxMana;
        
        maxHP = theMaxHP;
        
    }*/
    // Start is called before the first frame update
    public void setFacing(int n)
    {

    }
    public void setSpeed(float speed)
    {
        GetComponent<MeleeAttacker>().meleeSpeed *= speed;
    }
    void Start()
    {
        speed = GetComponent<MeleeAttacker>().meleeSpeed;
        Rigidbody2D r = this.gameObject.GetComponent<Rigidbody2D>();
        //r.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        dmg = 5;
        curMana = maxMana;
        curHP = maxHP;
        //Debug.Log(curHP);
    }
    public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.TryGetComponent(out PlayerClass ec) && tilAttack <= 0)
        {
            ec.getHit(dmg, "melee");
            c.gameObject.GetComponent<Rigidbody2D>().AddForce((c.gameObject.transform.position - transform.position) * 100, ForceMode2D.Force);
            c.gameObject.GetComponent<MovementScript>().timeTilmovement += .2f;
            tilAttack = 1;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (tilAttack > 0 && !stunned)
        {
            tilAttack -= Time.deltaTime;
        }
    }

    public void getHit(float dm, string typeHit)
    {
       
        //Debug.Log("soldier got hit for " + dm + typeHit + "--" + curHP);
        curHP -= dm;
        if (curHP <= 0)
        {
            die();
        }
       else { StartCoroutine(ResetColor(GetComponent<SpriteRenderer>(), dm)); }
    }
    public void die()
    {
        //Debug.Log("RIP bozo");
        player.GetComponent<PlayerClass>().totalEnemies--;
        player.GetComponent<PlayerClass>().totalkills++;
        Destroy(this.gameObject);


    }
    public void setPlayer(GameObject go)
    {
        player = go;
    }
    public GameObject ecgetObject()
    {
        return this.gameObject;
    }
    private IEnumerator ResetColor(SpriteRenderer sr, float dm)
    {
        stunned = true;
        GetComponent<MeleeAttacker>().meleeSpeed = 0;
        sr.color = new Color(1, .5f, .5f, 1);
        yield return new WaitForSeconds(dm/10f);
        sr.color = Color.white;
        stunned = false;
        GetComponent<MeleeAttacker>().meleeSpeed = speed;
    }
}
