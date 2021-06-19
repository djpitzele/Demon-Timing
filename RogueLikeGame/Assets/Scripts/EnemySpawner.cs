using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /* public void spawnEnemy(Vector3 pos, System.Type scripting, Sprite s)
     {
         GameObject enemyt = new GameObject("enemy"+count);
         count++;
         //enemyt.AddComponent<Transform>();
         enemyt.transform.position = pos;
         enemyt.AddComponent(scripting);
         enemyt.AddComponent<SpriteRenderer>().sprite = s;
         enemyt.AddComponent<Rigidbody2D>();
         enemyt.AddComponent<CapsuleCollider2D>();
         enemyt.AddComponent<MeleeAttacker>();
         //Instantiate(enemyt);
     }*/
    public void spawnEnemy(GameObject prefab)
    {
        Instantiate(prefab);
    }

    void Update()
    {
        
    }
}
