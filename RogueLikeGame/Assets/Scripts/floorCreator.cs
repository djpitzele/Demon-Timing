using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class floorCreator : MonoBehaviour
{
    public Tile daFloor;
    public List<SpawnerClass> spawners;
    public Tilemap floorPillars;
    private int width;
    private int height;
    private TileSetter otherTiles;
    public int spawnersLeft;
    private Tilemap tm;
    System.Random r = new System.Random();
    // for funsies
    private HashSet<Vector2> spawnerSet;
    
    // Start is called before the first frame update
    void Start()
    {
        tm = this.gameObject.GetComponent<Tilemap>();
        Vector3Int starter = new Vector3Int(0, 0, 0);
        otherTiles = floorPillars.gameObject.GetComponent<TileSetter>();
        Vector3Int ender = new Vector3Int(otherTiles.getWidth() - 1, otherTiles.getHeight() - 1, 0);
        tm.BoxFill(ender, daFloor, starter.x, starter.y, ender.x, ender.y);
        width = otherTiles.getWidth();
        height = otherTiles.getHeight();
        for (int i = 0; i < height; i++)
        {
            genRowSpawners(i);
            //Debug.Log(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void genRowSpawners(int curHeight)
    {
        int streak = 0;
        //float totalPillars = pillarsLeft;
        for (int i = 0; i < width; i++)
        {
            Vector3Int v = new Vector3Int(i, curHeight, 0);
            if (otherTiles.spawnerPossible(i, curHeight, spawnersLeft))
            {
                int x = r.Next(1000);
                //Debug.Log(streak);
                //Debug.Log(x);
                if (x > 885 || streak >= 5)
                {
                    //Debug.Log(x);
                    //Debug.Log(((pillarsLeft / (width * height)) * 80000));
                    tm.SetTile(v, CHOOSESPAWNER());
                    //tileCost[x][y] = 10000000;
                    streak = 0;
                    spawnersLeft--;
                    Vector2 temp = new Vector2(v.x, v.y);
                    spawnerSet.Add(temp);
                }
                else
                {
                    streak++;
                    //tm.SetTile(v, floor);
                }
            }
            else
            {
                //tm.SetTile(v, floor);
            }
            //Debug.Log(pillarsLeft);
        }
    }
    private Tile CHOOSESPAWNER()
    {
        SpawnerClass temp = spawners[r.Next(spawners.Count)];
        return new SpawnerTile(temp.sprite, temp.spawnedEnemy, genWaves());
    }
    private List<int> genWaves()
    {
        return new List<int>{ 1, 1, 1, 1 }; 
    }

}
