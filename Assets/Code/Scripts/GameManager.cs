using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    
    [Header("Cursor")]
    public RectTransform CursorPoint;
    public RectTransform RightCursor;
    public RectTransform UpCursor;
    public RectTransform LeftCursor;
    public RectTransform DownCursor;


    [Header("Map")]
    [SerializeField] MapManager _mapManager;
    
    [Header("Timer")]
    [SerializeField] TimeManager _timeManager;
    
    [Header("Spawner")]
    [SerializeField] EnemyBanditSmallCore _enemyBanditSmallCorePrefab;
    [SerializeField] EnemySpawner[] _enemySpawner;

    [Header("GameStates")]
    int _waveCurrent = -1; //0 1 2 3 4. Start with -1 because it will increase to 0 at first
    int _waveCurrentKillCount = 0;
    public int WaveCurrentKillCount{
        get{return _waveCurrentKillCount;}
        set{
            _waveCurrentKillCount = value;
            _waveKillCountText.text = value.ToString() + "/" + _waveMaxEnemies[_waveCurrent].ToString();  
            
            if(_waveCurrentKillCount >= _waveMaxEnemies[_waveCurrent])
            {
                if(_waveCurrent == _waveMaxEnemies.Length-2) //Has just completed Wave 4
                    StartCoroutine(FinalWaveAnimation());
                else if(_waveCurrent == _waveMaxEnemies.Length-1) //Has just completed Wave 5
                    StartCoroutine(WinAnimation());
                else
                    StartCoroutine(WaveAnimation());// The usual waves
            }
        }
    }
    [SerializeField] int[] _waveMaxEnemies = new int[5];
    [SerializeField] Sprite[] _waveSprite = new Sprite[5];
    [SerializeField] TextMeshProUGUI _waveKillCountText;
    [SerializeField] Sprite _waveFinalSprite;

    [SerializeField] Image _waveImg;
    [SerializeField] RectTransform _waveRt;

    [Header("Win")]
    [SerializeField] RectTransform _winRt;

    [Header("HUD")]
    public Transform HealthFill;
    public Transform HealthGhost;
    public Transform ManaFill;
    public Transform ManaGhost;
    public Transform EnergyFill;
    public Transform EnergyGhost;

    [SerializeField] TextMeshProUGUI _finalCoinAmountTimeText;
    [SerializeField] TextMeshProUGUI _finalTimeText;
    [SerializeField] TextMeshProUGUI _finalFastestTimeText;
    [SerializeField] Image _finalScoreImg;
    [SerializeField] Sprite[] _finalScoreSprite = new Sprite[4];//0 = A, 1 = B, 2 = C, 3 = D

    
    [Header("Shop")]
    [SerializeField] RectTransform _blacksmithRt;
     
    [Header("Particle Effects")]
    [SerializeField] ParticleSystem _deathParticle;
    [SerializeField] ParticleSystem _hitEffect;
    [SerializeField] ParticleSystem _killParticle;
    
    [Header("SFX")]
    public AudioClip KillSFX;
    public AudioClip HitSFX;
    public AudioClip CoinSFX;
    public AudioClip SlashSFX;
    public AudioClip RankASFX;
    public AudioClip SuperSlashSFX;
    public AudioClip NextWaveSFX;
    public AudioClip WinSFX;


