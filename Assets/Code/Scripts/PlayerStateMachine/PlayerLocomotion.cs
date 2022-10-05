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
}
