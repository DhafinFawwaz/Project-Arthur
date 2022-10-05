using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    PlayerCore _core;
    public PlayerCore Core{get{return _core;}}


    [Header("HitLag Duration")]
    [SerializeField] float _instantHitLagDuration = 0f;
    [SerializeField] float _weakHitLagDuration = 0.05f;
    [SerializeField] float _mediumHitLagDuration = 0.15f;
    [SerializeField] float _strongHitLagDuration = 0.4f;
    
    [Header("HitLag TimeScale")]
    [SerializeField] float _instantHitLagTimeScale = 1f;
    [SerializeField] float _weakHitLagTimeScale = 0.4f;
    [SerializeField] float _mediumHitLagTimeScale = 0.3f;
    [SerializeField] float _strongHitLagTimeScale = 0.1f;

    [Header("Particle Effects")]
    [SerializeField] ParticleSystem _deathParticle;
    [SerializeField] ParticleSystem _hitEffect;
    [SerializeField] ParticleSystem _killParticle;

    [Header("Spawner")]
    [SerializeField] GameObject _enemyBanditSmall;
    [SerializeField] int amount;
    [SerializeField] Vector2 _spawnArea;

    [Header("Stat")]
    [SerializeField] int _coinAmount = 0;
    [SerializeField] int _currentLevel = 0;
    public int CoinAmount{get{return _coinAmount;} set{_coinAmount = value;}}
    [SerializeField] Transform _coinAnchor;
    [SerializeField] Transform _coinCollectPoint;
    public Transform CoinCollectPoint{get{return _coinCollectPoint;}}
    [SerializeField] TextMeshProUGUI _coinAmountText;

    [Header("Coin Drop")]
    [SerializeField] CollectiblesManager _collectiblesManager;
    public CollectiblesManager CollectiblesManager{get{return _collectiblesManager;}}

    [Header("Map")]
    [SerializeField] MapManager _mapManager;

    [Header("SFX")]
    public AudioClip KillSFX;
    public AudioClip HitSFX;
    public AudioClip CoinSFX;


    
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SpawnEnemies();
        Singleton.Instance.Resolution.SetResolutionPercentage(1, 2);
        Cursor.visible = false;
        _core = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCore>();
        _mapManager.OnSceneLoaded();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnEnemies();
        Singleton.Instance.Resolution.SetResolutionPercentage(1, 2);
        Cursor.visible = false;
        _core = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCore>();
        _mapManager.OnSceneLoaded();
    }
    void SpawnEnemies()
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(_enemyBanditSmall, 
            new Vector2(Random.Range(-_spawnArea.x/2, _spawnArea.x/2), 
                Random.Range(-_spawnArea.y/2, _spawnArea.y/2)), Quaternion.identity);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector2.zero, _spawnArea);
    }



    public void InstantHitLag() => StartCoroutine(HitLag(_instantHitLagDuration, _instantHitLagTimeScale));
    public void WeakHitLag() => StartCoroutine(HitLag(_weakHitLagDuration, _weakHitLagTimeScale));
    public void MediumHitLag() => StartCoroutine(HitLag(_mediumHitLagDuration, _mediumHitLagTimeScale));
    public void StrongHitLag() => StartCoroutine(HitLag(_strongHitLagDuration, _strongHitLagTimeScale));

    ushort _key;
    IEnumerator HitLag(float duration, float timeScale)
    {
        _key++;
        ushort requirement = _key;
        Time.timeScale = timeScale;
        yield return new WaitForSecondsRealtime(duration);
        if(requirement == _key)Time.timeScale = 1;
    }

    public void PlayDeathParticle(Vector2 position)
    {
        _deathParticle.transform.position = position;
        _deathParticle.Play();
    }
    public void PlayKillParticle(Vector2 position)
    {
        Singleton.Instance.Audio.PlaySound(KillSFX);
        _killParticle.transform.position = position;
        _killParticle.Play();
    }
    public void PlayHitParticle(Vector3 position)
    {
        Singleton.Instance.Audio.PlaySound(HitSFX);
        _hitEffect.transform.position = position;
        _hitEffect.Play();
    }


    public void AddCoins(int value)
    {
        _coinAmount += value;
        _coinAmountText.text = "× " + _coinAmount.ToString();
        StartCoroutine(CoinAddedAnimation());
    }

    ushort _keyCoin;
    IEnumerator CoinAddedAnimation()
    {
        float t = 0;
        _keyCoin++;
        ushort requirement = _keyCoin;
        while(t <= 1 && requirement == _keyCoin)
        {
            t += Time.deltaTime/0.3f;
            _coinAnchor.localScale = Vector3.Lerp(Vector3.one*1.7f, Vector3.one, Ease.OutQuad(t));
            yield return null;
        }
    }

    // public void Upgrade

    void Update()//Everything will use this for optimization
    {
        _mapManager.GameUpdate();

    }
}
