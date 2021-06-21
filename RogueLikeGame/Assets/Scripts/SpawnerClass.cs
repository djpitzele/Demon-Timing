using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerClass : MonoBehaviour
{
    public Sprite sprite;
    public GameObject spawnedEnemy;
}
  /*  public SpawnerClass(Sprite spte, GameObject go)
    {
        sprite = spte;
        spawnedEnemy = go;
    }
    public Sprite GetSprite()
    {
        return sprite;
    }
    public GameObject GetSpawnedEnemy()
    {
        return spawnedEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
