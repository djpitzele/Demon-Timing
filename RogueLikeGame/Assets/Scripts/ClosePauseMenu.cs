using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(closePause);
    }

    // Update is called once per frame
    public void closePause()
    {
        transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
