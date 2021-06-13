using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EntityClass
{
    //private float curHP;
    //private float maxHP;
    //private float speed;
    //if timeToMovement is 0, can move; otherwise, it decreases each frame and cannot move
    //private int timeToMovement = 0;
    public void getHit(float dmg, string typeHit);
    /*{
        curHP -= dmg;
        if(curHP <= 0)
        {
            die();
        }
    }*/
    public void die();
    /*public void setCurHP(float hp)
    {
        curHP = hp;
    }
    public void setMaxHP(float hp)
    {
        maxHP = hp;
    }
    public void setSpeed(float speeds)
    {
        speed = speeds;
    }
    public float getCurHP()
    {
        return curHP;
    }
    public float getMaxHP()
    {
        return maxHP;
    }
    public float getSpeed()
    {
        return speed;
    }
    public float getX()
    {
        return xCoord;
    }
    public float getY()
    {
        return yCoord;
    }*/
}
