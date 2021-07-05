using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    public GameObject myPlayer;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(doClick);
    }

    public void doClick()
    {
        SceneManager.LoadScene(0);
        Destroy(this.transform.parent.parent.gameObject);
        Destroy(myPlayer);
    }
}
