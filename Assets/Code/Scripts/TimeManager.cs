using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;
    TimeSpan _timePlaying;

    bool _timerGoing;
    string _elapsedTimeStr;
    
    public float ElapsedTime{get{return _elapsedTime;}}
    public string ElapsedTimeStr{get{return _elapsedTimeStr;}}
    [SerializeField] float _elapsedTime;
    

    public void OnSceneLoaded()
    {  
        _timerText.text = "00:00.00";
        _timerGoing = false;
    }
    public void GameUpdate()
    {
        if(!_timerGoing)return;
        _elapsedTime += Time.unscaledDeltaTime;
        _timePlaying = TimeSpan.FromSeconds(_elapsedTime);
        _elapsedTimeStr = _timePlaying.ToString("mm':'ss'.'ff");
        _timerText.text = _elapsedTimeStr;
        if(_elapsedTime > 3600)_timerGoing = false;
    }

    
    public void BeginTimer()
    {
        _timerGoing = true;
        _elapsedTime = 0;
    }
    public void EndTimer()
    {
        _timerGoing = false;
    }
    

    ushort _key;
    IEnumerator ScaleRt(RectTransform rt, Vector3 targetScale, float tweenTime)
    {
        rt.gameObject.SetActive(true);
        Vector3 currentScale = rt.localScale;
        float t = 0;
        _key++;
        ushort requirement = _key;
        while (t <= 1 && _key == requirement)
        {
            t += Time.unscaledDeltaTime / tweenTime;
            rt.localScale = Vector3.Lerp(currentScale, targetScale, Ease.OutQuart(t));
            yield return null;
        }
        if(_key == requirement)rt.localScale = targetScale;
    }

}
