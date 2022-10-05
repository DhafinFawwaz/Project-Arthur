using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBanditSmallGroundState : EnemyBanditSmallBaseState
{
    public EnemyBanditSmallGroundState(EnemyBanditSmallCore contextCore, EnemyBanditSmallStates EnemyBanditSmallStates) : base (contextCore, EnemyBanditSmallStates)
    {
        IsRootState = true;
    }
    float _setTargetDelay = 0.2f;
    float _currentTimeToDelay = 0;
    public override void StateEnter()
    {
        _currentTimeToDelay = Random.Range(-0.2f, 0f); //So that each enemy will be random
        
    }
    public override void StateUpdate()
    {
        
    }

    public override void StateFixedUpdate()
    {
        _currentTimeToDelay += Time.fixedDeltaTime;
        Core.Animator.RotateWeapon(Core.PlayerTrans.position);
        if(_currentTimeToDelay >= _setTargetDelay)
        {
            _currentTimeToDelay = Random.Range(-0.2f, 0f);
            Core.Agent.SetDestination(Core.PlayerTrans.position);
            
            if((Core.PlayerTrans.position - Core.transform.position).magnitude <= 3)
                SwitchState(States.Attack());
        }
        // Core.Locomotion.Move(Core.Agent.desiredVelocity);

    }

    public override void StateExit()
    {
        
    }

    public override void StateOnHurt()
    {
        SwitchState(States.Hurt());
    }

}
