using UnityEngine;

[System.Serializable]
public class IdleState : StateClass
{
    //The time that this enemy waits in idle before going on patrol
    public float waitingTime = 5f;
    //The time remaining before this enemy changes states naturally
    protected float waitingTimeLeft = 5f;

    //The constructor, which also executes the basic constructor for this class
    public IdleState(CreatureScript creatureScript) : base(creatureScript) { }

    //The function that tells this state to change to a different one
    public override void ChangeState(StateClass newState, ref StateClass currentState)
    {
        //The current state executes its exit function
        currentState.OnExitState();
        //The current state is changed
        currentState = newState;
        //The new current state executes its enter function
        currentState.OnEnterState();
    }

    //On enter the variables for this state are reset and we display a message to let the player know the enemy is now waiting
    public override void OnEnterState()
    {
        waitingTimeLeft = waitingTime;
        creatureScript.navMeshAgent.speed = 5;
        //Debug.Log("Waiting");
    }

    //Every frame we count down from the waitingTimeLeft variable. When the timer reaches zero we change the state to patrolling
    public override void OnEveryFrame()
    {
        waitingTimeLeft -= Time.deltaTime;

        if (waitingTimeLeft <= 0)
        {
            ChangeState(creatureScript.roamingState, ref creatureScript.currentState);
            //creatureScript.animator.SetBool("IsWalking", true);
        }

        if (creatureScript.CheckIfPlayerVisible() && creatureScript.sceneName == "Night4")
        {
            ChangeState(creatureScript.chaseState, ref creatureScript.currentState);
        }

        if (creatureScript.CheckIfPlayerVisible() && creatureScript.sceneName == "Night3")
        {
            Debug.Log("Player is visable(I)");
            ChangeState(creatureScript.stalkState, ref creatureScript.currentState);
        }
    }

    //This state does nothing on every fixed frame
    public override void OnEveryPhysicsFrame()
    {

    }

    //When this enemy exits the idle state they display a message
    public override void OnExitState()
    {
        //Debug.Log("Alright time to do something.");
        creatureScript.animator.SetBool("IsWalking", true);
    }
}
