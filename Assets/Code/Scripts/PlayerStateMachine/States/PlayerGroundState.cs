using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerCore contextCore, PlayerStates playerStates) : base (contextCore, playerStates)
    {
        IsRootState = true;
    }
    public override void StateEnter()
    {
        if(!Core.Input.MoveInput.IsPressed())
            SetSubState(States.GroundIdle());
        else if(Core.Input.MoveInput.IsPressed())
            SetSubState(States.GroundMove());
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

    public override void StateOnHurt()
    {
        SwitchState(States.Hurt());
    }
}
