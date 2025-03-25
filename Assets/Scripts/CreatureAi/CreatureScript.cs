using UnityEngine;

public class CreatureScript : MonoBehaviour
{
    //The script in charge of controlling the navigation of this AI agent
    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    [Header("Awareness")]
    //The script that becomes aware of surrounding entities
    public CreatureAwareness awarenessSphere;
    //The taf of the player
    public string playerTag;
    //The target
    public GameObject target;

    [Header("Vision")]
    //The degree of visibility of this enemy
    public float coneOfVisionAngle;
    //The layers this enemy cannot see through
    public LayerMask visionLayers;
    //The eyes of this enemy
    public GameObject eyes;

    [Header("Hearing")]
    //The script handling the hearing of this enemy
    public CreatureHearing hearingScript;

    [Header("States")]
    //The current state of this enemy
    public StateClass currentState;
    //The other states that this enemy can have
    public StateClass idleState;
    public StateClass roamingState;
    public StateClass chaseState;
    public StateClass searchState;
    public StateClass hideState;
    public StateClass stalkState;
    public StateClass attackState;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //Get the navmeshagent in this game object
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        idleState = new IdleState(this);
        roamingState = new RoamingState(this);
        chaseState = new ChaseState(this);
        searchState = new SearchingState(this);
        //takeCoverState = new FSM_TakingCover(this);

        //Entry state gets assigned
        currentState = idleState;
        currentState.OnEnterState();
    }

    private void OnEnable()
    {
        //If we dont have an awareness sphere assigned we look for it here
        if (awarenessSphere == null)
            awarenessSphere = GetComponentInChildren<CreatureAwareness>();

        awarenessSphere.OnColliderEntersAwareness += TargetIfPlayer;
        //hearingScript.OnSoundHeard += InvestigateSound;
        //When the explosion awareness picks up on a sound, this enemy takes cover
    }

    private void OnDisable()
    {
        awarenessSphere.OnColliderEntersAwareness -= TargetIfPlayer;
        hearingScript.OnSoundHeard -= InvestigateSound;
    }

    // Update is called once per frame
    private void Update()
    {

        //Every frame we call the corresponding function on the current state
        currentState.OnEveryFrame();
    }

    private void FixedUpdate()
    {
        currentState.OnEveryPhysicsFrame();
    }

    /// <summary>
    /// Checks if the object that entered is the player and displays a message if that's the case
    /// </summary>
    /// <param name="col"></param>
    public void TargetIfPlayer(Collider col)
    {
        if (col.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Player is in awareness");
        }
    }

    /// <summary>
    /// Checks if the player is visible
    /// </summary>
    /// <returns>Whether the player is in range, in the FOV, and not blocked by a wall</returns>
    public bool CheckIfPlayerVisible()
    {
        //Debug.Log("checking if player is visable");
        Collider player;
        //Debug.Log(awarenessSphere.IsTagInRange(playerTag, out player));
        if (!awarenessSphere.IsTagInRange(playerTag, out player))
        {
            Debug.Log("Player isnt visable (TagRange)");
            return false;
        }

        //if (!IsObjectInRange(player.transform))
        //{
        //    Debug.Log("Player isnt visable (ObjRange)");

        //    return false;
        //}

        if (!IsObjectVisible(player.transform))
        {
            Debug.Log("Player isnt visable (ObjVis)");
            return false;
        }

        target = player.gameObject;
        Debug.Log("Player detected!");

        return true;
    }

    /// <summary>
    /// Returns whether the rangle between obj and this object is less than the cone of vision angle.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>Returns ture if obj is within the field of view of this enemy.</returns>
    public bool IsObjectInRange(Transform obj)
    {
        //Get us the angle between the direct line of vision of the eyes and the player in respect to the eyes
        float objAngle = Vector3.Angle(eyes.transform.forward, obj.position - eyes.transform.position);

        Debug.DrawRay(eyes.transform.position, obj.position - eyes.transform.position, (objAngle < coneOfVisionAngle) ? Color.red : Color.blue);

        return objAngle < coneOfVisionAngle;
    }

    /// <summary>
    /// Checks whether there's something blocking the view of obj from the enemy.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>Returns true if this object has uninterrupted view of obj</returns>
    public bool IsObjectVisible(Transform obj)
    {
        Ray ray = new Ray(eyes.transform.position, obj.position - eyes.transform.position);

        if (Physics.Raycast(ray, out RaycastHit hitObject, Mathf.Infinity, visionLayers))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            return hitObject.transform == obj;
        }

        Debug.DrawRay(ray.origin, ray.direction, Color.blue);
        return false;
    }

    /// <summary>
    /// When called, will change the enemy to the investigate state and have them check the position of the sound played
    /// </summary>
    /// <param name="sound">The sound played that will be investigated</param>
    public void InvestigateSound(SoundClass sound)
    {
        if (currentState is ChaseState)
            return;

        ((SearchingState)searchState).SetTargetPos(sound.position);
        currentState.ChangeState(searchState, ref currentState);
    }

    //public void TakeCover(SoundClass sound)
    //{
    //    if (sound is not WarningSoundClass)
    //    {
    //        Debug.Log("Ignoring sound because it's not a warning sound");
    //        return;
    //    }

    //    WarningSoundClass warningSound = (WarningSoundClass)sound;

    //    ((FSM_TakingCover)takeCoverState).SetExplosion(warningSound.explosionSource);
    //    currentState.ChangeState(takeCoverState, ref currentState);
    //}
}