#region General
    // void Awake() => SceneManager.sceneLoaded += OnSceneLoaded;

    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     Singleton.Instance.Resolution.SetResolutionPercentage(1, 2);
    //     Cursor.visible = false;
    //     _core = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCore>();
    //     _mapManager.OnSceneLoaded();
    //     _timeManager.OnSceneLoaded();
    //     _collectiblesManager.OnSceneLoaded();
    //     StartGame();

    //     _waveCurrent = -1;
    //     _waveCurrentKillCount = 0;
    //     _waveImg.gameObject.SetActive(false);
    //     _waveKillCountText.gameObject.SetActive(false);

    //     GameObject spawnerParent = GameObject.FindGameObjectWithTag("Spawner");
    //     for(int i = 0; i < _enemySpawner.Length; i++)
    //     {
    //         _enemySpawner[i] = spawnerParent.transform.GetChild(i).GetComponent<EnemySpawner>();
    //     }

    //     _winRt.gameObject.SetActive(false);
    //     _finalScoreImg.gameObject.SetActive(false);


    // }
    public void OnEnable()
    {
        // Singleton.Instance.Resolution.SetResolutionPercentage(1, 2);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)return;

        Cursor.visible = false;
        _core = player.GetComponent<PlayerCore>();
        _mapManager.OnSceneLoaded();
        _timeManager.OnSceneLoaded();
        _collectiblesManager.OnSceneLoaded();
        StartGame();

        _waveCurrent = -1;
        _waveCurrentKillCount = 0;
        _waveImg.gameObject.SetActive(false);
        _waveKillCountText.gameObject.SetActive(false);

        GameObject spawnerParent = GameObject.FindGameObjectWithTag("Spawner");
        for(int i = 0; i < _enemySpawner.Length; i++)
        {
            _enemySpawner[i] = spawnerParent.transform.GetChild(i).GetComponent<EnemySpawner>();
        }

        _winRt.gameObject.SetActive(false);
        _finalScoreImg.gameObject.SetActive(false);
    }

    public void StartGame() //Will be made to be only be called after finished loading
    {
        _timeManager.BeginTimer();
        StartCoroutine(WaveAnimation());
    }
    void Update()//Everything will use this for optimization
    {
        _mapManager.GameUpdate();
        _timeManager.GameUpdate();
    }
#endregion General


#region HitLag
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
#endregion HitLag

#region Particle
    public void PlayDeathParticle(Vector2 position)
    {
        //Singleton.Instance.Audio.PlaySound(DeathSFX)
        _deathParticle.transform.position = position;
        // _deathParticle.Play();
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.position = position;
        _deathParticle.Emit(emitParams, 1);
    }
    public void PlayKillParticle(Vector2 position)
    {
        Singleton.Instance.Audio.PlaySound(KillSFX);
        _killParticle.transform.position = position;
        // _killParticle.Play();
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.position = position;
        _killParticle.Emit(emitParams, 1);
    }
    public void PlayHitParticle(Vector3 position)
    {
        Singleton.Instance.Audio.PlaySound(HitSFX);
        _hitEffect.transform.position = position;
        // _hitEffect.Play();
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.position = position;
        _hitEffect.Emit(emitParams, 1);
    }
#endregion Particle

#region Coin
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
            _coinAnchor.localScale = Vector3.Lerp(Vector3.one*1.7f, Vector3.one, Ease.OutQuart(t));
            yield return null;
        }
    }
#endregion Coin


    

