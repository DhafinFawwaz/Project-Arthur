using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCore : MonoBehaviour
{
#region Members
    PlayerInput _input;
    PlayerLocomotion _locomotion;
    PlayerAnimator _animator;
    PlayerAttack _attack;
    PlayerStats _stats;

    #region StateMachine
    PlayerBaseState _currentState;
    PlayerStates _states;
    #endregion
#endregion Members


#region Getter/Setter
    public PlayerInput Input {get{return _input;}}
    public PlayerLocomotion Locomotion {get{return _locomotion;}}
    public PlayerAnimator Animator {get{return _animator;}}
    public PlayerAttack Attack {get{return _attack;}}
    public PlayerStats Stats {get{return _stats;}}


    #region StateMachine
    public PlayerBaseState CurrentState {get{return _currentState;} set{_currentState = value;}}
    public PlayerStates States {get{return _states;} set{_states = value;}}
	#endregion StateMachine
#endregion Getter/Setter




    void Start()
    {
        _animator = transform.GetChild(0).GetComponent<PlayerAnimator>();
        _input = GetComponent<PlayerInput>();
        _locomotion = GetComponent<PlayerLocomotion>();
        _stats = GetComponent<PlayerStats>();
        _attack = transform.GetChild(0).GetComponent<PlayerAttack>();

        _states = new PlayerStates(this);
        _currentState = _states.Ground();//Have to be after _playerControls.FindAction
        _currentState.StateEnter();
    }

    void Update()
    {
        _currentState.StateUpdates();      
    }
	void FixedUpdate()
	{
        _currentState.StateFixedUpdates();
	}

    public void OnAnimatorMove() => _currentState.StateOnAnimatorMoves();
    public void OnHitboxEnabled() => _currentState.StateOnHitboxEnableds();
    public void OnAnimationBufferable() => _currentState.StateOnAnimationBufferables();
    public void OnHitboxDisabled() => _currentState.StateOnHitboxDisableds();
    public void OnAnimationCancelable() => _currentState.StateOnAnimationCancelables();
    public void OnGravityEnabled() => _currentState.StateOnGravityEnableds();
    public void OnAnimationBeforeEnd() => _currentState.StateOnAnimationBeforeEnds();
    //Had to be done. I've tried many things, and it seems that this is the most proper way to do it.
    //StateOnAttackBeforeEnd() is to prevent bug when SwitchState and pressing the attack button at the same time.
    //Have to do it like this since there's is no other cleaner way to get an event when animation ends.
    //Animator OnStateExit() doesn't work since it will be called when exiting any animation with on any condition
    //Creating a specific boolean in each attack state won't work either
    public void OnAnimationEnd() => _currentState.StateOnAnimationEnds();
    
    public virtual void OnHurt() => _currentState.StateOnHurts();
}
