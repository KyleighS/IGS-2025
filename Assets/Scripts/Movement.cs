using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Footsteps")]
    public SoundClip_ScriptableObj sound;
    public AudioSource audioSource;
    protected SoundClass newSound;

    private float x;
    private float z;

    public CharacterController controller;
    public float baseSpeed = 15f;
    public float curSpeed;
    public float gravity = -9.8f;
    public float sprintSpeed = 2f;

    public Vector3 velocity;
    private bool playingAnim;

    private void Awake()
    {
        playingAnim = true;
        if (sound != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    { 

            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            //sprint if holding left shift
            if (Input.GetKey(KeyCode.LeftShift))
            {
                curSpeed = baseSpeed * sprintSpeed;
            }
            else
            {
                curSpeed = baseSpeed;
            }

            controller.Move(move * curSpeed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if (move.magnitude != 0 && !audioSource.isPlaying)
            {
                PlaySound();
            }
    }

    public virtual void PlaySound()
    {
        newSound = new SoundClass(audioSource, sound.clip, sound.range, this.transform.position);
        newSound.Play();

    }

    public bool setAnim()
    {
        playingAnim = false;
        return playingAnim;
    }
}
