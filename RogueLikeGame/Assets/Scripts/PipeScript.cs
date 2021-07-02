using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class PipeScript : MonoBehaviour, EntityClass
{
    public RedDragonScript dragonScript;
    private float hp = 60;
    private bool fireon = false;
    public floorCreator floor;
    public GameObject fire;
    private int index;
    public float timeBetweenFlames;
    private float timeUntilNextFlame;
    public GameObject[] flames = new GameObject[24];
    public List<Animator> animators;
    //index 0 is the fire, index 1 is itself
    BoxCollider2D[] boxCollider2Ds;
    // Start is called before the first frame update
    void Start()
    {
        animators = new List<Animator>();
        boxCollider2Ds = GetComponents<BoxCollider2D>();
        for(int i = 1; i <= 24; i++)
        {
            GameObject f = Instantiate(fire, this.transform);
            f.SetActive(false);
            f.transform.position += new Vector3(-1 * i, 0, 0);
            flames[i - 1] = f;
            //f.GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;
            animators.Add(f.GetComponent<Animator>());

        }
        timeUntilNextFlame = timeBetweenFlames;
        index = 0;

        /*foreach(Transform child in transform)
        {
            animators.Add(child.gameObject.GetComponent<Animator>());
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (fireon && index % 5 == 0)
        {
            flames[index / 5].SetActive(true);
            index++;
        }
        else if (fireon)
        {
            index++;
        }
        Debug.Log(index);*/
        if (fireon)
        {
            if(index >= 24)
            {
                Invoke("DisableFire", 3f);
                foreach(Animator a in animators)
                {
                    a.SetBool("doneSpawning", true);
                }
                index = 0;
            }
            timeUntilNextFlame -= Time.deltaTime;
            if (timeUntilNextFlame <= 0)
            {
                flames[index].SetActive(true);
                timeUntilNextFlame = timeBetweenFlames;
                index++;
            }
            Debug.Log(animators[0].GetBool("doneSpawning"));
        }
    }
    public void getHit(float dm, string typeHit)
    {
        if(typeHit == "ice")
        {
            hp -= dm;
        }
        if(hp == 0)
        {
            die();
        }
    }
    public void die()
    {
        dragonScript.pipes.Remove(this);
        Debug.Log("good job");
        Destroy(this.gameObject);
        floor.waves -= 100;
    }
    public void setPlayer(GameObject player)
    {

    }
    public GameObject ecgetObject()
    {
        return this.gameObject;
    }
    public void Fire()
    {
       fireon = true;
        foreach (Animator a in animators)
        {
            a.SetBool("doneSpawning", false);
        }
        //transform.localScale *= 2;
    }
    public void DisableFire()
    {
        fireon = false;
        index = 0;
        foreach(GameObject g in flames)
        {
            g.SetActive(false);
        }
    }

}
