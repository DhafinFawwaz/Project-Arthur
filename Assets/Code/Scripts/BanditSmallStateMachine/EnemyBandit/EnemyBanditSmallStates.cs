using System.Collections.Generic;
public class EnemyBanditSmallStates
{
    enum State
    {
        ground,

        attack,

        hurt,

    }
    EnemyBanditSmallCore _core;
    Dictionary<State, EnemyBanditSmallBaseState> _states = new Dictionary<State, EnemyBanditSmallBaseState>();
    public EnemyBanditSmallStates(EnemyBanditSmallCore contextCore)
    {
        _core = contextCore;

        _states[State.ground] = new EnemyBanditSmallGroundState(_core, this);

        _states[State.attack] = new EnemyBanditSmallAttackState(_core, this);
        
        _states[State.hurt] = new EnemyBanditSmallHurtState(_core, this);
    }

    public EnemyBanditSmallBaseState Ground() => _states[State.ground];    

    public EnemyBanditSmallBaseState Attack() => _states[State.attack];

    public EnemyBanditSmallBaseState Hurt() => _states[State.hurt];
    
}
