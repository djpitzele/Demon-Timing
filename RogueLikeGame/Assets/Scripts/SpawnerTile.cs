using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SpawnerTile : Tile
{
    // Start is called before the first frame update
    public Sprite sprite;
    public Color color = Color.white;
    public Matrix4x4 transformb = Matrix4x4.identity;
    public GameObject gameobject = null;
    public TileFlags flags = TileFlags.LockColor;
    public List<int> wavesTillSpawn;
    public GameObject spawnedEnemy;
    public ColliderType colliderType = ColliderType.Sprite;
    public Vector3 pos;
    private Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);
    public GameObject player;
    public GameObject f;
    // Start is called before the first frame update
    public void Start()
    {
        //Debug.Log(wavesTillSpawn);
        WaveOver();
        
    }
     public SpawnerTile(Sprite s, GameObject go, List<int> w, Vector3 v, GameObject p)
    {
        sprite = s;
        spawnedEnemy = go;
        wavesTillSpawn = w;
        pos = v;
        Start();
        player = p;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (wavesTillSpawn.Count == 0)
        {
            return;
        }
        else if (wavesTillSpawn[0] == 0)
        {
            Spawn();
            wavesTillSpawn.RemoveAt(0);
        }*/
        

    }
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        tileData.sprite = this.sprite;
        tileData.color = this.color;
        tileData.transform = this.transformb;
        tileData.gameObject = this.gameobject;
        tileData.flags = this.flags;
        tileData.colliderType = this.colliderType;
    }
    public void WaveOver()
    {
        Debug.Log("wave" + player.GetComponent<PlayerClass>().totalEnemies);
        
        if (wavesTillSpawn.Count == 0)
        {
            return;
        }
        
        else if (wavesTillSpawn[0] == 0)
        {
            Spawn();
            wavesTillSpawn.RemoveAt(0);
        }
        else
        {
            wavesTillSpawn[0] -= 1;
            //Debug.Log("Deez" + wavesTillSpawn[0]);
        }

    }
    public void Spawn()
    {
        GameObject temp = Instantiate(spawnedEnemy, pos + offset, Quaternion.identity);
        temp.GetComponent<EntityClass>().setPlayer(player);
        player.GetComponent<PlayerClass>().totalEnemies++;
    }
   
}