#region GameStates

    IEnumerator WaveAnimation()
    {
        yield return new WaitForSecondsRealtime(3f);

        Singleton.Instance.Audio.PlaySound(NextWaveSFX);

        _waveCurrent += 1;
        _waveCurrentKillCount = 0;
        _waveImg.sprite = _waveSprite[_waveCurrent];
        _waveImg.gameObject.SetActive(true);
        _waveKillCountText.gameObject.SetActive(true);
        _waveKillCountText.text = "0/" + _waveMaxEnemies[_waveCurrent].ToString();  
        
        _waveRt.anchoredPosition = new Vector2(0, -263);
        StartCoroutine(RtScaleOutQuart(_waveRt, 10, 4, 0.3f));
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(RtScaleOutQuart(_waveRt, 4, 1, 0.3f));
        StartCoroutine(RtPositionOutQuart(_waveRt, new Vector2(0, -263), new Vector2(0, -30), 0.3f));

        yield return new WaitForSecondsRealtime(1);
        for(int i = 0; i < _enemySpawner.Length; i++)
        {
            _enemySpawner[i].Spawn(_enemyBanditSmallCorePrefab, _waveMaxEnemies[_waveCurrent]/_enemySpawner.Length);
        }
    }
    IEnumerator FinalWaveAnimation()
    {
        yield return new WaitForSecondsRealtime(3f);

        Singleton.Instance.Audio.PlaySound(NextWaveSFX);

        _waveCurrent += 1;
        _waveCurrentKillCount = 0;
        _waveImg.sprite = _waveSprite[_waveCurrent];
        _waveImg.gameObject.SetActive(true);
        _waveKillCountText.gameObject.SetActive(true);
        _waveKillCountText.text = "0/" + _waveMaxEnemies[_waveCurrent].ToString();  
        
        _waveRt.anchoredPosition = new Vector2(0, -263);
        StartCoroutine(RtScaleOutQuart(_waveRt, 10, 4, 0.3f));
        
        yield return new WaitForSecondsRealtime(1f);
        _waveImg.sprite = _waveFinalSprite;
        StartCoroutine(RtScaleOutQuart(_waveRt, 10, 4, 0.3f));
        yield return new WaitForSecondsRealtime(2f);

        StartCoroutine(RtScaleOutQuart(_waveRt, 4, 1, 0.3f));
        StartCoroutine(RtPositionOutQuart(_waveRt, new Vector2(0, -263), new Vector2(0, -30), 0.3f));

        yield return new WaitForSecondsRealtime(1);
        for(int i = 0; i < _enemySpawner.Length; i++)
        {
            _enemySpawner[i].Spawn(_enemyBanditSmallCorePrefab, _waveMaxEnemies[_waveCurrent]/_enemySpawner.Length);
        }
    }

    IEnumerator WinAnimation()
    {
        SaveData saveData = Singleton.Instance.Save.Data;

        yield return new WaitForSecondsRealtime(1f);

        Singleton.Instance.Audio.PlaySound(WinSFX);

        _winRt.gameObject.SetActive(true);
        StartCoroutine(RtPositionOutQuart(_winRt, Vector2.up*540, Vector2.zero, 0.5f));

        _timeManager.EndTimer();
        _finalCoinAmountTimeText.text = "×" + (saveData.Coin + _coinAmount).ToString();
        _finalTimeText.text = "Time: " + _timeManager.ElapsedTimeStr;

        float currentTime = _timeManager.ElapsedTime;
        float currentFastestTime = saveData.FastestSwordsmanTime;

        if(currentTime < currentFastestTime)
        {
            _finalFastestTimeText.text = "New Fastest Time: " + _timeManager.ElapsedTimeStr;
            saveData.FastestSwordsmanTime = currentTime;
            saveData.FastestSwordsmanTimeStr = _timeManager.ElapsedTimeStr;

        }
        else
        {
            _finalFastestTimeText.text = "Fastest Time: " + saveData.FastestSwordsmanTimeStr;
        }

        
        if(currentTime > 240f) //D
        {
            _finalScoreImg.sprite = _finalScoreSprite[3];
            saveData.RankSwordsman = 3;
        }
        else if(currentTime > 180f) //C
        {
            _finalScoreImg.sprite = _finalScoreSprite[2];
            saveData.RankSwordsman = 2;
        }
        else if(currentTime > 120f) //B
        {
            _finalScoreImg.sprite = _finalScoreSprite[1];
            saveData.RankSwordsman = 1;
        }
        else //A
        {
            _finalScoreImg.sprite = _finalScoreSprite[0];
            saveData.RankSwordsman = 0;
        }

        saveData.Coin += _coinAmount;
        Singleton.Instance.Save.SaveData();

        yield return new WaitForSecondsRealtime(1f);
        _finalScoreImg.gameObject.SetActive(true);
        StartCoroutine(RtScaleInQuad(_finalScoreImg.GetComponent<RectTransform>(), 10, 1, 0.3f));
        yield return new WaitForSecondsRealtime(0.3f);
        Singleton.Instance.Audio.PlaySound(Singleton.Instance.Game.RankASFX);

    }

    IEnumerator RtPositionOutQuart(RectTransform rt, Vector2 startPosition, Vector2 targetPosition, float duration)
    {
        float t = 0;
        while(t <= 1)
        {
            t += Time.unscaledDeltaTime/duration;
            rt.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, Ease.OutQuart(t));
            yield return null;
        }
        rt.anchoredPosition = targetPosition;
    }
    IEnumerator RtScaleOutQuart(RectTransform rt, float startScaleFloat, float targetScaleFloat, float duration)
    {
        Vector2 startScale = Vector2.one * startScaleFloat;
        Vector2 targetScale = Vector2.one * targetScaleFloat;
        float t = 0;
        while(t <= 1)
        {
            t += Time.unscaledDeltaTime/duration;
            rt.localScale = Vector2.LerpUnclamped(startScale, targetScale, Ease.OutQuart(t));
            yield return null;
        }
        rt.localScale = targetScale;
    }
    IEnumerator RtScaleInQuad(RectTransform rt, float startScaleFloat, float targetScaleFloat, float duration)
    {
        Vector2 startScale = Vector2.one * startScaleFloat;
        Vector2 targetScale = Vector2.one * targetScaleFloat;
        float t = 0;
        while(t <= 1)
        {
            t += Time.unscaledDeltaTime/duration;
            rt.localScale = Vector2.LerpUnclamped(startScale, targetScale, Ease.InQuad(t));
            yield return null;
        }
        rt.localScale = targetScale;
    }

    #endregion GameStates
    

