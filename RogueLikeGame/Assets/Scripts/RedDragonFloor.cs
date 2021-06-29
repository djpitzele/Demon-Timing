using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDragonFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void changeCamera(GameObject cam)
    {
        cam.GetComponent<Camera>().orthographicSize = 8.6f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
