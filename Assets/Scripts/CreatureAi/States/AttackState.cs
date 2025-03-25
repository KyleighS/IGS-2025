using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackState : StateClass
{
    public AttackState(CreatureScript creatureScript) : base(creatureScript) { } 

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
        creatureScript.gameManager.evidenceSlider.value = 0;
        SceneManager.LoadScene(creatureScript.gameManager.nextScene);
    }

    public override void OnEveryFrame()
    {

    }

    public override void OnEveryPhysicsFrame()
    {

    }

    public override void OnExitState()
    {

    }

}
