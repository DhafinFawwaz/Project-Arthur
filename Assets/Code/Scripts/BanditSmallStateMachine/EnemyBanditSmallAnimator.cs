using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBanditSmallAnimator : MonoBehaviour
{

#region Members
    EnemyBanditSmallCore _core;
    Animator _anim;
    int _isLookingRight = 0;//Flipping skin is better without statemachine. Don't overengineer eveything.
    int _isChangingLookTo;
    bool _isWeaponRotatable = true;

    [SerializeField] Transform _weaponAnchor;
    [SerializeField] ParticleSystem _swordSlash1;
#endregion Members
    

#region Getter/Setter
    public bool IsWeaponRotatable{set{_isWeaponRotatable = value;}}
#endregion   

    void Start()
    {
        _core = transform.parent.GetComponent<EnemyBanditSmallCore>();
        _anim = GetComponent<Animator>();
    }


    public void RotateWeapon(Vector2 _playerPosition)
    {
        if(_playerPosition.x > _core.transform.position.x)
        {
            _isLookingRight = 0;
            transform.localEulerAngles = new Vector3(0, _isLookingRight, 0);
        }
        else
        {
            _isLookingRight = 180;
            transform.localEulerAngles = new Vector3(0, _isLookingRight, 0);
        } 

        _weaponAnchor.right = _playerPosition - (Vector2)_weaponAnchor.position;
        if(_isLookingRight == 180)
            _weaponAnchor.localEulerAngles = new Vector3(180, 180, -_weaponAnchor.localEulerAngles.z);

        #region Flipping without statemachine
        //Don't ever read this mess anymore, ignore it forever
        if(_isChangingLookTo != _isLookingRight)
        {
            _isChangingLookTo = _isLookingRight;
            transform.localEulerAngles = new Vector3(0, _isChangingLookTo, 0);
        }
        #endregion Flipping without statemachine 
    }

    public void Play(string animationName) => _anim.Play(animationName, -1, 0);
    public void Play(int hash) => _anim.Play(hash, -1, 0);
    

    public void PlaySwordSlash1Particle() => _swordSlash1.Play();
    public int ToHash(string name) => Animator.StringToHash(name);

}
