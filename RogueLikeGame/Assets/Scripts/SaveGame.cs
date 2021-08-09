using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
[System.Serializable]
public class SaveGame
{
    public static SaveGame current;
    //public PlayerClass pc;
    public float curHP;
    public float curMana;
    public int gold;
    public int totalKills;
    public List<int> orderScenes;
    public int curSceneIndex = 1;
    public float time;
    public int curShade;
    public int[] spells = new int[3];
    //public int[] choices = new int[5];
    public bool[][] choices = new bool[5][];
    //0 index = bought first choice? 1 index = 0, 1, or 2: first choice? remaining 3: levels of the last progression
}

