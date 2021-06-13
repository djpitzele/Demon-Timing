using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : MonoBehaviour
{
    public GameObject myPlayer;
    public GameObject thePillarGrid;
    private int timeTilFind = 0;
    private Vector3 destination;
    private ContactFilter2D filter;
    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
        filter = new ContactFilter2D();
        filter.useLayerMask = true;
        filter.SetLayerMask((LayerMask)3);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeTilFind == 0)
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
            HashSet<Vector2> closedList = new HashSet<Vector2>();
            List<Vector2> openList = new List<Vector2>();
            List<Vector2> curPath = new List<Vector2>();
            Vector3Int temporary = thePillarGrid.GetComponent<Grid>().WorldToCell(transform.position);
            Vector2 startPos = new Vector2(temporary.x, temporary.y);
            Vector3Int temporary2 = thePillarGrid.GetComponent<Grid>().WorldToCell(myPlayer.transform.position);
            Vector2 endPos = new Vector2(temporary2.x, temporary2.y);

        }
    }
}
