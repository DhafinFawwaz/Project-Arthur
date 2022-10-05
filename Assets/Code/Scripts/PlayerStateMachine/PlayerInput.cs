using System.Collections;
using UnityEngine;
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
    public Transform Cursor{get{return _cursor;}}

    [SerializeField] Transform _cursor;
    [SerializeField] Transform _leftCursor;
    [SerializeField] Transform _rightCursor;
    [SerializeField] Transform _upCursor;
    [SerializeField] Transform _downCursor;

    PlayerControls _playerControls;
    InputAction _mouseInput;
    InputAction _moveInput;
    InputAction _attackInput;
    InputAction _actionInput;
    InputAction _super1Input;
    InputAction _buyInput;
    Vector3 _mousePosition;
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
    void OnEnable() => _playerControls.Enable();
	void OnDisable() => _playerControls.Disable();

    void Update()
    {
        _mousePosition = _mouseInput.ReadValue<Vector2>();
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
        _mousePosition.z = 0;
        _cursor.position = _mousePosition;

        if(_attackInput.WasPressedThisFrame())StartCoroutine(MouseClick());
    }

    ushort _key;
    
    IEnumerator MouseClick()
    {
        float start = 0.5f;
        float end = 1f;
        float t = 0;
        _key++;
        ushort requirement = _key;
        while(t <= 1 && _key == requirement)
        {
            t += Time.unscaledDeltaTime/0.2f;
            _leftCursor.localPosition = Vector2.Lerp(new Vector2(-start, 0), new Vector2(-end, 0), Ease.OutQuad(t));
            _rightCursor.localPosition = Vector2.Lerp(new Vector2(start, 0), new Vector2(end, 0), Ease.OutQuad(t));
            _upCursor.localPosition = Vector2.Lerp(new Vector2(0, start), new Vector2(0, end), Ease.OutQuad(t));
            _downCursor.localPosition = Vector2.Lerp(new Vector2(0, -start), new Vector2(0, -end), Ease.OutQuad(t));
            yield return null;
        }
    }
}
