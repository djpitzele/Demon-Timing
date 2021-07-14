using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementScript : MonoBehaviour
{
    //cooldown is the cooldown on the dash
    public float cooldown = 0;
    public GameObject canvas;
    public float speed = 11f;
    private static double pi = Math.PI;
    private float fpi = Convert.ToSingle(pi);
    public Rigidbody2D rb;
    //1 = right, -1 = left
    private int facing = 1;
    public float timeTilmovement;
    private int menuCooldown = 0;
    public GameObject pauseMenu;
    public GameObject interaction;
    private float esc;
    private float cooldown2;
    // Start is called before the first frame update
    void Start()
    {
        interaction = canvas.transform.Find("Interaction").gameObject;
        interaction.SetActive(false);
        pauseMenu = canvas.transform.Find("Pause Menu").gameObject;
        rb = GetComponent<Rigidbody2D>();
        pauseMenu.SetActive(false);
        //pauseMenu = canvas.transform.Find("PauseMenu").gameObject;
    }

    private void Update()
    {
        if (Input.GetAxis("Reset") != 0 && menuCooldown <= 0)
        {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            menuCooldown = 200;
        }
        if (menuCooldown > 0)
        {
            menuCooldown--;
        }
        //Debug.Log(Input.GetAxis("Reset") + " " + esc);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float curMovement = Input.GetAxis("Horizontal");
        /*if(Input.GetAxis("Fire2") != 0)
        {
            GetComponent<PlayerClass>().nextScene();
        }*/
        if (timeTilmovement <= 0)
        {

            if (curMovement > 0)
            {
                if (facing != 1)
                {
                    Vector3 curScale = transform.localScale;
                    curScale.x *= -1;
                    transform.localScale = curScale;
                    transform.Find("Sword").gameObject.GetComponent<swordMovement>().attackPosition.x *= -1;
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
                    transform.Find("Sword").gameObject.GetComponent<swordMovement>().attackPosition.x *= -1;
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
                //Debug.Log(rb == null);
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
    public void OnTriggerStay2D(Collider2D collision)
    {
        cooldown2 -= Time.deltaTime;
        //Debug.Log("deezgd");
        if (collision.gameObject.TryGetComponent<NPClass>(out NPClass nc))
        {
            //Debug.Log("deezg");
            if (Input.GetAxis("Submit") != 0 && cooldown2 <= 0)
            {
                Interaction i = nc.Interact();
                if (i != null)
                {
                    interaction.SetActive(true);
                    interaction.GetComponent<Image>().sprite = i.theSprite;
                    interaction.GetComponentsInChildren<Text>()[0].text = i.message;
                    Debug.Log(i.message);
                    cooldown2 = 0.4f;
                }
                else
                {
                    interaction.SetActive(false);
                    cooldown2 = 0.4f;
                }
            }
        }
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
