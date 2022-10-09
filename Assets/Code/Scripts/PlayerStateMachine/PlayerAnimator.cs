using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

#region Members
    PlayerCore _core;
    Animator _anim;
    int _isLookingRight = 0;//Flipping skin is better without statemachine. Don't overengineer eveything.
    int _isChangingLookTo;
    bool _isWeaponRotatable = true;

    [SerializeField] Transform _weaponAnchor;
    [SerializeField] Transform _bodyAnchor;
    [SerializeField] ParticleSystem _swordSlash1;
    [SerializeField] ParticleSystem _swordSlash2;
    [SerializeField] ParticleSystem _swordSlash3;
    [SerializeField] ParticleSystem _swordSuper1;
    [SerializeField] ParticleSystem _swordSuper2;
    
#endregion Members
    

#region Getter/Setter
    public bool IsWeaponRotatable{set{_isWeaponRotatable = value;}}
#endregion   

    void Start()
    {
        _core = transform.parent.GetComponent<PlayerCore>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        RotateWeapon();
    }

    void RotateWeapon()
    {
        if(!_isWeaponRotatable)return;
        RotateWeaponBasedOnMouse();
    }
    public void RotateWeaponBasedOnMouse()
    {
        // //Don't ever read this mess anymore, ignore it forever
        _weaponAnchor.right = _core.Input.MousePosition - _weaponAnchor.position;
        if(_core.Input.MousePosition.x < transform.position.x)
        {
            _weaponAnchor.localEulerAngles = new Vector3(180, 0, -_weaponAnchor.localEulerAngles.z);
            _bodyAnchor.localEulerAngles = new Vector3(0, 180, 0);
        }
        else _bodyAnchor.localEulerAngles = Vector3.zero;
    }


    public void Play(string animationName) => _anim.Play(animationName, -1, 0);
    

    public void PlaySwordSlash1Particle() => _swordSlash1.Play();
    public void PlaySwordSlash2Particle() => _swordSlash2.Play();
    public void PlaySwordSlash3Particle() => _swordSlash3.Play();

    public void PlaySwordSuper1Particle() => _swordSuper1.Play();
    public void PlaySwordSuper2Particle() => _swordSuper2.Play();
    

}
