using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] float _delay = 0;

    [Foldout("HUD", true)] 
    [Header("HUD")]
    [SerializeField] TextMeshProUGUI _coinAmountText;
    
    [Foldout("HomeMenu", true)] 
    [Header("HomeMenu")]
    [SerializeField] GameObject _homeMenu;
    [SerializeField] RectTransform _startRt;
    [SerializeField] RectTransform _upgradeRt;
    [SerializeField] RectTransform _settingsRt;
    [SerializeField] RectTransform _exitRt;
    
    [Foldout("StartMenu", true)] 
    [Header("StartMenu")]
    [SerializeField] GameObject _startMenu;
    [SerializeField] RectTransform _mageRt;
    [SerializeField] RectTransform _swordsmanRt;
    [SerializeField] RectTransform _archerRt;
    [SerializeField] RectTransform _backStartMenuRt;

    [Foldout("UpgradeMenu", true)] 
    [Header("UpgradeMenu")]
    [SerializeField] GameObject _upgradeMenu;
    [SerializeField] RectTransform _blacksmithRt;
    [SerializeField] RectTransform _healerRt;
    [SerializeField] RectTransform _bankerRt;
    [SerializeField] RectTransform _witchRt;
    [SerializeField] RectTransform _backUpgradeRt;

    [Foldout("SettingsMenu", true)] 
    [Header("SettingsMenu")]
    [SerializeField] GameObject _settingsMenu;
    [SerializeField] RectTransform _backSettingsRt;
    [SerializeField] GameObject _settings;

    SaveData _data;
    void Start()
    {
        // Singleton.Instance.Resolution.SetResolutionPercentage(1, 2);
        Singleton.Instance.Game.gameObject.SetActive(false);
        _data = Singleton.Instance.Save.LoadData();
        
        _coinAmountText.text = "× " + _data.Coin;
        
        Cursor.visible = true;
    }
    public void DisableAll()
    {
        _homeMenu.SetActive(false);
        _settings.SetActive(false);
    }
    public void StartToHome() => StartCoroutine(StartToHomeAnimation());
    public void UpgradeToHome() => StartCoroutine(UpgradeToHomeAnimation());
    public void SettingsToHome() => StartCoroutine(SettingsToHomeAnimation());
    public void HomeToStart() => StartCoroutine(HomeToStartAnimation());
    public void HomeToUpgrade() => StartCoroutine(HomeToUpgradeAnimation());
    public void HomeToSettings() => StartCoroutine(HomeToSettingsAnimation());
    public void HomeToExit() => Application.Quit();

    public void PickSwordsman()
    {
        Singleton.Instance.Scene.LoadSceneWithTransition("Level1Test");
        Singleton.Instance.Scene.AddOnLoadingEnd(EnableGame);
    }
    void EnableGame() => Singleton.Instance.Game.gameObject.SetActive(true);

    void ActivateHome()
    {
        DisableAll();
        _homeMenu.SetActive(true);

    }
    public void LoadSettings()
    {
        Singleton.Instance.Transition.Out()
            .AddOutEnd(Singleton.Instance.Transition.InDefault)
            .SetDelayAfterOut(_delay)
            .AddOutEnd(ActivateSettings);
        
    }
    void ActivateSettings()
    {
        DisableAll();
        _settings.SetActive(true);
    }

    public void LoadScene(string sceneName)
        => Singleton.Instance.Scene.LoadSceneWithTransition(sceneName);


#region Coroutines
    IEnumerator StartToHomeAnimation()
    {
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator UpgradeToHomeAnimation()
    {
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator SettingsToHomeAnimation()
    {
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator HomeToStartAnimation()
    {
        Singleton.Instance.Transition.BlockButton(true);
        StartCoroutine(EaseInRt(_exitRt, new Vector2(200, _exitRt.anchoredPosition.y), 0.5f));
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(EaseInRt(_settingsRt, new Vector2(200, _settingsRt.anchoredPosition.y), 0.5f));
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(EaseInRt(_upgradeRt, new Vector2(200, _upgradeRt.anchoredPosition.y), 0.5f));
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(EaseInRt(_startRt, new Vector2(200, _startRt.anchoredPosition.y), 0.5f));
        yield return new WaitForSeconds(0.15f);

        _startMenu.gameObject.SetActive(true);
        StartCoroutine(EaseOutRt(_swordsmanRt, new Vector2(_swordsmanRt.anchoredPosition.x, -50f), 0.5f));
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(EaseOutRt(_mageRt, new Vector2(_mageRt.anchoredPosition.x, -50f), 0.5f));
        StartCoroutine(EaseOutRt(_archerRt, new Vector2(_archerRt.anchoredPosition.x, -50f), 0.5f));
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(EaseOutRt(_backStartMenuRt, new Vector2(-100f, _backStartMenuRt.anchoredPosition.y), 0.5f));


        _homeMenu.gameObject.SetActive(false);
        Singleton.Instance.Transition.BlockButton(false);
    }
    IEnumerator HomeToUpgradeAnimation()
    {
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator HomeToSettingsAnimation()
    {
        yield return new WaitForSeconds(0.5f);
    }

#endregion Coroutines

#region Tween
    IEnumerator EaseOutRt(RectTransform Rt, Vector2 targetPosition, float duration)
    {
        Vector2 currentPosition = Rt.anchoredPosition;
        float t = 0;

        while (t <= 1)
        {
            t += Time.unscaledDeltaTime/duration;
            Rt.anchoredPosition = Vector2.Lerp(currentPosition, targetPosition, Ease.OutQuart(t));
            yield return null;
        }
        Rt.anchoredPosition = targetPosition;
    }
    IEnumerator EaseInRt(RectTransform Rt, Vector2 targetPosition, float duration)
    {
        Vector2 currentPosition = Rt.anchoredPosition;
        float t = 0;

        while (t <= 1)
        {
            t += Time.unscaledDeltaTime/duration;
            Rt.anchoredPosition = Vector2.Lerp(currentPosition, targetPosition, Ease.InQuart(t));
            yield return null;
        }
        Rt.anchoredPosition = targetPosition;
    }
#endregion Tween
    
    
}
