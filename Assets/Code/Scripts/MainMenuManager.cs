using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    //Freely edit this whole thing
    [SerializeField] GameObject _home;
    [SerializeField] GameObject _settings;
    [SerializeField] float _delay = 0;
    public void DisableAll()
    {
        _home.SetActive(false);
        _settings.SetActive(false);
    }
    public void LoadHome()
    {
        Singleton.Instance.Transition.Out()
            .AddOutEnd(Singleton.Instance.Transition.InDefault)
            .SetDelayAfterOut(_delay)
            .AddOutEnd(ActivateHome);
    }
    void ActivateHome()
    {
        DisableAll();
        _home.SetActive(true);
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
}
