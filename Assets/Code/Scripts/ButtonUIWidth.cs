using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

#if UNITY_EDITOR 
using UnityEditor.Events;
[ExecuteInEditMode]
#endif
public class ButtonUIWidth : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] float _duration = 0.1f;
    [SerializeField] float _enterWidth = 200f;
    [SerializeField] float _exitWidth = 160f;
    [SerializeField] float _downWidth = 200f;
    [SerializeField] float _upWidth = 160f;
    [SerializeField] Color _downColor = new Color(74f/255, 95f/255, 97f/255, 1f);
    [SerializeField] Color _upColor = new Color(174f/255f, 230f/255f, 234f/255f, 1f);
    [SerializeField] RectTransform _imageToResize;
    [SerializeField] Image _img;
    [SerializeField] Image _whiteOutImg;
    bool _isWhiteOut = false;


    [SerializeField] UnityEvent _clickEvent;
    [SerializeField] UnityEvent _pointerEnterEvent;
    [SerializeField] UnityEvent _pointerExitEvent;
    [SerializeField] UnityEvent _pointerDownEvent;
    [SerializeField] UnityEvent _pointerUpEvent;

    enum CursorState
    {
        inside, outside
    }
    CursorState _currentCursorState;

#if UNITY_EDITOR 
    void Awake()
    {
        //So that it only creates the listener once, when the component is dragged on, not when the scene is loaded.
        if(_pointerEnterEvent == null)
        {
            _pointerEnterEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(_pointerEnterEvent, EnterAnimation);
        }
        if(_pointerExitEvent == null)
        {
            _pointerExitEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(_pointerExitEvent, ExitAnimation);
        }
        if(_pointerDownEvent == null)
        {
            _pointerDownEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(_pointerDownEvent, DownAnimation);
        }
        if(_pointerUpEvent == null)
        {
            _pointerUpEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(_pointerUpEvent, UpAnimation);
        }
        if(_imageToResize == null)
        {
            _imageToResize = GetComponent<RectTransform>();
            _img = GetComponent<Image>();    
        }
    }
#endif


    public void PlaySound(AudioClip audioClip)
    {
        Singleton.Instance.Audio.PlaySound(audioClip);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _pointerEnterEvent.Invoke();
        _currentCursorState = CursorState.inside;
        // StartCoroutine(Enter());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _pointerExitEvent.Invoke();
        _currentCursorState = CursorState.outside;
        // StartCoroutine(Exit());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDownEvent.Invoke();
        // StartCoroutine(Down());
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(_currentCursorState == CursorState.inside)_pointerEnterEvent.Invoke();
        // StartCoroutine(Up());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _clickEvent.Invoke();
    }

    public void EnterAnimation(){if(!_isWhiteOut)StartCoroutine(TweenWidth(_imageToResize, new Vector2(_enterWidth, _imageToResize.sizeDelta.y)));}
    public void ExitAnimation() {if(!_isWhiteOut)StartCoroutine(TweenWidth(_imageToResize, new Vector2(_exitWidth, _imageToResize.sizeDelta.y)));}
    public void DownAnimation() {if(!_isWhiteOut)StartCoroutine(TweenWidth(_imageToResize, new Vector2(_downWidth, _imageToResize.sizeDelta.y)));}
    public void UpAnimation()   {if(!_isWhiteOut)StartCoroutine(TweenWidth(_imageToResize, new Vector2(_upWidth, _imageToResize.sizeDelta.y)));}

    ushort _key;
    //Value will keep changing so that everytime a new TweenScale() coroutine is called,
    //the previous coroutine will be stopped and the new Scaling animation will be executed
    //without interuption.
    
    IEnumerator TweenWidth(RectTransform rt, Vector3 endSizeDelta)
    {
        _key++;
        ushort requirement = _key;
        Vector3 startSizeDelta = rt.sizeDelta;
        float t = 0;
        while (t <= 1 && requirement == _key)
        {
            t += Time.unscaledDeltaTime / _duration;
            rt.sizeDelta = Vector3.Lerp(startSizeDelta, endSizeDelta, Ease.OutPowBack(t, 4));
            yield return null;
        }
        if(requirement == _key)rt.sizeDelta = endSizeDelta;//if the key didn't change then get into endSizeDelta
    }
    public void DownColor() => _img.color = _downColor;
    public void UpColor() => _img.color = _upColor;


    public void WhiteOut() => StartCoroutine(WhiteOutAnimation(_img));
    ushort _whiteOutKey;
    IEnumerator WhiteOutAnimation(Image img)
    {
        _whiteOutKey++;
        Color startColor = Color.white;
        Color endColor = Color.white;
        endColor.a = 0;

        _whiteOutImg.gameObject.SetActive(true);
        _whiteOutImg.GetComponent<RectTransform>().sizeDelta = _imageToResize.sizeDelta;

        ushort requirement = _whiteOutKey;
        float t = 0;
        _isWhiteOut = true;
        while (t <= 1 && requirement == _whiteOutKey)
        {
            t += Time.unscaledDeltaTime / 0.5f;
            _whiteOutImg.color = Color.Lerp(startColor, endColor, Ease.OutQuad(t));
            yield return null;
        }
        if(requirement == _key)_whiteOutImg.color = endColor;//if the key didn't change then get into endSizeDelta

        _isWhiteOut = false;
        _whiteOutImg.gameObject.SetActive(false);
        if(_currentCursorState == CursorState.outside)_pointerUpEvent.Invoke();
    }

}
