using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Random;
using UnityEngine.Tilemaps;
using System;

public class TileSetter : MonoBehaviour
{
    //public int[][] tileCost;
    public int width;
    public int height;
    public int pillarsLeft;
    public Tile floor;
    public Tile pillar;
    public Tilemap tm = new Tilemap();
    private HashSet<Vector2> pillarSet;
    System.Random r = new System.Random();
    public int getWidth()
    {
        return width;
    }

    public int getHeight()
    {
        return height;
    }
    // Start is called before the first frame update
    void Start()
    {
        pillarSet = new HashSet<Vector2>();
        //pillarsLeft = (int)((((width - 1) * (height - 1)) * 0.16) + 0.5);
        Tilemap tm = GetComponent<Tilemap>();
        //Tile floor = (Tile)Resources.Load("smile");
        //Tile pillar = (Tile)Resources.Load("blackSquare");
        for(int i = 0; i < height; i++)
        {
            genRowPillars(i);
            //Debug.Log(i);
        }
    }
    
    public HashSet<Vector2> getPillars()
    {
        return pillarSet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool pillarPossible(int x, int y, int pillarsLeft)
    {
        if (pillarsLeft == 0)
        {
            return false;
        }
        else if (x == 0 || y == 0 || x == width-1 || y == height-1)
        {
            return false;
        }
        /*else if(tm.GetTile(new Vector3Int(x-1, y-1, 0)) != pillar && tm.GetTile(new Vector3Int(x, y - 1, 0)) != pillar && tm.GetTile(new Vector3Int(x + 1, y - 1, 0)) != pillar && tm.GetTile(new Vector3Int(x - 1, y, 0)) != pillar)
        {
            return true;
        }*/
        else
        {
            return true;
        }
    }
    public bool spawnerPossible(int x, int y, int spawnersLeft)
    {
        Vector3Int v = new Vector3Int(x, y, 0);
        if(spawnersLeft == 0)
        {
            return false;
        }
        if(tm.GetTile(v) == pillar)
        {
            return false;
        }
        else
        {
            return (tm.GetTile(v + Vector3Int.up) != pillar || tm.GetTile(v + Vector3Int.left) != pillar || tm.GetTile(v + Vector3Int.right) != pillar || tm.GetTile(v + Vector3Int.down) != pillar);
        }
        /*return (tm.GetTile(new Vector3Int(x - 1, y - 1, 0)) != pillar && tm.GetTile(new Vector3Int(x, y - 1, 0)) != pillar && tm.GetTile(new Vector3Int(x + 1, y - 1, 0)) != pillar && tm.GetTile(new Vector3Int(x - 1, y, 0)) != pillar &&
            tm.GetTile(new Vector3Int(x, y, 0)) != pillar && tm.GetTile(new Vector3Int(x + 1, y, 0)) != pillar && tm.GetTile(new Vector3Int(x - 1, y + 1, 0)) != pillar && tm.GetTile(new Vector3Int(x, y + 1, 0)) != pillar &&
            tm.GetTile(new Vector3Int(x + 1, y + 1, 0)) != pillar);*/
    }
    public void genRowPillars(int curHeight)
    {
        int streak = 0;
        //float totalPillars = pillarsLeft;
        for(int i = 0; i < width; i++)
        {
            Vector3Int v = new Vector3Int(i, curHeight, 0);
            if (pillarPossible(i, curHeight, pillarsLeft)) {
                int x = r.Next(1000);
                //Debug.Log(streak);
                //Debug.Log(x);
                if (x > 885 || streak >= 5)
                {
                    //Debug.Log(x);
                    //Debug.Log(((pillarsLeft / (width * height)) * 80000));
                    tm.SetTile(v, pillar);
                    //tileCost[x][y] = 10000000;
                    streak = 0;
                    pillarsLeft--;
                    Vector2 temp = new Vector2(v.x, v.y);
                    pillarSet.Add(temp);
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
}
