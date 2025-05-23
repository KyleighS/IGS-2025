using UnityEngine;

public class ChaseState : StateClass
{
    //Instead of recalculating every frame, we only do it every so often to save on performance
    public float recalculatePathEverySeconds = 1f;
    //Variable used to keep track of the time before the next path recalculation
    private float timer = 0f;
    //How close the enemy has to be to their destination to consider to have "arrived"
    public float destinationTolerance = 1f;
    //The destination of this enemy
    public Vector3 targetPos;

    public ChaseState(CreatureScript creatureScript) : base(creatureScript)
    {
    }

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
        creatureScript.animator.SetBool("IsRunning", true);
        creatureScript.audioSource.Play();
    }

    public override void OnEveryFrame()
    {
        timer -= Time.deltaTime;

        //If the player isn't visible...
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


        //If the player is visible...
        //And enough time has passed to recalculate the path to them...
        if (timer <= 0f)
        {
            //We set the new "last known location", calculate the path, and reset the timer
            targetPos = creatureScript.target.transform.position;
            creatureScript.navMeshAgent.SetDestination(targetPos);
            timer = recalculatePathEverySeconds;
        }
    }

    public override void OnEveryPhysicsFrame()
    {

    }

    public override void OnExitState()
    {
        creatureScript.animator.SetBool("IsRunning", false);
        creatureScript.audioSource.Stop();
    }

}
