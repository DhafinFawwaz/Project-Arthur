using System.Collections;
using UnityEngine;

public class MusicLoader : MonoBehaviour
{
    [SerializeField] AudioClip _musicClip;
    void OnEnable()
    {
        if(Singleton.Instance == null)
        {
            StartCoroutine(DelayOnEnable());
            return;
        }

        if(_musicClip == Singleton.Instance.Audio.GetCurrentMusicClip())
        {}
        else if(_musicClip != null)
            Singleton.Instance.Audio.PlayMusic(_musicClip);
        else 
            Singleton.Instance.Audio.StopMusic();
    }

    IEnumerator DelayOnEnable()
    {
        yield return new WaitForEndOfFrame();
        if(_musicClip != null)
            Singleton.Instance.Audio.PlayMusic(_musicClip);
        else Singleton.Instance.Audio.StopMusic();
    }
}
