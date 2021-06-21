using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SpawnerTile : Tile
{
    // Start is called before the first frame update
    public Sprite sprite;
    public Color color = Color.white;
    public Matrix4x4 transform = Matrix4x4.identity;
    public GameObject gameobject = null;
    public TileFlags flags = TileFlags.LockColor;
    public List<int> wavesTillSpawn;
    public GameObject spawnedEnemy;
    public ColliderType colliderType = ColliderType.Sprite;
  
    // Start is called before the first frame update
    void Start()
    {

    }
    public SpawnerTile(Sprite s, GameObject go, List<int> w)
    {
        sprite = s;
        spawnedEnemy = go;
        wavesTillSpawn = w;

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
        tileData.transform = this.transform;
        tileData.gameObject = this.gameobject;
        tileData.flags = this.flags;
        tileData.colliderType = this.colliderType;
    }
    public void WaveOver()
    {
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
        }
    }
    public void Spawn()
    {
        Instantiate(spawnedEnemy);
    }
   
}
