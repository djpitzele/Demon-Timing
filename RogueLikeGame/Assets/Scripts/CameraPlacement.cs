using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// USE -10 INSTEAD OF 0 FOR THE Z-AXIS OF THE CAMERA
public class CameraPlacement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(10, 5, -10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
