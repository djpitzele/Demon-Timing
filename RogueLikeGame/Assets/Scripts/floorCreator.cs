using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class floorCreator : MonoBehaviour
{
    public Tile daFloor;
    private List<SpawnerClass> spawners;
    public List<Sprite> spritesToMatch;
    public List<GameObject> enemiesToMatch;
    public Tilemap floorPillars;
    private int width;
    private int height;
    private TileSetter otherTiles;
    public int spawnersLeft;
    private Tilemap tm;
    System.Random r = new System.Random();
    public int AvgMobs;
    public int waves;
    private int initialSpawnersLeft;
    // for funsies
    //private HashSet<Vector2> spawnerSet;
    
    // Start is called before the first frame update
    void Start()
    {
        spawners = new List<SpawnerClass>();
        for (int i = 0; i < spritesToMatch.Count; i++)
        {
            spawners.Add(new SpawnerClass(spritesToMatch[i], enemiesToMatch[i]));
        }
        tm = this.gameObject.GetComponent<Tilemap>();
        Vector3Int starter = new Vector3Int(0, 0, 0);
        otherTiles = floorPillars.gameObject.GetComponent<TileSetter>();
        Vector3Int ender = new Vector3Int(otherTiles.getWidth() - 1, otherTiles.getHeight() - 1, 0);
        tm.BoxFill(ender, daFloor, starter.x, starter.y, ender.x, ender.y);
        width = otherTiles.getWidth();
        height = otherTiles.getHeight();
        initialSpawnersLeft = spawnersLeft;
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
                if (x > 950)
                {
                    //Debug.Log(x);
                    //Debug.Log(((pillarsLeft / (width * height)) * 80000));
                    tm.SetTile(v, chooseSpawner(v));
                    //tileCost[x][y] = 10000000;
                    streak = 0;
                    spawnersLeft--;
                    Vector2 temp = new Vector2(v.x, v.y);
                    //spawnerSet.Add(temp);
                }
            }
        }
    }
    private Tile chooseSpawner(Vector3Int v)
    {
        //SpawnerClass temp = spawners[r.Next(spawners.Count)];
        SpawnerClass temp = spawners[0];
        Vector3 changedV = floorPillars.gameObject.GetComponentInParent<Grid>().CellToWorld(v);
        return new SpawnerTile(temp.sprite, temp.spawnedEnemy, genWaves(AvgMobs, waves, spawners.Count), changedV);
    }
    private List<int> genWaves(int a, int w, int s)
    {
        int d = 0;
        List<int> timeBetween = new List<int>();
        for(int i = 0; i < w; i++)
        {
            int b = r.Next(1000);
            //CHANGE 1 ON NEXT LINE TO DECREASE CHANCE OF SPAWN
            if (b > (a * 1) / (w * s))
            {
                timeBetween.Add(d);
                d = 0;
            }
            else
            {
                d++;
            }
        }
        return timeBetween;
    }

}
