using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Scripting;
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
        //Debug.Log("wave" + player.GetComponent<PlayerClass>().totalEnemies);
        
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
        GameObject temp = Instantiate(spawnedEnemy, pos, Quaternion.identity);
        temp.GetComponent<EntityClass>().setPlayer(player);
        PlayerClass.main.GetComponent<PlayerClass>().totalEnemies++;
    }
   
}
