using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Vector2 _area;
    [SerializeField] int spawnBatch = 2;

    public void Spawn(EnemyCore core, int amount) => StartCoroutine(IterateSpawn(core, amount));
    IEnumerator IterateSpawn(EnemyCore core, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(core,
                (Vector2)transform.position + new Vector2(Random.Range(-_area.x/2, _area.x/2), Random.Range(-_area.y/2, _area.y/2)),
                Quaternion.identity
            );

            if(i % spawnBatch == 0)
                yield return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _area);
    }
}
