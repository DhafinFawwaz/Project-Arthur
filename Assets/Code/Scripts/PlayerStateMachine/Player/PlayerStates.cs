using System.Collections.Generic;
public class PlayerStates
{
    enum State
    {
        ground,
        groundIdle,
        groundMove,

        dash,
        

        attack,
        attackSlash1,
        attackSlash2,
        attackSlash3,

        super1,

        hurt,

    }
    PlayerCore _core;
    Dictionary<State, PlayerBaseState> _states = new Dictionary<State, PlayerBaseState>();
    public PlayerStates(PlayerCore contextCore)
    {
        _core = contextCore;

        _states[State.ground] = new PlayerGroundState(_core, this);
        _states[State.groundIdle] = new PlayerGroundIdleState(_core, this);
        _states[State.groundMove] = new PlayerGroundMoveState(_core, this);

        _states[State.dash] = new PlayerDashState(_core, this);


        _states[State.attack] = new PlayerAttackState(_core, this);
        _states[State.attackSlash1] = new PlayerAttackSlash1State(_core, this);
        _states[State.attackSlash2] = new PlayerAttackSlash2State(_core, this);
        _states[State.attackSlash3] = new PlayerAttackSlash3State(_core, this);

        _states[State.super1] = new PlayerSuper1State(_core, this);
        
        _states[State.hurt] = new PlayerHurtState(_core, this);
    }

    public PlayerBaseState Ground() => _states[State.ground];    
    public PlayerBaseState GroundIdle() => _states[State.groundIdle];    
    public PlayerBaseState GroundMove() => _states[State.groundMove];

    public PlayerBaseState Dash() => _states[State.dash];

    public PlayerBaseState Attack() => _states[State.attack];
    public PlayerBaseState AttackSlash1() => _states[State.attackSlash1];
    public PlayerBaseState AttackSlash2() => _states[State.attackSlash2];
    public PlayerBaseState AttackSlash3() => _states[State.attackSlash3];

    public PlayerBaseState Super1() => _states[State.super1];
    
}
