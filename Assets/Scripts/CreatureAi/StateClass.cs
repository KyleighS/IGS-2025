using UnityEngine;

public abstract class StateClass
{
    public CreatureScript creatureScript;
    public StateClass(CreatureScript creatureScript)
    {
        this.creatureScript = creatureScript;
    }

    public abstract void OnEnterState();
    public abstract void OnExitState();
    public abstract void ChangeState(StateClass newState, ref StateClass currentState);
    public abstract void OnEveryFrame();
    public abstract void OnEveryPhysicsFrame();

}
