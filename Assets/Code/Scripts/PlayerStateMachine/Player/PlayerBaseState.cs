public abstract class PlayerBaseState
{
    bool _isRootState = false;
    PlayerCore _core;
    PlayerStates _states;
    PlayerBaseState _currentSubState;
    PlayerBaseState _currentSuperState;
    public PlayerBaseState CurrentSubState {get {return _currentSubState;}}
    public PlayerBaseState CurrentSuperState {get {return _currentSuperState;}}
    protected bool IsRootState{get{return _isRootState;} set{_isRootState = value;}}
    protected PlayerCore Core{get{return _core;}}
    protected PlayerStates States{get{return _states;}}
    public PlayerBaseState(PlayerCore contextCore, PlayerStates playerStates)
    {
        _core = contextCore;
        _states = playerStates;
    }

    public abstract void StateEnter();
    public abstract void StateUpdate();
    public abstract void StateFixedUpdate();
    public abstract void StateExit();
    public virtual void StateOnAnimatorMove(){}
    public virtual void StateOnHurt(){}
    public virtual void StateOnHitboxEnabled(){}
    public virtual void StateOnAnimationBufferable(){}
    public virtual void StateOnHitboxDisabled(){}
    public virtual void StateOnAnimationCancelable(){}
    public virtual void StateOnGravityEnabled(){}
    public virtual void StateOnAnimationBeforeEnd(){}
    public virtual void StateOnAnimationEnd(){}

    public void StateUpdates()
    {
        StateUpdate();
        if(_currentSubState != null)
        {
            _currentSubState.StateUpdates();
        }
    }
    public void StateFixedUpdates()
    {
        StateFixedUpdate();
        if(_currentSubState != null)
        {
            _currentSubState.StateFixedUpdates();
        }
    }
    
    public void StateOnAnimatorMoves()
    {
        StateOnAnimatorMove();
        if(_currentSubState != null)
        {
            _currentSubState.StateOnAnimatorMoves();
        }
    }
    public void StateOnHurts()
    {
        StateOnHurt();
        if(_currentSubState != null)
        {
            _currentSubState.StateOnHurts();
        }
    }
    public void StateOnHitboxEnableds()
    {
        StateOnHitboxEnabled();
        if(_currentSubState != null)
        {
            _currentSubState.StateOnHitboxEnableds();
        }
    }
    public void StateOnAnimationBufferables()
    {
        StateOnAnimationBufferable();
        if(_currentSubState != null)
        {
            _currentSubState.StateOnAnimationBufferables();
        }
    }
    public void StateOnHitboxDisableds()
    {
        StateOnHitboxDisabled();
        if(_currentSubState != null)
        {
            _currentSubState.StateOnHitboxDisableds();
        }
    }
    public void StateOnAnimationCancelables()
    {
        StateOnAnimationCancelable();
        if(_currentSubState != null)
        {
            _currentSubState.StateOnAnimationCancelables();
        }
    }
    public void StateOnGravityEnableds()
    {
        StateOnGravityEnabled();
        if(_currentSubState != null)
        {
            _currentSubState.StateOnGravityEnableds();
        }
    }
    public void StateOnAnimationBeforeEnds()
    {
        StateOnAnimationBeforeEnd();
        if(_currentSubState != null)
        {
            _currentSubState.StateOnAnimationBeforeEnds();
        }
    }
    public void StateOnAnimationEnds()
    {
        StateOnAnimationEnd();
        if(_currentSubState != null)
        {
            _currentSubState.StateOnAnimationEnds();
        }
    }
    
    
    
    protected void SwitchState(PlayerBaseState newState)
    {
        StateExit();
        newState.StateEnter();

        if(_isRootState)
        {
            _core.CurrentState = newState;
        }
        else if(_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }
    protected void SwitchSuperState(PlayerBaseState newState)
    {
        if(_isRootState)return;
        StateExit();
        newState.StateEnter();
        _core.CurrentState = newState;
    }
    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}