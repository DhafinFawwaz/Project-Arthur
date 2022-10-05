using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBanditSmallHurtState : EnemyBanditSmallBaseState
{
    public EnemyBanditSmallHurtState(EnemyBanditSmallCore contextCore, EnemyBanditSmallStates EnemyBanditSmallStates) : base (contextCore, EnemyBanditSmallStates)
    {
        IsRootState = true;
    }
    public override void StateEnter()
    {
        Core.Agent.enabled = false;
        Core.Animator.Play("BanditHurt");
    }
    public override void StateUpdate()
    {
        
    }

    public override void StateFixedUpdate()
    {
        //If there's a launcher (like springs in sonic) or getting hit so hard that gets you launched,
        //make a LaunchedState. Switching state to air is the last one to be done.
        //when the velocity goes down, LaunchedState will then switch into AirState 
        //This way Core.Input.ReleaseJump() won't be called except jumping from button.

    }

    public override void StateExit()
    {
        
    }

    public override void StateOnAnimationEnd()
    {
        Core.Agent.enabled = true;
        SwitchState(States.Ground());
    }
}
