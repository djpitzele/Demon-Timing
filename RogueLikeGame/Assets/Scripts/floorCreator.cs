using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class floorCreator : MonoBehaviour
{
    public Tile daFloor;
    // Start is called before the first frame update
    void Start()
    {
        Tilemap tm = this.gameObject.GetComponent<Tilemap>();
        Vector3Int starter = new Vector3Int(0, 0, 0);
        var otherTiles = GameObject.Find("PillarTilemap").GetComponent<TileSetter>();
        Vector3Int ender = new Vector3Int(otherTiles.getWidth() - 1, otherTiles.getHeight() - 1, 0);
        tm.BoxFill(ender, daFloor, starter.x, starter.y, ender.x, ender.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
