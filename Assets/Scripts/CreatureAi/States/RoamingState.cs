using UnityEngine;

[System.Serializable]
public class RoamingState : StateClass
{
    //The radius area for this enemy to chose a destination
    public float roamRange = 10f;
    //How close the enemy has to be to their destination to consider to have "arrived"
    public float destinationTolerance = 2f;
    //The destination of this enemy
    public Vector3 targetPos;


    public RoamingState(CreatureScript creatureScript) : base(creatureScript) { }

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
        //The new destination is a random point in a sphere around the enemy of radius patrolRange
        targetPos = creatureScript.transform.position + Random.insideUnitSphere * roamRange;
        //The enemyscript is told that their new destination is the targetPos
        creatureScript.navMeshAgent.SetDestination(targetPos);
        //We refresh the target position so it matches the one calculated by the navmesh
        //targetPos = creatureScript.navMeshAgent.pathEndPosition;
        //
        //Debug.Log("Moving to " + targetPos.ToString());
    }

    public override void OnEveryFrame()
    {
        //Debug.Log(Vector3.Distance(creatureScript.transform.position, targetPos));
        //If the distance between the enemy and its destination is less than the maximum accepted or the path cannot be reached
        //if (Vector3.Distance(creatureScript.transform.position, targetPos) <= destinationTolerance
        if ((Mathf.Abs(creatureScript.transform.position.x-targetPos.x)+Mathf.Abs(creatureScript.transform.position.z - targetPos.z)) <= destinationTolerance
        || creatureScript.navMeshAgent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            //Change back to the idle state
            ChangeState(creatureScript.idleState, ref creatureScript.currentState);
        }

        if (creatureScript.CheckIfPlayerVisible())
        {
            if(creatureScript.sceneName == "Night 3")
            {
                ChangeState(creatureScript.stalkState, ref creatureScript.currentState);
            }
            if(creatureScript.sceneName == "Night 4" || creatureScript.sceneName == "Night 5")
            {
                ChangeState(creatureScript.chaseState, ref creatureScript.currentState);
            }
        }

        if (creatureScript.creatureInView && creatureScript.sceneName == "Night 2" || creatureScript.sceneName == "Night 3")
        {
            ChangeState(creatureScript.hideState, ref creatureScript.currentState);
        }
    }

    public override void OnEveryPhysicsFrame()
    {

    }

    public override void OnExitState()
    {
        //Debug.Log("Leaving patrol state");
    }
    public void OnTriggerEnter(Collider other)
    {
        if(creatureScript.sceneName == "Night 4" || creatureScript.sceneName == "Night 5")
        {
            if (other.tag == "Player")
            {
                ChangeState(creatureScript.attackState, ref creatureScript.currentState);
            }

        }
    }
}
