using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// [ExecuteInEditMode]
public class DebugInfo : MonoBehaviour
{
    [SerializeField] bool _showDebug = true;
    [SerializeField] Color _color = Color.red;
    [SerializeField] int _fontSize = 30;
    [SerializeField] int _offset = 20;
	[SerializeField] Rect _fpsRect = new Rect(20, 20, 400, 100);
 	GUIStyle _style;
	float _fps;

    PlayerControls _playerControls;
    InputAction _restartInput;
    InputAction _showDebugInput;
    InputAction _homeInput;

    [SerializeField] PlayerCore _core;
    void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
        _restartInput = _playerControls.FindAction("Restart", true);
        _showDebugInput = _playerControls.FindAction("ShowDebug", true);
        _homeInput = _playerControls.FindAction("Home", true);
    }
    // void OnEnable() => _playerControls.Enable();
	// void OnDisable() => _playerControls.Disable();
    void Start()
    {
        
        _style = new GUIStyle();
        _style.fontSize = _fontSize;
        _style.normal.textColor = _color;
        StartCoroutine(RecalculateFPS());
    }
    

	private IEnumerator RecalculateFPS()
	{
		while (true)
		{
            _style.fontSize = _fontSize;
			_fps = 1/Time.deltaTime;
			yield return new WaitForSeconds(1);
		}
	}

    void Update()
    {
        if(_showDebugInput.WasPressedThisFrame())_showDebug = !_showDebug;
        if(_restartInput.WasPressedThisFrame())
        {
            string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            Singleton.Instance.Scene.LoadSceneWithTransition(sceneName);
            Singleton.Instance.Transition.SetMusicFade(false);
            Singleton.Instance.Transition.AddOutEnd(DisableGame);
            Singleton.Instance.Transition.AddInStart(EnableGame);
        }
        if(_homeInput.WasPressedThisFrame())
        {
            Singleton.Instance.Scene.LoadSceneWithTransition("MainMenu");
            Singleton.Instance.Transition.SetMusicFade(true);
            Singleton.Instance.Transition.AddOutEnd(DisableGame);
        }
    }
    void DisableGame() => Singleton.Instance.Game.gameObject.SetActive(false);
    void EnableGame() => Singleton.Instance.Game.gameObject.SetActive(true);
    // void OnGUI()
    // {
    //     if(!_showDebug)return;
    //     _style.normal.textColor = _color;
    //     _style.alignment = TextAnchor.UpperRight;

    //     float multiplier = (float)Screen.height/1080;
    //     _style.fontSize = (int)(_fontSize * multiplier);

    //     _fpsRect.x = _offset + _fpsRect.width;
    //     _fpsRect.y = _offset;
    //     Rect labelRect = new Rect(Screen.width - _fpsRect.x * multiplier, 
    //                        _fpsRect.y * multiplier, 
    //                        _fpsRect.width * multiplier, 
    //                        _fpsRect.height * multiplier);

    //     string labelText = "<b>FPS: "+ string.Format("{0:0.0}\n"+
    //         "MaxRes: " +Display.main.systemWidth.ToString()+"x"+Display.main.systemHeight.ToString() + "\n" +
    //         "Res: "  +Screen.width.ToString()+"x"+Screen.height.ToString(),_fps) + "\n";

    //     if(_core != null)
    //     {
    //         if(_core.CurrentState != null)
    //         {
    //             labelText += _core.CurrentState.ToString() + "\n";
    //             if(_core.CurrentState.CurrentSubState != null)
    //                 labelText += _core.CurrentState.CurrentSubState.ToString() + "\n";
    //         }
    //     }
            
    //     labelText += "Press R key to restart\nPress F1 to toggle debug\nLeft Click to attack\n Right Click to dash\n spacebar to super ability 1";
    //     labelText += "</b>";

    //     GUI.Label(labelRect, labelText ,_style);
    // }
}
