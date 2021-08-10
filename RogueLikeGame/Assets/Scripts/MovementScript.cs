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
    public GameObject sword;
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
    private float cooldown3;
    public GameObject spell;
    public int manaUsed = 5;
    public PlayerClass pc;
    public float dashDistance = 9f;
    //lower = more effiecnent
    public float manaEfficiency = 1f;
    public bool speedAbility = false;
    public float dashDelay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        sword = transform.Find("Sword").gameObject;
        pc = GetComponent<PlayerClass>();
        interaction = canvas.transform.Find("Interaction").gameObject;
        interaction.SetActive(false);
        pauseMenu = canvas.transform.Find("Pause Menu").gameObject;
        rb = GetComponent<Rigidbody2D>();
        pauseMenu.SetActive(false);
        //pauseMenu = canvas.transform.Find("PauseMenu").gameObject;
        canvas.transform.Find("ManaUsage").GetComponent<ManaUsageScript>().updateMana(this, pc);
    }
    public void dash()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = (Vector2)Camera.main.ScreenToWorldPoint(mousePos);
        //Debug.Log(mousePos.ToString() + rb.ToString() + transform.position.ToString() + dashDistance);
        rb.MovePosition(Vector3.MoveTowards(transform.position, mousePos, dashDistance));
        
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
        if(Input.mouseScrollDelta.y != 0)
        {
            manaUsed = (int)Math.Min(GetComponent<PlayerClass>().maxMana, Math.Max((manaUsed + (int)Input.mouseScrollDelta.y * 5), 0));
            canvas.transform.Find("ManaUsage").GetComponent<ManaUsageScript>().updateMana(this, pc);
        }
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float curMovement = Input.GetAxis("Horizontal");
        if(Input.GetAxis("Fire2") != 0)
        {
            GetComponent<PlayerClass>().nextScene();
        }
        if(Input.GetAxis("Ability") != 0 && pc.abilityCooldown <= 0)
        {
            if(pc.haveAbility)
            {
                StartCoroutine(PlayerClass.main.playerAbility.Invoke());
            }
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
                    sword.GetComponent<swordMovement>().attackPosition.x *= -1;
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
                    sword.gameObject.GetComponent<swordMovement>().attackPosition.x *= -1;
                }
                facing = -1;
            }
            if (Input.GetAxis("Jump") == 1 && dashDelay <= 0) {
                if (cooldown <= 0)
                {
                    dash();
                    cooldown = 1;
                    dashDelay = 0.1f;
                }
                else if(speedAbility && pc.abilityCooldown <= 0f)
                {
                    dash();
                    pc.abilityCooldown = 2.5f;
                    dashDelay = 0.1f;
                }
            }
            else
            {
                Vector3 changes = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                //Debug.Log(rb == null);
                rb.MovePosition(transform.position + changes * speed * Time.deltaTime);
                GetComponent<Animator>().SetBool("Walking", changes != Vector3.zero);
            }
        }

        if (cooldown > 0)
        {
            cooldown -= Time.fixedDeltaTime;
        }
        if(dashDelay > 0)
        {
            dashDelay -= Time.fixedDeltaTime;
        }
        if(timeTilmovement > 0)
        {
            timeTilmovement -= Time.fixedDeltaTime;
        }
        if (cooldown3 <= 0 && !(pc.menuOn) && manaUsed != 0 && pc.curMana >= manaUsed)
        {
            if (Input.GetAxis("Spell1") != 0 && pc.spells[0] != 0)
            {
                UseSpell(pc.spells[0]);
                cooldown3 = .2f;
            }
            if (Input.GetAxis("Spell2") != 0 && pc.spells[1] != 0)
            {
                UseSpell(pc.spells[1]);
                cooldown3 = .2f;
            }
            if (Input.GetAxis("Spell3") != 0 && pc.spells[2] != 0)
            {
                UseSpell(pc.spells[2]);
                cooldown3 = .2f;
            }
        }
        else
        {
            cooldown3 -= Time.fixedDeltaTime;
        }


        // Debug.Log(manaUsed.ToString() + " " + Input.mouseScrollDelta); 
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
            if (Input.GetAxis("Submit") != 0 && cooldown2 <= 0 )
            {
                Interaction i = nc.Interact();
                if (i != null)
                {
                    interaction.SetActive(true);
                    interaction.GetComponent<Image>().sprite = i.theSprite;
                    interaction.GetComponentsInChildren<Text>()[0].text = i.message;
                    //Debug.Log(i.message);
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
    void UseSpell(int i)
    {
        //Debug.Log(i);
        /*GameObject g = Instantiate(spell);
        g.GetComponent<Spells>().Start2();
        g.GetComponent<Spells>().createSpell(i, manaUsed);
        g.GetComponent<Spells>().pc = pc;*/
        SpellTracker.main.CreateSpell(i, manaUsed);
        GetComponent<PlayerClass>().curMana -= (manaUsed * manaEfficiency);
        
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
