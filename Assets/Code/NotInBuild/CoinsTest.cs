using System.Collections;
using UnityEngine;

public class CoinsTest : MonoBehaviour
{
    [SerializeField] Vector2 _area;
    [SerializeField] int _amount = 200;
    [SerializeField] int _spawnBatch = 3;
    [SerializeField] float _startDelay = 1f;
    [SerializeField] Coin _coin;
    [SerializeField] int _spawned = 0;

    void Start() => StartCoroutine(IterateSpawn());
    IEnumerator IterateSpawn()
    {
        yield return new WaitForSecondsRealtime(_startDelay);
        for(int i = 0; i < _amount; i++)
        {
            Coin spawnedCoin = Instantiate(_coin,
                new Vector2(Random.Range(-_area.x/2, _area.x/2), Random.Range(-_area.y/2, _area.y/2)),
                Quaternion.identity
            );
            Color color;
            color.r = 1;
            color.g = Random.Range(0.05f, 1f); //Random flipbook offset
            color.b = Random.Range(0.05f, 1f); //Random Jump offset
            color.a = 1;

            spawnedCoin.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = color;
            _spawned = i+1;
            if(i % _spawnBatch == 0)
                yield return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector2.zero, _area);
    }
}
