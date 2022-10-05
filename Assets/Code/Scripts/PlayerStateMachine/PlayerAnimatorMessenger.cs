using UnityEngine;

public class PlayerAnimatorMessenger : MonoBehaviour
{
    PlayerCore _core;
    void Awake() => _core = transform.parent.GetComponent<PlayerCore>();
    void OnAnimatorMove() => _core.OnAnimatorMove();
    public void OnHitboxEnabled() => _core.OnHitboxEnabled();
    public void OnAnimationBufferable() => _core.OnAnimationBufferable();
    public void OnHitboxDisabled() => _core.OnHitboxDisabled();
    public void OnAnimationCancelable() => _core.OnAnimationCancelable();
    public void OnGravityEnabled() => _core.OnGravityEnabled();
    public void OnAnimationBeforeEnd() => _core.OnAnimationBeforeEnd();
    public void OnAnimationEnd() => _core.OnAnimationEnd();
}
