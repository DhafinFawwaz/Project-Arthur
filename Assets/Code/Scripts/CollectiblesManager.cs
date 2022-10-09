using UnityEngine;
using UnityEngine.Pool;

public class CollectiblesManager : MonoBehaviour
{
    [SerializeField] int _coinCapacity = 100;
    [SerializeField] int _MaxCoinCapacity = 200;
    [SerializeField] Coin _coinPrefab;
    ObjectPool<Coin> _coinPool;

    public void OnSceneLoaded()
    {
        _coinPool = new ObjectPool<Coin>(() => {
            return Instantiate(_coinPrefab);
            }, coin => {
                coin.gameObject.SetActive(true);
            }, coin => {
                coin.gameObject.SetActive(false);
            }, coin => {
                Destroy(coin.gameObject);
            }, false, _coinCapacity, _MaxCoinCapacity
        );


    }

    public void DropCoins(int amount, Vector2 position)
    {
        for(int i = 0; i < amount; i++)
        {
            Coin spawnedCoin = _coinPool.Get();
            spawnedCoin.transform.position = position;
            spawnedCoin.OnSpawn();
        }
    }
    public void DestroyCoins(Coin coin) => _coinPool.Release(coin);
}
