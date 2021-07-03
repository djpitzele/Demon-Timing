using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour
{
    //cooldown is the cooldown on the dash
    public float cooldown = 0;
    public GameObject canvas;
    private float speed = 11f;
    private static double pi = Math.PI;
    private float fpi = Convert.ToSingle(pi);
    Rigidbody2D rb;
    //1 = right, -1 = left
    private int facing = 1;
    public float timeTilmovement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float curMovement = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Reset") != 0)
        {
            SceneManager.LoadScene(0);
            Destroy(canvas);
            Destroy(this.gameObject);
        }
        if(Input.GetAxis("Submit") != 0)
        {
            GetComponent<PlayerClass>().nextScene();
        }
        if (timeTilmovement <= 0)
        {

            if (curMovement > 0)
            {
                if (facing != 1)
                {
                    Vector3 curScale = transform.localScale;
                    curScale.x *= -1;
                    transform.localScale = curScale;
                    transform.GetChild(0).gameObject.GetComponent<swordMovement>().attackPosition.x *= -1;
                }
                facing = 1;
            }
            else if (curMovement < 0)
            {
                if (facing != -1)
                {
                    Vector3 curScale = transform.localScale;
                    curScale.x *= -1;
                    transform.localScale = curScale;
                    transform.GetChild(0).gameObject.GetComponent<swordMovement>().attackPosition.x *= -1;
                }
                facing = -1;
            }
            if (cooldown <= 0 && UnityEngine.Input.GetAxis("Jump") == 1)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                //Debug.Log(mousePos);
                rb.MovePosition(Vector3.MoveTowards(transform.position, mousePos, 9));
                cooldown = 1;
            }
            else
            {
                Vector3 changes = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                rb.MovePosition(transform.position + changes * speed * Time.deltaTime);
            }
        }

        if (cooldown > 0)
        {
            cooldown -= Time.fixedDeltaTime;
        }
        if(timeTilmovement > 0)
        {
            timeTilmovement -= Time.fixedDeltaTime;
        }
    }

    public int getFacing()
    {
        return facing;
    }
    /*public void resetscene()
    {
        //Scene s = SceneManager.GetActiveScene();
        //Scene s = SceneManager.GetSceneByName("Assets/RedDragon");
        SceneManager.LoadScene(2);
    }*/

    //float xDiff = UnityEngine.Input.GetAxis("Horizontal") * speed;--
    //float yDiff = UnityEngine.Input.GetAxis("Vertical") * speed;
    /*
    if(cooldown == 0 && UnityEngine.Input.GetAxis("Jump") == 1)
    {
        float xP = this.transform.position.x * 96;
        float yP = this.transform.position.y * 96;
        //Debug.Log(xP + "---" + yP);
        Vector3 temporaryVar = Input.mousePosition;
        float xMouse = temporaryVar.x;
        float yMouse = temporaryVar.y;
        //float xMouse = UnityEngine.Input.GetAxis("Mouse X");
        //float yMouse = UnityEngine.Input.GetAxis("Mouse Y");
        //Debug.Log(xMouse + "---" + yMouse);
        float angle;
        if(xMouse == xP && (yMouse - yP) >= 0)
        {
            angle = fpi / 2;
        }
        else if (xMouse == xP && (yMouse - yP) < 0)
        {
            angle = fpi / -2;
        }
        else
        {
            float num = (yMouse - yP) / (xMouse - xP);
            angle = Convert.ToSingle(Math.Atan(num));
        }
        if(xMouse - xP < 0)
        {
            angle += (fpi);
        }
        //Debug.Log(angle * (180 / fpi));
        xDiff += Convert.ToSingle(Math.Cos(angle)) * 4f;
        yDiff += Convert.ToSingle(Math.Sin(angle)) * 4f;
        cooldown = 60;
    }*/
    /*if(UnityEngine.Input.GetAxis("Jump") == 1)
    {
        float xP = Math.Abs(this.transform.position.x);
        float yP = Math.Abs(this.transform.position.y);
        float xMouse = Math.Abs(UnityEngine.Input.GetAxis("Mouse X"));
        float yMouse = Math.Abs(UnityEngine.Input.GetAxis("Mouse Y"));
        float xd = xMouse - xP;
        float yd = yMouse - yP;
        float d = Convert.ToSingle(Math.Sqrt((xd * xd) + (yd * yd)));
        float f = 0.2f / d;
        xDiff += xd * f;
        yDiff += yd * f;
    }*/
    //transform.Translate(xDiff, yDiff, 0);
}
