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
    //newImage.rectTransform.anchoredPosition = Vector3.zero;
    private void OnEnable()
    {
        if (!start)
        {
            int i = 0;
            //Debug.Log(i + " " + purchaseActions.Count);

            foreach (UnityAction ua in purchaseActions)
            {

                GameObject g = Instantiate(Option);
                Button b = g.GetComponent<Button>();
                RectTransform r = g.GetComponent<RectTransform>();
                b.gameObject.transform.SetParent(transform.parent, false);
                Debug.Log(r.localPosition);
                r.localPosition = new Vector3(transform.parent.gameObject.GetComponent<RectTransform>().rect.width * ((i + 1) / 4f), r.localPosition.y, 0);
                r.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, transform.parent.gameObject.GetComponent<RectTransform>().rect.height * 0.8f);
                r.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, transform.parent.gameObject.GetComponent<RectTransform>().rect.width / ((float)purchaseActions.Count + 2));
                //r.localScale = new Vector3(Screen.width / 3f, Screen.height * 0.8f, 1);
                g.name = "Option " + (i+1);
                b.onClick.AddListener(purchaseActions[i]);
                b.GetComponentsInChildren<Text>()[0].text = labels[i];
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
