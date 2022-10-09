using System.Collections;
using UnityEngine;

public class Boot : MonoBehaviour
{
    void Start() => StartCoroutine(BootDelay());
    IEnumerator BootDelay()
    {
        yield return new WaitForSecondsRealtime(1f);

        Singleton.Instance.Resolution.SetResolutionPercentage(1, 2);
        Singleton.Instance.Scene.LoadSceneWithTransition("MainMenu");
    }

        
    void UnblockButton() => Singleton.Instance.Transition.BlockButton(false);
}
