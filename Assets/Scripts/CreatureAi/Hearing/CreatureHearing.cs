using UnityEngine;
using UnityEngine.Events;

public class CreatureHearing : MonoBehaviour
{
    public CreatureScript creatureScript;
    //Whether this enemy can hear through walls
    public bool canHearThroughWalls = true;
    //The event called when the sound is head
    public UnityAction<SoundClass> OnSoundHeard;

    /// <summary>
    /// Checks whether this enemy heard that sound
    /// </summary>
    /// <param name="sound"></param>
    public void CheckIfHeard(SoundClass sound)
    {
        //Debug.Log("I heard something");
        if(creatureScript.sceneName == "Night 4" || creatureScript.sceneName == "Night 5")
        {
            if (canHearThroughWalls)
            {
                OnSoundHeard?.Invoke(sound);
                return;
            }
            else
            {
                Debug.DrawLine(sound.position, this.transform.position, Color.green);
                if (Physics.Raycast(sound.position, (this.transform.position - sound.position).normalized, out RaycastHit hitInfo))
                {
                    if (hitInfo.transform != this.transform)
                    {
                        return;
                    }

                    OnSoundHeard?.Invoke(sound);
                }
            }

        }

    }
}
