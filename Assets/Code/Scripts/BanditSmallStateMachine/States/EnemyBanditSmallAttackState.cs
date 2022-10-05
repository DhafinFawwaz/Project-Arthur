using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBanditSmallAttackState : EnemyBanditSmallBaseState
{
    public EnemyBanditSmallAttackState(EnemyBanditSmallCore contextCore, EnemyBanditSmallStates EnemyBanditSmallStates) : base (contextCore, EnemyBanditSmallStates)
    {
        IsRootState = true;
    }

    int _currentCombo = 0;
    public int CurrentCombo {get{return _currentCombo;} set{_currentCombo = value;}}
    public override void StateEnter()
    {
        _currentCombo = 0;
        Core.Agent.velocity = Vector2.zero;
        Core.Animator.Play("BanditSlash1");
        Core.Animator.PlaySwordSlash1Particle();
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

    public override void StateOnAnimationEnd()
    {
        SwitchState(States.Ground());
    }

    public override void StateOnHurt()
    {
        SwitchState(States.Hurt());
    }
}
