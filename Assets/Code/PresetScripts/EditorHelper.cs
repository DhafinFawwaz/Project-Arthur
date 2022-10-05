using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorHelper : MonoBehaviour
{



    public void TransitionOut()
    {
        Debug.Log("Transition Out");
        Singleton.Instance.Transition.Out()
            // .SetOnAnimation(Singleton.Instance.transition.anim.OutAnimation)
        ;
    }
    
    public void TransitionIn()
    {
        Debug.Log("Transition In");
        Singleton.Instance.Transition.In()
        ;
    }

    [SerializeField] GameObject _mainMenuCanvas;
    [SerializeField] GameObject _settingsCanvas;
    void OnEnableMainMenu()
    {
        DisableAllCanvas();
        _mainMenuCanvas.SetActive(true);
    }

    void OnEnableSettings()
    {
        DisableAllCanvas();
        _settingsCanvas.SetActive(true);
    }
    void DisableAllCanvas()
    {
        _mainMenuCanvas.SetActive(false);
        _settingsCanvas.SetActive(false);
    }

    public void LoadSettings()
    {
        Debug.Log("Loading Settings");
        Singleton.Instance.Transition.Out()
            .AddOutEnd(Singleton.Instance.Transition.InDefault)
            .AddOutEnd(OnEnableSettings)
        ;
    }
    public void LoadMainMenu()
    {
        Debug.Log("Loading MainMenu");
        Singleton.Instance.Transition.Out()
            .AddOutEnd(Singleton.Instance.Transition.InDefault)
            .AddOutEnd(OnEnableMainMenu)
        ;
    }
    
}