#region LoadScene
    public void LoadHome() => StartCoroutine(LoadHomeAnimation());

    IEnumerator LoadHomeAnimation()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Singleton.Instance.Scene.LoadSceneWithTransition("MainMenu");
        Singleton.Instance.Transition
            .AddOutEnd(DisableGame);
    }
    void DisableGame()
    {
        Singleton.Instance.Game.gameObject.SetActive(false);
        Cursor.visible = true;
    }
#endregion LoadScene


#region Shop
    public void EnableBlacksmith()
    => StartCoroutine(ShopRtOutQuartKeyed(_blacksmithRt, _blacksmithRt.anchoredPosition, new Vector2(0, 0), 0.3f));
    public void DisableBlacksmith()
    => StartCoroutine(ShopRtInQuartKeyed(_blacksmithRt, _blacksmithRt.anchoredPosition, new Vector2(0, -250), 0.3f));



    ushort _keyRtPositionOutQuartKeyed;
    IEnumerator ShopRtOutQuartKeyed(RectTransform rt, Vector2 startPosition, Vector2 targetPosition, float duration)
    {
        rt.gameObject.SetActive(true);
        _keyRtPositionOutQuartKeyed++;
        ushort requirement = _keyRtPositionOutQuartKeyed;
        float t = 0;
        while(t <= 1 && requirement == _keyRtPositionOutQuartKeyed)
        {
            t += Time.unscaledDeltaTime/duration;
            rt.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, Ease.OutQuart(t));
            yield return null;
        }
        if(requirement == _keyRtPositionOutQuartKeyed)
        rt.anchoredPosition = targetPosition;
    }
    IEnumerator ShopRtInQuartKeyed(RectTransform rt, Vector2 startPosition, Vector2 targetPosition, float duration)
    {
        _keyRtPositionOutQuartKeyed++;
        ushort requirement = _keyRtPositionOutQuartKeyed;
        float t = 0;
        while(t <= 1 && requirement == _keyRtPositionOutQuartKeyed)
        {
            t += Time.unscaledDeltaTime/duration;
            rt.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, Ease.InQuart(t));
            yield return null;
        }
        if(requirement == _keyRtPositionOutQuartKeyed)
        {
            rt.anchoredPosition = targetPosition;
            rt.gameObject.SetActive(false);
        }
    }
#endregion Shop
}
