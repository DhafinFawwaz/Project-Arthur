using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

#if UNITY_EDITOR 
using UnityEditor.Events;
[ExecuteInEditMode]
#endif
public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] float _duration = 0.1f;
    [SerializeField] float _enterScale = 1.2f;
    [SerializeField] float _exitScale = 1f;
    [SerializeField] float _downScale  = 1.3f;
    [SerializeField] float upScale  = 1f;
    [SerializeField] Transform _imageToResize;


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
        if(_imageToResize == null)_imageToResize = transform;
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

    public void EnterAnimation(){StartCoroutine(TweenScale(_imageToResize, _enterScale));}
    public void ExitAnimation(){StartCoroutine(TweenScale(_imageToResize, _exitScale));}
    public void DownAnimation(){StartCoroutine(TweenScale(_imageToResize, _downScale));}
    public void UpAnimation(){StartCoroutine(TweenScale(_imageToResize, upScale));}

    ushort _key;
    //Value will keep changing so that everytime a new TweenScale() coroutine is called,
    //the previous coroutine will be stopped and the new Scaling animation will be executed
    //without interuption.
    
    IEnumerator TweenScale(Transform trans, float endScale)
    {
        _key++;
        ushort requirement = _key;
        float startScale = trans.localScale.x;
        float t = 0;
        while (t <= 1 && requirement == _key)
        {
            trans.localScale = Vector3.one * Mathf.LerpUnclamped(startScale, endScale, Ease.OutPowBack(t, 4));
            t += Time.unscaledDeltaTime / _duration;
            yield return null;
        }
        if(requirement == _key)trans.localScale = Vector3.one * endScale;//if the key didn't change then get into endScale
    }

}
