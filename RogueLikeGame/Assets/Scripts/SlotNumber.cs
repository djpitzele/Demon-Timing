using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotNumber : MonoBehaviour
{
    private int number2;
    public Sprite[] numbers = new Sprite[9];
    public int number
    {
        get { return number2; }
        set { number2 = value; GetComponent<SpriteRenderer>().sprite = numbers[value]; }
    }
    // Start is called before the first frame update
}
