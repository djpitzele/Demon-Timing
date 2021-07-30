using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblerScript : MonoBehaviour, NPClass
{
    public Sprite gamblerSprite;
    public GameObject tokenPrefab;
    public bool inRange;
    public PlayerClass pc;
    public BuyMenuScript bms;
    public int interactions = 0;

    public void dropToken()
    {
        GameObject t = Instantiate(tokenPrefab);
        t.transform.position = transform.position + new Vector3(0, -2, 0);
        pc.gold -= 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        pc = PlayerClass.main;
        bms = pc.bms;
    }
    public Interaction Interact()
    {
        if (interactions == 0)
        {
            PlayerClass.main.theCanvas.GetComponentsInChildren<GoldUI>()[0].updateGoldLobby();
            if(pc.gold >= 10)
            {
                interactions++;
            }
            interactions++;
            return (new Interaction(gamblerSprite, "Up for some gambling?", Resources.GetBuiltinResource<Font>("Arial.ttf")));
        }
        else if(interactions == 1 && pc.gold < 10)
        {
            interactions++;
            return (new Interaction(gamblerSprite, "Are you sure? Bad things happen when you're in debt.", Resources.GetBuiltinResource<Font>("Arial.ttf")));
        }
        else if(interactions == 2)
        {
            SpellTracker.main.Start2();
            interactions++;
            bms.purchaseActions = new List<UnityEngine.Events.UnityAction>();
            bms.purchaseActions.Add(dropToken);
            //Debug.Log("shady" + bms.purchaseActions.Count);
            bms.labels = new List<string>();
            bms.labels.Add("One Gambling Token");
            bms.costs = new List<string>();
            bms.costs.Add("10 Gold");
            bms.gameObject.SetActive(true);
        }
        else if (interactions == 3)
        {
            interactions = 0;
            bms.labels = new List<string>();
            bms.purchaseActions = new List<UnityEngine.Events.UnityAction>();
            bms.costs = new List<string>();
            bms.gameObject.SetActive(false);
            bms.destroyAllOptions();
        }
        return null;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass pcg))
        {
            inRange = true;
            pc = pcg;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerClass>(out PlayerClass pc))
        {
            inRange = false;
            bms.transform.parent.Find("Interaction").gameObject.SetActive(false);
            bms.gameObject.SetActive(false);
            bms.destroyAllOptions();
            interactions = 0;
        }
    }
}
