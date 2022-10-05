using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttackSlash1State : PlayerBaseState
{
    public PlayerAttackSlash1State(PlayerCore contextCore, PlayerStates playerStates) : base (contextCore, playerStates)
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

    bool _isDashBuffered = false;
    public override void StateEnter()
    {
        Core.Animator.Play("SwordSlash1");
        Core.Animator.PlaySwordSlash1Particle();
        Singleton.Instance.Audio.PlaySound(Core.Stats.SlashSFX);
        _isCancelable = false;
        _isBufferable = false;
        _isBuffered = false;
        _isGravitied = false;
        _isAnimationEnded = false;
        _isHitboxEnabled = true;
        _enemyHitList.Clear();
        _superState.CurrentCombo++;
        Core.Locomotion.Move();
    }

    public override void StateUpdate()
    {
        //Buffer attack
        if(_isBufferable && !_isCancelable && Core.Input.AttackInput.WasPressedThisFrame())
        {    
            _isBuffered = true;
            _isDashBuffered = false;//So that only one of these is buffered
        } 

        //Buffer dash
        if(_isBufferable && !_isCancelable && Core.Input.ActionInput.WasPressedThisFrame())
        {
            _isDashBuffered = true;
            _isBuffered = false;
        }
        
        //Execute buffered attack
        else if(_isBufferable && _isCancelable && _isBuffered)
            SwitchState(States.AttackSlash2());
        
        // //Execute buffered dash
        // else if(_isBufferable && _isCancelable && _isDashBuffered)
        //     SwitchSuperState(States.Dash());
        // Immediete dash
        if(Core.Input.ActionInput.WasPressedThisFrame())
        {
            SwitchSuperState(States.Dash());
        }
        if(Core.Input.Super1Input.WasPressedThisFrame())
            SwitchSuperState(States.Super1());

        //The normal one (execute attack without input buffering)
        else if(_isCancelable && Core.Input.AttackInput.WasPressedThisFrame() && !_isAnimationEnded)
            SwitchState(States.AttackSlash2());
    }

    public override void StateFixedUpdate()
    {
        Core.Animator.IsWeaponRotatable = false;

        // Stop player if enemy get hit with the following        
        // Core.Locomotion.Rb.velocity = Vector2.zero;
        // Or make the player velocity slower

        if(_isHitboxEnabled)
        {
            Collider2D[] hits = Core.Attack.EnableHitbox();
            if(hits == null)return;
            foreach(Collider2D hit in hits)
            {
                if(hit == null)return;
                Rigidbody2D hitRigidbody = hit.attachedRigidbody;
                
                if(hitRigidbody == null) return;
                if(_enemyHitList.Contains(hitRigidbody))continue;
                
                _enemyHitList.Add(hitRigidbody);
                
                EnemyCore enemyCore = hitRigidbody.GetComponent<EnemyCore>();
                Vector2 direction = (hitRigidbody.position - Core.Locomotion.Rb.position).normalized;
                enemyCore.Launch(direction, 2);
                enemyCore.Stats.Damage(Random.Range(20f, 35f), direction);
                Singleton.Instance.Game.WeakHitLag();

                Core.Locomotion.Rb.velocity = Vector2.zero;
                // enemyCore.FlipToLookAt(hit.point);

                // Singleton.Instance.game.WeakHitLag();
                // Core.Animator.PlayHitParticle(hit.ClosestPoint(Core.Locomotion.Rb.position));
                Singleton.Instance.Game.PlayHitParticle(hit.transform.position);
            }
        }
    }

    public override void StateExit()
    {
        Core.Animator.IsWeaponRotatable = true;
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
        SwitchSuperState(States.Ground());
    }
}
