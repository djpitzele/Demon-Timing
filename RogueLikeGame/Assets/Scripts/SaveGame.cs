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

}
