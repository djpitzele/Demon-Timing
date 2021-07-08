using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PermVar
{
    public static PermVar current;
    public int Shade;
    public float meleeBuff;
    public static void Reset()
    {
        PermVar.setCurrent(0,0f);
    }
    //how can you set the default of curShade, curmeleeBuff and other inputs to current
    public static PermVar setCurrent(int curShade, float curmeleeBuff)
    {
        current = new PermVar();
        current.Shade = curShade;
        current.meleeBuff = curmeleeBuff;
        return current;
    }
}
