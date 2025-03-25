using UnityEngine;
using UnityEngine.Events;

public class CreatureHearing : MonoBehaviour
{
    public bool CanHearThroughWalls = true;
    public UnityAction<SoundClass> OnSoundHeard;

    public void CheckIfHeard(SoundClass sound)
    {
        Debug.Log("I heard something");

        if (CanHearThroughWalls)
        {
            OnSoundHeard.Invoke(sound);
            return;
        }
        else
        {
            //Debug.DrawLine();
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
