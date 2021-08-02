using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class floorCreator : MonoBehaviour
{
    public Tile theFloor;
    public Tile topWallTile;
    public Tile rightWallTile;
    public Tile leftWallTile;
    public Tile botWallTile;
    public Tile LeftbotWallcornerTile;
    public Tile RightbotWallcornerTile;
    public Tile RightTopWallcornerTile;
    public Tile LeftTopWallcornerTile;
    private List<SpawnerClass> spawners;
    public List<Sprite> spritesToMatch;
    public List<GameObject> enemiesToMatch;
    public Tilemap floorPillars;
    private int width;
    private int height;
    private TileSetter otherTiles;
    public int spawnersLeft;
    private Tilemap tm;
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
    public static System.Random r = new System.Random();

    // for funsies
    //private HashSet<Vector2> spawnerSet;ev

    // Start is called before the first frame update
    void Start()
    {
        camera =  Camera.main.gameObject;
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
        genWalls();
        int larger = Math.Max(width, height);
        camera.transform.position = new Vector3(width / 2.0f, height / 2.0f, -10);
        camera.GetComponent<Camera>().orthographicSize = (camScaling / 10.0f) * larger;
        Vector2[] thePoints = { new Vector2(0, 0), new Vector2(width, 0), new Vector2(width, height), new Vector2(0, height), new Vector2(0, 0) };
        theWalls = GameObject.Find("Walls");
        theWalls.GetComponent<EdgeCollider2D>().points = thePoints;
        if(TryGetComponent<RedDragonFloor>(out RedDragonFloor rdf)) {
            rdf.changeCamera(camera);
        }
      
        StartCoroutine("beAsleep");
    }

    public IEnumerator nextWave(List<SpawnerTile> sts)
    {
        startChecking = false;
        yield return new WaitForSeconds(0.5f);
        foreach (SpawnerTile t in sts)
        {
            t.WaveOver();
        }
        startChecking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startChecking && player.GetComponent<PlayerClass>().totalEnemies == 0 && waves > 0)
        {
            StartCoroutine("nextWave", spawnerTiles);
            waves--;
            if(waves == 0)
            {
                player.GetComponent<PlayerClass>().curShade += 1;
                player.GetComponent<PlayerClass>().theCanvas.GetComponentsInChildren<ShadeScript>()[0].updateShade();
            }
        }

    }
    public void genRowSpawners(int curHeight)
    {
        //float totalPillars = pillarsLeft;
        for (int i = 0; i < width; i++)
        {
            Vector3Int v = new Vector3Int(i, curHeight, 0);
            if (otherTiles.spawnerPossible(i, curHeight, spawnersLeft))
            {
                int x = r.Next(1000);
                //Debug.Log(streak);
                //Debug.Log(x);
                if (x < (spawnersLeft * 2000) / (width * height))
                {
                    //Debug.Log(x);
                    //Debug.Log(((pillarsLeft / (width * height)) * 80000));
                    SpawnerTile t = chooseSpawner(v);
                    spawnerTiles.Add(t);
                    tm.SetTile(v, t);
                    //tileCost[x][y] = 10000000;
                    spawnersLeft--;
                    Vector2 temp = new Vector2(v.x, v.y);
                    //spawnerSet.Add(temp);
                }
            }
        }
    }
    public void genWalls()
    {
        Vector3Int[] topWalls = new Vector3Int[width]; 
        Tile[] topWallTiles = new Tile[width];
        for (int i = 0; i < width; i++)
        {
            topWalls[i] = new Vector3Int(i, height, 0);
            topWallTiles[i] = topWallTile;
        }
        Vector3Int[] botWalls = new Vector3Int[width];
        Tile[] botWallTiles = new Tile[width];
        for (int i = 0; i < width; i++)
        {
            botWalls[i] = new Vector3Int(i, -1, 0);
            botWallTiles[i] = botWallTile;
        }
        Vector3Int[] leftWalls = new Vector3Int[height];
        Tile[] leftWallTiles = new Tile[height];
        for (int i = 0; i < height; i++)
        {
            leftWalls[i] = new Vector3Int(-1, i , 0);
            leftWallTiles[i] = leftWallTile;
        }
        Vector3Int[] rightWalls = new Vector3Int[height];
        Tile[] rightWallTiles = new Tile[height];
        for (int i = 0; i < height; i++)
        {
            rightWalls[i] = new Vector3Int(width, i, 0);
            rightWallTiles[i] = rightWallTile;
        }
        Vector3Int[] corners = new Vector3Int[4];
        corners[0] = new Vector3Int(-1, -1, 0);
        corners[1] = new Vector3Int(width, -1, 0);
        corners[2] = new Vector3Int(width, height, 0);
        corners[3] = new Vector3Int(-1, height, 0);
        Tile[] cornerWallTiles = { LeftbotWallcornerTile, RightbotWallcornerTile, RightTopWallcornerTile, LeftTopWallcornerTile };
        tm.SetTiles(topWalls, topWallTiles);
        tm.SetTiles(botWalls, botWallTiles);
        tm.SetTiles(leftWalls, leftWallTiles);
        tm.SetTiles(rightWalls, rightWallTiles);
        tm.SetTiles(corners, cornerWallTiles);

      
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
        t.wavesTillSpawn = genWaves(AvgMobs, waves, initialSpawnersLeft);
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
            //CHANGE 1 ON NEXT LINE TO DECREASE CHANCE OF SPAWN
            if (r.Next(1000) <= (a * 1000) / s)
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
