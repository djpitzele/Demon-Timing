using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class floorCreator : MonoBehaviour
{
    public Tile theFloor;
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
    public GameObject player;
    public List<SpawnerTile> spawnerTiles;
    private bool startChecking = false;
    private Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);
    public GameObject camera;
    public float camScaling;
    public GameObject theWalls;
    // for funsies
    //private HashSet<Vector2> spawnerSet;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MainChar");
        spawners = new List<SpawnerClass>();
        for (int i = 0; i < spritesToMatch.Count; i++)
        {
            spawners.Add(new SpawnerClass(spritesToMatch[i], enemiesToMatch[i]));
        }
        tm = this.gameObject.GetComponent<Tilemap>();
        Vector3Int starter = new Vector3Int(0, 0, 0);
        otherTiles = floorPillars.gameObject.GetComponent<TileSetter>();
        Vector3Int ender = new Vector3Int(otherTiles.getWidth() - 1, otherTiles.getHeight() - 1, 0);
        tm.BoxFill(ender, theFloor, starter.x, starter.y, ender.x, ender.y);
        width = otherTiles.getWidth();
        height = otherTiles.getHeight();
        //Debug.Log(width);
        initialSpawnersLeft = spawnersLeft;
        for (int i = 0; i < height; i++)
        {
            genRowSpawners(i);
            //Debug.Log(i);
        }
        int larger = Math.Max(width, height);
        camera.transform.position = new Vector3(width / 2.0f, height / 2.0f, -10);
        camera.GetComponent<Camera>().orthographicSize = (camScaling / 10.0f) * larger;
        Vector2[] thePoints = { new Vector2(0, 0), new Vector2(width, 0), new Vector2(width, height), new Vector2(0, height), new Vector2(0, 0) };
        theWalls.GetComponent<EdgeCollider2D>().points = thePoints;
        if(TryGetComponent<RedDragonFloor>(out RedDragonFloor rdf)) {
            rdf.changeCamera(camera);
        }
        StartCoroutine("beAsleep");
    }

    // Update is called once per frame
    void Update()
    {
        if (startChecking && player.GetComponent<PlayerClass>().totalEnemies == 0 && waves > 0)
        {
            foreach (SpawnerTile t in spawnerTiles)
            {
                t.WaveOver();
            }
            waves--;
            

        }

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
                if (x < 6000 / (width * height))
                {
                    //Debug.Log(x);
                    //Debug.Log(((pillarsLeft / (width * height)) * 80000));
                    SpawnerTile t = chooseSpawner(v);
                    spawnerTiles.Add(t);
                    tm.SetTile(v, t);
                    //tileCost[x][y] = 10000000;
                    streak = 0;
                    spawnersLeft--;
                    Vector2 temp = new Vector2(v.x, v.y);
                    //spawnerSet.Add(temp);
                }
            }
        }
    }

    private IEnumerator beAsleep()
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == 1)
            {
                foreach (SpawnerTile t in spawnerTiles)
                {
                    t.WaveOver();
                }
            }
            yield return new WaitForSecondsRealtime(1f);
        }
        startChecking = true;
    }

    private SpawnerTile chooseSpawner(Vector3Int v)
    {
        //SpawnerClass temp = spawners[r.Next(spawners.Count)];
        SpawnerClass temp = spawners[r.Next(spawners.Count)];
        Vector3 changedV = floorPillars.gameObject.GetComponentInParent<Grid>().CellToWorld(v) + offset;
        SpawnerTile t = (SpawnerTile)ScriptableObject.CreateInstance("SpawnerTile");// SpawnerTile(temp.sprite, temp.spawnedEnemy, genWaves(AvgMobs, waves, spawners.Count), changedV);
        t.sprite = temp.sprite;
        t.spawnedEnemy = temp.spawnedEnemy;
        t.wavesTillSpawn = genWaves(AvgMobs, waves, spawners.Count);
        t.pos = changedV;
        t.player = player;
        t.f = this.gameObject;
       
        return t;
    }
    private List<int> genWaves(int a, int w, int s)
    {
        int d = 0;
        List<int> timeBetween = new List<int>();
        for(int i = 0; i < w; i++)
        {
            int b = r.Next(1000);
            //CHANGE 1 ON NEXT LINE TO DECREASE CHANCE OF SPAWN
            if (b > (a * 500) / (w * s))
            {
                //Debug.Log(d);
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
