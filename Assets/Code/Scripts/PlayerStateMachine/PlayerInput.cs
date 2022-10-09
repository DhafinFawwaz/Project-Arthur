using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputAction MouseInput{get{return _mouseInput;}}
    public InputAction MoveInput{get{return _moveInput;}}
    public InputAction AttackInput{get{return _attackInput;}}
    public InputAction ActionInput{get{return _actionInput;}}
    public InputAction Super1Input{get{return _super1Input;}}
    public InputAction BuyInput{get{return _buyInput;}}

    public Vector3 MousePosition{get{return _mousePosition;}}
    public Transform Cursor{get{return _cursorPoint;}}

    RectTransform _cursorPoint;
    RectTransform _rightCursor;
    RectTransform _upCursor;
    RectTransform _leftCursor;
    RectTransform _downCursor;

    PlayerControls _playerControls;
    InputAction _mouseInput;
    InputAction _moveInput;
    InputAction _attackInput;
    InputAction _actionInput;
    InputAction _super1Input;
    InputAction _buyInput;
    Vector3 _mousePosition;
    // [SerializeField] Transform _mousePositionDebug;
    void Awake()
    {
        _playerControls = new PlayerControls();
        _mouseInput = _playerControls.FindAction("MousePosition");
		_moveInput = _playerControls.FindAction("MoveInput");
		_attackInput = _playerControls.FindAction("AttackInput");
		_actionInput = _playerControls.FindAction("ActionInput");
		_super1Input = _playerControls.FindAction("Super1Input");
		_buyInput = _playerControls.FindAction("BuyInput");

    }
    void Start()
    {
        _cursorPoint = Singleton.Instance.Game.CursorPoint;
        _rightCursor = Singleton.Instance.Game.RightCursor;
        _upCursor = Singleton.Instance.Game.UpCursor;
        _leftCursor = Singleton.Instance.Game.LeftCursor;
        _downCursor = Singleton.Instance.Game.DownCursor;
    }
    void OnEnable() => _playerControls.Enable();
	void OnDisable() => _playerControls.Disable();

    void Update()
    {
        _cursorPoint.position = _mouseInput.ReadValue<Vector2>(); //Screen
        
        _mousePosition = Camera.main.ScreenToWorldPoint(_mouseInput.ReadValue<Vector2>()); //World
        _mousePosition.z = 0;
        // _mousePositionDebug.position = _mousePosition;


        if(_attackInput.WasPressedThisFrame())StartCoroutine(MouseClick());
    }

    ushort _key;
    
    IEnumerator MouseClick()
    {
        float start = 16f;
        float end = 24f;
        float t = 0;
        _key++;
        ushort requirement = _key;
        while(t <= 1 && _key == requirement)
        {
            t += Time.unscaledDeltaTime/0.2f;
            _leftCursor.anchoredPosition = Vector2.Lerp(new Vector2(-start, 0), new Vector2(-end, 0),   Ease.OutQuad(t));
            _rightCursor.anchoredPosition = Vector2.Lerp(new Vector2(start, 0), new Vector2(end, 0),    Ease.OutQuad(t));
            _upCursor.anchoredPosition = Vector2.Lerp(new Vector2(0, start), new Vector2(0, end),       Ease.OutQuad(t));
            _downCursor.anchoredPosition = Vector2.Lerp(new Vector2(0, -start), new Vector2(0, -end),   Ease.OutQuad(t));
            yield return null;
        }
        if(_key == requirement)
        {
            _leftCursor.anchoredPosition = new Vector2(-end, 0);
            _rightCursor.anchoredPosition = new Vector2(end, 0);
            _upCursor.anchoredPosition = new Vector2(0, end);
            _downCursor.anchoredPosition = new Vector2(0, -end);
        }
    }
}
