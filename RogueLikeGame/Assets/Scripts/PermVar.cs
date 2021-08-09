using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
[System.Serializable]
public class PermVar
{
    public static PermVar current;
    public int Shade;
    /*public float meleeBuff;
    //healthBuff and manaBuff are the amount of health on top of 100, meleeBuff is the % damage added
    public int healthBuff;
    public int manaBuff;*/
}
