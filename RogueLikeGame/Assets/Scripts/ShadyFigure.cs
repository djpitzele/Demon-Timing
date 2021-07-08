using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShadyFigure : MonoBehaviour, NPClass
{
    public bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        if(PermVar.current.Shade > FindCost())
        {
            PermVar.setCurrent(PermVar.current.Shade -= FindCost(), PermVar.current.meleeBuff += .1f);
        }
        
        if(TryGetComponent<Animator>(out Animator a) && a.GetBool("Talking"))
        {
            a.SetBool("Talking", true);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass pc))
        {
            inRange = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass pc))
        {
            inRange = false;
        }
    }

    int FindCost()
    {
        return (int)Math.Pow((PermVar.current.meleeBuff / .1f), 1.42f);
    }
}
