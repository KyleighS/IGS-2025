using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetTrigger("Walk");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetTrigger("Running");
            }
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }
}

