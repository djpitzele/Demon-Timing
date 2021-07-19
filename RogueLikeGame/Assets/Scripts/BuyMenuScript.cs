using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuyMenuScript : MonoBehaviour
{
    public NPClass opener;
    //Prefab
    public GameObject Option;
    public List<UnityAction> purchaseActions = new List<UnityAction>();
    public List<string> labels = new List<string>();
    //costs is just initial price
    public List<string> costs = new List<string>();
    public List<GameObject> options = new List<GameObject>();
    public bool start = true;
    // Start is called before the first frame update
    void Start()
    {
        /*int i = 0;
        foreach(UnityAction ua in purchaseActions)
        {
            Debug.Log(i);
            Button b = Instantiate(Option).GetComponent<Button>();
            b.gameObject.transform.SetParent(transform, false);
            b.onClick.AddListener(purchaseActions[i]);
            b.GetComponentsInChildren<Text>()[0].text = labels[i];
            i++;
        }*/
            
 
    }

    public void destroyAllOptions()
    {
        //empty line
        foreach(GameObject g in options)
        {
            Destroy(g);
        }
        PlayerClass.main.menuOn = false;
        options.Clear();
    }

    //newImage.rectTransform.anchoredPosition = Vector3.zero;
    private void OnEnable()
    {
        if (!start)
        {
            PlayerClass.main.menuOn = true;
            int i = 0;
            //Debug.Log(i + " " + purchaseActions.Count);

            foreach (UnityAction ua in purchaseActions)
            {
                GameObject g = Instantiate(Option);
                options.Add(g);
                Button b = g.GetComponent<Button>();
                RectTransform r = g.GetComponent<RectTransform>();
                b.gameObject.transform.SetParent(transform.parent, false);
                //was adding 400 for no reason, so we subtract 400
                r.localPosition = new Vector3(transform.parent.gameObject.GetComponent<RectTransform>().rect.width * ((i + 1) / 4f) - 400, r.localPosition.y, 0);
                //Debug.Log(r.localPosition);
                r.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, transform.parent.gameObject.GetComponent<RectTransform>().rect.height * 0.8f);
                r.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, transform.parent.gameObject.GetComponent<RectTransform>().rect.width / ((float)purchaseActions.Count + 2));
                //r.localScale = new Vector3(Screen.width / 3f, Screen.height * 0.8f, 1);
                g.name = "Option " + (i+1);
                b.onClick.AddListener(purchaseActions[i]);
                g.GetComponentsInChildren<Text>()[0].text = labels[i];
                g.GetComponentsInChildren<Text>()[1].text = costs[i];
                i++;
            }
        }
        else
        {
            start = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
