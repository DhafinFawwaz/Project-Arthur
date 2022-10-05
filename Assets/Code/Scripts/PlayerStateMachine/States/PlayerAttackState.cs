using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerCore contextCore, PlayerStates playerStates) : base (contextCore, playerStates)
    {
        IsRootState = true;
    }

    int _currentCombo = 0;
    public int CurrentCombo {get{return _currentCombo;} set{_currentCombo = value;}}
    public override void StateEnter()
    {
        SetSubState(States.AttackSlash1());
        States.AttackSlash1().StateEnter();
        _currentCombo = 0;
    }

    public override void StateUpdate()
    {
        
    }

    public override void StateFixedUpdate()
    {
        
    }

    public override void StateExit()
    {
        
    }
}
