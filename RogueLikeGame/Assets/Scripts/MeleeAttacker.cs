using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeAttacker : MonoBehaviour
{
    public GameObject myPlayer;
    public GameObject thePillarGrid;
    //timeTilFind is in seconds
    private float timeTilFind;
    private Vector3 destination;
    private ContactFilter2D filter;
    private List<Vector2> path;
    private int goingTowards = 0;
    private Grid theGrid;
    private Vector2 offset = new Vector2(0.5f, 0.5f);
    public float meleeSpeed;
    private HashSet<Vector2> wrongpath;
    private Vector2 oldPlayerPos;
    private bool start;
    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
        filter = new ContactFilter2D();
        filter.useLayerMask = true;
        filter.SetLayerMask((LayerMask)3);
        theGrid = thePillarGrid.GetComponent<Grid>();
        wrongpath = new HashSet<Vector2>();
        timeTilFind = 0;
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        //TO DO: ONLY ADJACENT SQUARES CAN BE GONE TO
        //Debug.Log(path[0]);
        if (timeTilFind <= 0 && (ourWorldToCell((Vector2)myPlayer.transform.position) != oldPlayerPos || start))
        {
            /*RaycastHit2D[] results = new RaycastHit2D[1];
            int blocksInFront = Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(myPlayer.transform.position.x, myPlayer.transform.position.y), filter, results);
            if(blocksInFront == 0)
            {
                destination = myPlayer.transform.position;
            }
            else
            {

            }*/
            Vector2[] posns = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
            HashSet<Vector2> closedList = new HashSet<Vector2>();
            closedList.UnionWith(thePillarGrid.GetComponentInChildren<TileSetter>().getPillars());
            closedList.UnionWith(wrongpath);
            List<Vector2> openList = new List<Vector2>();
            List<Vector2> curPath = new List<Vector2>();
            Vector3Int temporary = theGrid.WorldToCell(transform.position);
            Vector2 startPos = new Vector2(temporary.x, temporary.y);
            Vector3Int temporary2 = theGrid.WorldToCell(myPlayer.transform.position);
            Vector2 endPos = new Vector2(temporary2.x, temporary2.y);
            oldPlayerPos = endPos;
            Vector2 curVector = new Vector2(-10000, -10000);
            Vector2 tempPosn = ourWorldToCell(transform.position);
            foreach (Vector2 v in posns)
            {
                if (!(closedList.Contains(tempPosn + v)))
                {
                    openList.Add(tempPosn + v);
                }
            }
            while (openList.Count != 0 && curVector != endPos)
            {
                int minIndex = -1;
                int minCost = int.MaxValue;
                for (int i = 0; i < openList.Count; i++)
                {
                    curVector = openList[i];
                    Vector2 fgetter = (startPos - curVector);
                    int f = (int)(Math.Abs((decimal)fgetter.x) + Math.Abs((decimal)fgetter.y));
                    Vector2 gGetter = (endPos - curVector);
                    int g = (int)(Math.Abs((decimal)gGetter.x) + Math.Abs((decimal)gGetter.y));
                    int h = f + g;
                    if (h < minCost)
                    {
                        minIndex = i;
                        minCost = h;
                    }
                }

                
                curPath.Add(openList[minIndex]);
                closedList.Add(openList[minIndex]);
                curVector = openList[minIndex];
                openList.RemoveAt(minIndex);
                openList.Clear();
                foreach (Vector2 v in posns)
                {
                    if (!(closedList.Contains(curVector + v)))
                    {
                        
                        openList.Add(curVector + v);
                    }
                }
               // Debug.Log(curPath[0]);
                if (openList.Count == 0)
                {
                    wrongpath.Add(curPath[curPath.Count - 1]);
                    Update();
                    return;
                }
            }
            if(openList.Count == 0)
            {
                Debug.Log("boxed like a fish");
                Destroy(this);
            }
            goingTowards = 0;
            path = curPath;
            timeTilFind = 0.5f;
        }
        if((Vector2)transform.position == ourCellToWorld(path[goingTowards]) + offset && path.Count - 1 != goingTowards)
        {
           
            goingTowards++;
        }
        else
        {
            moveTowardsNext();
        }
        timeTilFind -= Time.deltaTime;
        //Debug.Log(timeTilFind);
    }

    public void moveTowardsNext()
    {
        Vector2 nextPoint = path[goingTowards];
        transform.position = Vector2.MoveTowards(transform.position, ourCellToWorld(nextPoint) + offset, meleeSpeed * Time.deltaTime);
    }

    private Vector2 ourCellToWorld(Vector2 v)
    {
        Vector3Int newV = new Vector3Int((int)v.x, (int)v.y, 0);
        Vector3 temp = theGrid.CellToWorld(newV);
        return (new Vector2(temp.x, temp.y));
    }
    private Vector2 ourWorldToCell(Vector2 v)
    {
        Vector3 newV = new Vector3((int)v.x, (int)v.y, 0);
        Vector3Int temp = theGrid.WorldToCell(newV);
        return (new Vector2(temp.x, temp.y));
    }
}
