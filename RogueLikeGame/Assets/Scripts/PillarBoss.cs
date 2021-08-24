using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PillarBoss : MonoBehaviour
{
    public Tile redPillar;
    public int state = 0;
    public PlayerClass pc;
    public int hp = 5;
    public int count2 = 5;
    public int count { get { return count2; } set { count2 = value; if (count2 <= 0) { count = 5; state = (state + 1) % 2; } } }
    public float cooldown = 15f;
    public bool stunned = false;
    public Grid pillarGrid;
    public Tilemap pillars;
    public GameObject door;
    public Sprite goldenDoor;
    //0 = about to place pillars 1 = jumping 
    // Start is called before the first frame update
    void Start()
    {
        pc = PlayerClass.main;
        door = DoorOut.main.gameObject;
        door.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else if (stunned && cooldown <= 0)
        {
            stunned = false;
            GetComponent<SpriteRenderer>().color = Color.white;
            cooldown = 1f;
        }
        else if(state == 0 && !stunned)
        {
            StartCoroutine(placepillar(PlayerClass.main.transform.position));
            count--;
        }
        else if(state == 1 && !stunned)
        {
            StartCoroutine(jump(PlayerClass.main.transform.position));
            count--;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EntityClass ec))
        {
            collision.attachedRigidbody.AddForce((collision.gameObject.transform.position - transform.position).normalized * 50000);
            ec.getHit(20, "melee");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EntityClass ec))
        {
            ec.getHit(10, "melee");
        }
   
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pillars") && transform.position.z == 0)
        {
            stunned = true;
            GetComponent<SpriteRenderer>().color = Color.red;
            cooldown += 2f;
            hp--;
            pillars.SetTiles(radius(pillarGrid.WorldToCell((Vector2)transform.position)), nulls());
            //Debug.Log(hp + " " + pillarGrid.WorldToCell((Vector2)transform.position));
            if (hp == 0)
            {
                die();
            }
        }
    }
    public Tile[] nulls()
    {
        Tile[] t = new Tile[25];
        for(int i = 0; i < 25; i++)
        {
            t[i] = null;
        }
        return t;
    }
    public void die()
    {
        floorCreator.main.waves = 0;
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
        door.GetComponent<SpriteRenderer>().sprite = goldenDoor;
        Destroy(this.gameObject);
    }
    public Vector3Int[] radius(Vector3Int v)
    {

        List<Vector3Int> vs = new List<Vector3Int>();
        for(int i = -2; i<= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                vs.Add(new Vector3Int(v.x + i, v.y + j, v.z));
            }
        }
        Vector3Int[] va = vs.ToArray();
        return va;
    }
    public IEnumerator jump(Vector3 pos)
    {
        cooldown = 1f;
        stunned = true;
        Vector3 startpos = transform.position;
        if (9f - pos.y <= 1.5f)
        {
            pos.y += -1.5f;
        }
        else if((pos.y <= 1.5f))
        {
            pos.y += 1.5f;
        }
        else if ((16f - pos.x <= 1.5f))
        {
            pos.x += -1.5f;
        }
        else if ((pos.x <= 1.5f))
        {
            pos.x += 1.5f;
        }
        int i = 0;
        float dist = ((Vector2)(pos - transform.position)).magnitude;
        
        while(transform.position != pos && i <= 400)
        { 
            i++;
            Vector2 v = Vector2.MoveTowards(transform.position, pos, .05f);
            float zchange = transform.position.z;
            if(((Vector2)(transform.position - startpos)).magnitude > dist / 2f)
            {
                zchange += -.5f;
            }
            else
            {
                zchange += .5f;
            }
            Vector3 delta = new Vector3(v.x, v.y, zchange);
            transform.position = delta;
            yield return new WaitForEndOfFrame();
        }
        GetComponent<CircleCollider2D>().enabled = true;
        transform.Find("Impact Circle").GetComponent<SpriteRenderer>().enabled = true;
        cooldown = 2f;
        yield return new WaitForFixedUpdate();
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(.3f);
        transform.Find("Impact Circle").GetComponent<SpriteRenderer>().enabled = false;


    }
    public IEnumerator placepillar(Vector3 pos)
    {
        cooldown = 1.1f;
        yield return new WaitForSeconds(.1f);
        pillars.SetTile(pillarGrid.WorldToCell(pos), redPillar);
        
    }
}
