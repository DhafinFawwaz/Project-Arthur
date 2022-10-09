using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotion : MonoBehaviour
{
#region Members

    PlayerCore _core;
    [SerializeField] PlayerControls _playerControls;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _velPower;
    [SerializeField] float _acceleration;
    [SerializeField] float _deceleration;
    Vector2 _moveInputDirection;
#endregion Members

#region Getter/Setter
    public Rigidbody2D Rb{get{return _rb;}}
#endregion Getter/Setter



    void Awake()
    {
        _core = GetComponent<PlayerCore>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
	{
		_moveInputDirection = _core.Input.MoveInput.ReadValue<Vector2>();
		
		float targetSpeed = _moveInputDirection.magnitude * _moveSpeed;
		Vector2 speedDif = _moveInputDirection * _moveSpeed - _rb.velocity;

		float accelerationRate;
        accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _deceleration;

		_rb.AddForce(accelerationRate * speedDif);
	}


    float _launchedDuration = 0.2f;

    public void Launch(Vector2 launchDirection, float length)
    => StartCoroutine(Launched(launchDirection, length));
    ushort key;
    IEnumerator Launched(Vector2 launchDirection, float length)
    {
        // _core.OnHurt();
        _rb.velocity = Vector2.zero;
        Vector3 startPosition = _rb.position;
        Vector3 targetPosition = _rb.position + launchDirection * length;
        float t = 0;
        key++;
        ushort requirement = key;
        while(t <= 1 && requirement == key)
        {
            t += Time.deltaTime/_launchedDuration;
            _rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, Ease.OutQuart(t)));
            yield return null;
        }
        if(requirement == key)
        {
            _rb.MovePosition(targetPosition);
        }
    }
}
