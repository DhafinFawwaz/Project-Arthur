using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBanditSmallLocomotion : EnemyLocomotion
{
#region Members

    [SerializeField] float _moveSpeed;
    [SerializeField] float _velPower;
    [SerializeField] float _acceleration;
    [SerializeField] float _deceleration;

    Vector2 _moveInputDirection;
#endregion Members




    void Awake()
    {
        Core = GetComponent<EnemyBanditSmallCore>();
        Rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
	{
		// _moveInputDirection = _core.Input.MoveInput.ReadValue<Vector2>();
		// _moveInputDirection = PlayerPosition, use AI;
		
		float targetSpeed = _moveInputDirection.magnitude * _moveSpeed;
		Vector2 speedDif = _moveInputDirection * _moveSpeed - Rb.velocity;

		float accelerationRate;
        accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _deceleration;

		Rb.AddForce(accelerationRate * speedDif);
	}
    public void Move(Vector2 velocity) => Rb.velocity = velocity;




    
}
