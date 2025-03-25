using UnityEngine;

public class SoundClass
{
    public AudioSource source;
    public AudioClip clip;
    public Vector3 position;
    public float range;

    public SoundClass(AudioSource source, AudioClip clip, Vector3 position, float range)
    {
        this.source = source;
        this.clip = clip;
        this.position = position;
        this.range = range;
    }

    public void Play()
    {
        this.source.clip = this.clip;
        this.source.transform.position = this.position;
        this.source.Play();
        RangeNotify();
    }

    protected virtual void RangeNotify()
    {
        Collider[] listeners = Physics.OverlapSphere(this.position, this.range);
        //Gizmos.DrawSphere(this.position, this.range);

        //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sphere.transform.localScale = Vector3.one * this.range * 2f;
        //sphere.transform.position = this.position;

        foreach (var listener in listeners)
        {
            if (listener.TryGetComponent<CreatureHearing>(out var enemyHearing))
            {
                enemyHearing.CheckIfHeard(this);
            }
        }
    }
}
