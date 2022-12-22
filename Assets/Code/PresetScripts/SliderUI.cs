using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [SerializeField] int _maxSliderValue = 7;
    [SerializeField] float _duration = 0.1f;
    [SerializeField] Slider _slider;
    [SerializeField] Transform _handleImg;
    [SerializeField] Transform _handlePos;
    [SerializeField] Image _fillImg;


    float _previousValue;
    void Start()
    {
        _previousValue = _slider.value;
        _slider.onValueChanged.AddListener(delegate {SetValue ();});
    }
    public void SetValueInstant(float newVal)
    {
        _slider.onValueChanged.RemoveAllListeners();
        _sliderValue = newVal;
        _fillImg.fillAmount = _sliderValue;
        _slider.value = _sliderValue;
        _handleImg.position = _handlePos.position;
        _slider.onValueChanged.AddListener(delegate {SetValue ();});
    }
    public void SetValue()
    {
        float newValue = Mathf.Round(_slider.value*_maxSliderValue)/_maxSliderValue;
        if(!(Mathf.Approximately(newValue, _previousValue)))
        {
            SliderValue = _slider.value;
            StartCoroutine(HandleAnimation(_handleImg, _handlePos.position, _fillImg, newValue));
        }
        _slider.value = Mathf.Round(_slider.value*_maxSliderValue)/_maxSliderValue;
        _previousValue = _slider.value;
    }

    ushort _key;
    IEnumerator HandleAnimation(Transform handle, Vector2 newPos, Image fillImg, float newFill)
    {
        float t = 0;
        Vector2 currentPos = handle.position;
        float currentFill = fillImg.fillAmount;
        _key++;
        ushort requirement = _key;
        while(t <= 1 && requirement == _key)
        {
            handle.position = Vector2.Lerp(currentPos, newPos, Ease.OutQuart(t));
            fillImg.fillAmount = Mathf.Lerp(currentFill, newFill, Ease.OutQuart(t));
            t += Time.unscaledDeltaTime/_duration;
            yield return null;
        }
        if(requirement == _key)
            handle.position = newPos;
    }


    public event OnVariableChangedDelegate OnVariableChanged;
    public delegate void OnVariableChangedDelegate(float newVal);

    float _sliderValue;
    [HideInInspector] public float SliderValue
    {
        get
        {
            return _sliderValue;
        }
        set
        {
            _sliderValue = value;
            if(OnVariableChanged != null)
                OnVariableChanged(_sliderValue);
        }
    }

}