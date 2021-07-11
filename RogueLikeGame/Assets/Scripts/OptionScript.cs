using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class OptionScript : MonoBehaviour
{

    public UnityEngine.Events.UnityAction puchasebutcooler;
    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public OptionScript(UnityAction p)
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(p);
    }
}
