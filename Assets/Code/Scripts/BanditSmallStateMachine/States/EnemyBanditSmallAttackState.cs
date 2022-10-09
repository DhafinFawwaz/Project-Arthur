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

    bool _isHitboxEnabled = false;
    List<Rigidbody2D> _enemyHitList = new List<Rigidbody2D>();

    public override void StateEnter()
    {
        _currentCombo = 0;
        // Core.Agent.velocity = Vector2.zero;
        Core.Agent.acceleration = 30;
        Core.Animator.Play("BanditSlash1");
        Core.Animator.PlaySwordSlash1Particle();

        _isHitboxEnabled = false;
        _enemyHitList.Clear();
    }

    public override void StateUpdate()
    {
        
    }

    public override void StateFixedUpdate()
    {
        Core.Animator.IsWeaponRotatable = false;

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
                
                PlayerCore playerCore = hitRigidbody.GetComponent<PlayerCore>();
                Vector2 direction = (hitRigidbody.position - Core.Locomotion.Rb.position).normalized;
                playerCore.OnHurt();
                playerCore.Locomotion.Launch(direction, 2);
                playerCore.Stats.Damage(Random.Range(20f, 35f), direction);
                Singleton.Instance.Game.StrongHitLag();

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
        
    }

    public override void StateOnHitboxEnabled() => _isHitboxEnabled = true;
    public override void StateOnHitboxDisabled() => _isHitboxEnabled = false;
    public override void StateOnAnimationEnd()
    {
        SwitchState(States.Ground());
    }

    public override void StateOnHurt()
    {
        SwitchState(States.Hurt());
    }
}
