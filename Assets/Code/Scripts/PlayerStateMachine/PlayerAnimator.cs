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
        
        _weaponAnchor.right = _core.Input.MousePosition - _weaponAnchor.position;
        if(_isLookingRight == 180)
            _weaponAnchor.localEulerAngles = new Vector3(180, 180, -_weaponAnchor.localEulerAngles.z);
        //below doesn't work
        //_weaponAnchor.localEulerAngles = new Vector3(_isLookingRight, _isLookingRight, -_weaponAnchor.localEulerAngles.z);

        #region Flipping without statemachine
        //Don't ever read this mess anymore, ignore it forever
        if(_core.Input.Cursor.position.x > transform.position.x)
            _isLookingRight = 0;
        else 
            _isLookingRight = 180;

        if(_isChangingLookTo != _isLookingRight)
        {
            _isChangingLookTo = _isLookingRight;
            transform.localEulerAngles = new Vector3(0, _isChangingLookTo, 0);
        }
        #endregion Flipping without statemachine 
    }
    public void Play(string animationName) => _anim.Play(animationName, -1, 0);
    

    public void PlaySwordSlash1Particle() => _swordSlash1.Play();
    public void PlaySwordSlash2Particle() => _swordSlash2.Play();
    public void PlaySwordSlash3Particle() => _swordSlash3.Play();

    public void PlaySwordSuper1Particle() => _swordSuper1.Play();
    public void PlaySwordSuper2Particle() => _swordSuper2.Play();
    

}
