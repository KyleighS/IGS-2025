using UnityEngine;

[CreateAssetMenu(fileName = "SO_SoundClip", menuName = "Scriptable Objects/Sound Clip_Scriptable Obj")]
public class SoundClip_ScriptableObj : ScriptableObject
{
    public AudioClip clip;
    public float range;

}
