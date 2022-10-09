using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerDashState : PlayerBaseState
{
    public PlayerDashState(PlayerCore contextCore, PlayerStates playerStates) : base (contextCore, playerStates)
    {
        IsRootState = true;
    }
    float _dashDuration = 0.2f;
    float _dashLength = 5f;
    float _t = 0;
    float _tcut = 0.8f;//Stop in the middle of easing
    Vector3 _startPosition;
    Vector3 _targetPosition;

    public override void StateEnter()
    {
        _t = 0;
        _startPosition = Core.Locomotion.Rb.position;
        Vector3 direction = (Core.Input.MousePosition - _startPosition).normalized;
        _targetPosition = _startPosition + direction * _dashLength;
    }
    public override void StateUpdate()
    {
        
    }

    public override void StateFixedUpdate()
    {
        if(_t <= _tcut)
        {
            _t += Time.fixedDeltaTime/_dashDuration;
            Core.Locomotion.Rb.MovePosition(Vector3.Lerp(_startPosition, _targetPosition, Ease.OutQuart(_t)));
        }
        else SwitchState(States.Ground());

    }

    public override void StateExit()
    {
        
    }

}
