using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction
{
    public Sprite theSprite;
    public string message;
    public Font theFont;

    public Interaction(Sprite s, string mes, Font f)
    {
        theSprite = s;
        message = mes;
        theFont = f;
    }
}
