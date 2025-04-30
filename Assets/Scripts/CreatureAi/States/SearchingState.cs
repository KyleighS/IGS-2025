using UnityEngine;

public class SearchingState : StateClass
{
    //How close the enemy has to be to their destination to consider to have "arrived"
    public float destinationTolerance = 1f;
    //The destination of this enemy
    public Vector3 targetPos;

    public SearchingState(CreatureScript creatureScript) : base(creatureScript) { }

    public override void ChangeState(StateClass newState, ref StateClass currentState)
    {
        //The current state executes its exit function
        currentState.OnExitState();
        //The current state is changed
        currentState = newState;
        //The new current state executes its enter function
        currentState.OnEnterState();
    }

    //Allows us to set the target position of the investigation
    public void SetTargetPos(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    public override void OnEnterState()
    {
        //The enemyscript is told that their new destination is the targetPos
        creatureScript.navMeshAgent.SetDestination(targetPos);
        //We refresh the target position so it matches the one calculated by the navmesh
        targetPos = creatureScript.navMeshAgent.pathEndPosition;
        creatureScript.animator.SetBool("IsWalking", true);
        //
        //Debug.Log("Moving to " + targetPos.ToString());
    }

    public override void OnEveryFrame()
    {

        //If the distance between the enemy and its destination is less than the maximum accepted or the path cannot be reached
        if (Vector3.Distance(creatureScript.transform.position, targetPos) <= destinationTolerance
            || creatureScript.navMeshAgent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            //Change back to the idle state
            ChangeState(creatureScript.idleState, ref creatureScript.currentState);
        }

        if (creatureScript.CheckIfPlayerVisible())
        {
            if (creatureScript.sceneName == "Night 4" || creatureScript.sceneName == "Night 5")
            {
                ChangeState(creatureScript.chaseState, ref creatureScript.currentState);
            }
        }
    }

    public override void OnEveryPhysicsFrame()
    {

    }

    public override void OnExitState()
    {
        creatureScript.animator.SetBool("IsWalking", false);
        //Debug.Log("Leaving patrol state");
    }
}
