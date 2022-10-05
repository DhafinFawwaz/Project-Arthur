using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{

    float _duration = 0.5f;
    float _delayAfterOut = 0.2f;
    float _delayBeforeIn = 0.2f;
    bool _isMusicFade = true;
    bool _isMusicFadeInstant = true;
    public TransitionAnimation Anim;
    void Start(){SetOutDefault();SetInDefault();}
#region Set
    public TransitionManager SetDelayAfterOut(float t)
    {
        _delayAfterOut = t;
        return this;
    }
    public TransitionManager SetDelayBeforeIn(float t)
    {
        _delayBeforeIn = t;
        return this;
    }
    public TransitionManager SetDuration(float t)
    {
        _duration = t;
        return this;
    }

    public TransitionManager SetMusicFade(bool b)
    {
        _isMusicFade = b;
        return this;
    }

    public TransitionManager SetOutStart(OutStartDelegate func)
    {
        OutStart = func;
        return this;
    }
    public TransitionManager SetOutAnimation(OutAnimationDelegate func)
    {
        OutAnimation = func;
        return this;
    }
    public TransitionManager SetOutEnd(OutEndDelegate func)
    {
        OutEnd = func;
        return this;
    }
    public TransitionManager AddOutStart(OutStartDelegate func)
    {
        OutStart += func;
        return this;
    }
    public TransitionManager AddOutAnimation(OutAnimationDelegate func)
    {
        OutAnimation += func;
        return this;
    }
    public TransitionManager AddOutEnd(OutEndDelegate func)
    {
        OutEnd += func;
        return this;
    }

    
    public TransitionManager SetInStart(InStartDelegate func)
    {
        InStart = func;
        return this;
    }
    public TransitionManager SetInAnimation(InAnimationDelegate func)
    {
        InAnimation = func;
        return this;
    }
    public TransitionManager SetInEnd(InEndDelegate func)
    {
        InEnd = func;
        return this;
    }
    public TransitionManager AddInStart(InStartDelegate func)
    {
        InStart += func;
        return this;
    }
    public TransitionManager AddInAnimation(InAnimationDelegate func)
    {
        InAnimation += func;
        return this;
    }
    public TransitionManager AddInEnd(InEndDelegate func)
    {
        InEnd += func;
        return this;
    }

#endregion Set


    public void SetOutDefault()
    {
        _duration = 0.5f;
        _delayAfterOut = 1f;
        _delayBeforeIn = 1f;
        _isMusicFade = true;
        _isMusicFadeInstant = true;
        OutStart     = Singleton.Instance.Transition.Anim.OutStart;
        OutAnimation = Singleton.Instance.Transition.Anim.OutAnimation;
        OutEnd       = Singleton.Instance.Transition.Anim.OutEnd;
    }
    public void SetInDefault()
    {
        _duration = 0.5f;
        _delayAfterOut = 1f;
        _delayBeforeIn = 1f;
        _isMusicFade = true;
        _isMusicFadeInstant = true;
        InStart     = Singleton.Instance.Transition.Anim.InStart;
        InAnimation = Singleton.Instance.Transition.Anim.InAnimation;
        InEnd       = Singleton.Instance.Transition.Anim.InEnd;
    }

    public delegate void OutStartDelegate();
    OutStartDelegate OutStart;
    public delegate void OutAnimationDelegate(float t);
    OutAnimationDelegate OutAnimation;
    public delegate void OutEndDelegate();
    OutEndDelegate OutEnd;

    public delegate void InStartDelegate();
    InStartDelegate InStart;
    public delegate void InAnimationDelegate(float t);
    InAnimationDelegate InAnimation;
    public delegate void InEndDelegate();
    InEndDelegate InEnd;

    

    public TransitionManager Out()
    {
        StartCoroutine(TransitionOut());
        return this;
    }

    IEnumerator TransitionOut()
    {
        float t = 0;
        OutStart();
        while(t <= 1)
        {
            OutAnimation(t);
            if(_isMusicFade)
                Singleton.Instance.Audio.SetMusicSourceVolume(1 - t);
            t += Time.unscaledDeltaTime/_duration;
            yield return null;
        }
        if(_isMusicFade)
            Singleton.Instance.Audio.SetMusicSourceVolume(0);
        yield return new WaitForSecondsRealtime(_delayAfterOut);
        OutEnd();
        SetOutDefault();
    }
    public void OutDefault()
    {
        SetOutDefault();
        StartCoroutine(TransitionOut());
    }



    public TransitionManager In()
    {
        StartCoroutine(TransitionIn());
        return this;
    }
    IEnumerator TransitionIn()
    {
        yield return new WaitForSecondsRealtime(_delayBeforeIn);
        float t = 0;
        if(_isMusicFadeInstant)
            Singleton.Instance.Audio.SetMusicSourceVolume(1);
        InStart();
        while(t <= 1)
        {
            InAnimation(t);
            
            t += Time.unscaledDeltaTime/_duration;
            yield return null;
        }
        InEnd();
        SetInDefault();
    }
    public void InDefault()
    {
        SetInDefault();
        StartCoroutine(TransitionIn());
    }



    
}
