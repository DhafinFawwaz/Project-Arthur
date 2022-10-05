using UnityEngine;
using UnityEngine.AI;
public class EnemyBanditSmallCore : EnemyCore
{
#region Members
    EnemyBanditSmallLocomotion _locomotion;
    EnemyBanditSmallAnimator _animator;

    #region StateMachine
    EnemyBanditSmallBaseState _currentState;
    EnemyBanditSmallStates _states;
    #endregion
#endregion Members


#region Getter/Setter
    public EnemyBanditSmallLocomotion Locomotion {get{return _locomotion;}}
    public EnemyBanditSmallAnimator Animator {get{return _animator;}}


    #region StateMachine
    public EnemyBanditSmallBaseState CurrentState {get{return _currentState;} set{_currentState = value;}}
    public EnemyBanditSmallStates States {get{return _states;} set{_states = value;}}
	#endregion StateMachine
#endregion Getter/Setter



#region AnimationHash
    int BanditHurtHash;
#endregion

    void Start()
    {
        _animator = transform.GetChild(0).GetComponent<EnemyBanditSmallAnimator>();
        _locomotion = GetComponent<EnemyBanditSmallLocomotion>();
        Rb = GetComponent<Rigidbody2D>();
        Stats = GetComponent<EnemyStats>();
        
        _states = new EnemyBanditSmallStates(this);
        _currentState = _states.Ground();//Have to be after _EnemyBanditSmallControls.FindAction
        _currentState.StateEnter();

        PlayerTrans = Singleton.Instance.Game.Core.transform;
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        BanditHurtHash = Animator.ToHash("BanditHurt");
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
    
    public override void OnHurt() => _currentState.StateOnHurts();
    
}
