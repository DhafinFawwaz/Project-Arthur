using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerGroundMoveState : PlayerBaseState
{
    public PlayerGroundMoveState(PlayerCore contextCore, PlayerStates playerStates) : base (contextCore, playerStates)
    {
        IsRootState = false;
    }

    public override void StateEnter()
    {

    }
    
    public override void StateUpdate()
    {
        if(Core.Input.MoveInput.WasReleasedThisFrame())
            SwitchState(States.GroundIdle());
        else if(Core.Input.AttackInput.WasPressedThisFrame())
            SwitchSuperState(States.Attack());
        else if(Core.Input.ActionInput.WasPressedThisFrame())
            SwitchSuperState(States.Dash());
        else if(Core.Input.Super1Input.WasPressedThisFrame())
            SwitchSuperState(States.Super1());
        

    }
    public override void StateFixedUpdate()
    {
        Core.Locomotion.Move();
    }

    public override void StateExit()
    {
        
    }
}
