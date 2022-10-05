using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttackSlash3State : PlayerBaseState
{
    public PlayerAttackSlash3State(PlayerCore contextCore, PlayerStates playerStates) : base (contextCore, playerStates)
    {
        IsRootState = false;
        _superState = ((PlayerAttackState)(States.Attack()));
    }
    PlayerAttackState _superState;
    bool _isCancelable = false;
    bool _isBufferable = false;
    bool _isBuffered = false;
    bool _isGravitied = false;
    bool _isAnimationEnded = false;
    bool _isHitboxEnabled = false;
    List<Rigidbody2D> _enemyHitList = new List<Rigidbody2D>();
    public override void StateEnter()
    {
        Core.Animator.Play("SwordSlash1");
        Core.Locomotion.Rb.velocity = new Vector2(0, Core.Locomotion.Rb.velocity.y);
        _isCancelable = false;
        _isBufferable = false;
        _isBuffered = false;
        _isGravitied = false;
        _isAnimationEnded = false;
        _isHitboxEnabled = true;
        _enemyHitList.Clear();
        _superState.CurrentCombo++;
    }

    public override void StateUpdate()
    {
        if(_isBufferable && !_isCancelable && Core.Input.AttackInput.WasPressedThisFrame())
            _isBuffered = true;
        
        else if(_isBufferable && _isCancelable && _isBuffered)
            SwitchState(States.AttackSlash2());
        
        else if(_isCancelable && Core.Input.AttackInput.WasPressedThisFrame() && !_isAnimationEnded)
            SwitchState(States.AttackSlash2());
    }

    public override void StateFixedUpdate()
    {
        
        // if(_isHitboxEnabled)
        // {
        //     RaycastHit2D[] hits = Core.Attack.EnableHitbox();
        //     if(hits == null)return;
        //     foreach(RaycastHit2D hit in hits)
        //     {
        //         Rigidbody2D hitRigidbody = hit.rigidbody;
                
        //         if(hitRigidbody == null) return;
        //         if(_enemyHitList.Contains(hitRigidbody))continue;
                
        //         _enemyHitList.Add(hitRigidbody);
                
        //         EnemyCore enemyCore = hitRigidbody.GetComponent<EnemyCore>();
        //         enemyCore.OnHurt(10,
        //             enemyCore.Animator.EnemySmallGuyHurtForwardWeak//Make the global version later
        //         );
        //         enemyCore.FlipToLookAt(hit.point);

        //         Singleton.Instance.game.WeakHitLag();
        //         Core.PlayHitEffect(hit.point);
        //     }
        // }
    }

    public override void StateExit()
    {
        
    }

    // public override void StateOnAnimatorMove()
    // {
    //     Vector2 targetPosition = (Vector2)Core.Animator.deltaPosition;
    //     Core.transform.Translate(targetPosition);
    // }
    public override void StateOnAnimationBufferable() => _isBufferable = true;
    public override void StateOnAnimationCancelable() => _isCancelable = true;
    public override void StateOnGravityEnabled() => _isGravitied = true;
    public override void StateOnAnimationBeforeEnd() => _isAnimationEnded = true;
    public override void StateOnHitboxEnabled() => _isHitboxEnabled = true;
    public override void StateOnHitboxDisabled() => _isHitboxEnabled = false;
    public override void StateOnAnimationEnd()
    {
        
    }
}
