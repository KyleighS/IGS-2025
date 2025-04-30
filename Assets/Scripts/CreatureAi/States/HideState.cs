using UnityEngine;

public class HideState : StateClass
{
    public Vector3 targetPos;
    public float destinationTolerance = 1f;

    public HideState(CreatureScript creatureScript) : base(creatureScript) { }
    public override void ChangeState(StateClass newState, ref StateClass currentState)
    {
        //The current state executes its exit function
        currentState.OnExitState();
        //The current state is changed
        currentState = newState;
        //The new current state executes its enter function
        currentState.OnEnterState();
    }

    public override void OnEnterState()
    {
        Debug.Log("Creature has been seen");
        targetPos = creatureScript.FindHidingPoint();
        creatureScript.navMeshAgent.SetDestination(targetPos);
        creatureScript.navMeshAgent.speed = 5;
        creatureScript.animator.SetBool("IsRunning", true);
    }

    public override void OnEveryFrame()
    {
        if (!creatureScript.CheckIfPlayerVisible())
        {
            //And the enemy reached the player's last known location...
            if (Vector3.Distance(targetPos, creatureScript.transform.position) <= destinationTolerance)
            {
                //The enemy changes to the patrol state
                ChangeState(creatureScript.idleState, ref creatureScript.currentState);
            }

            return;
        }
    }

    public override void OnEveryPhysicsFrame()
    {

    }

    public override void OnExitState()
    {
        creatureScript.animator.SetBool("IsRunning", false);
    }
}
