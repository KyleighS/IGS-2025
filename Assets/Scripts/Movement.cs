using UnityEngine;

public class Movement : MonoBehaviour
{
    private float x;
    private float z;

    public CharacterController controller;
    public float baseSpeed = 15f;
    public float curSpeed;
    public float gravity = -9.8f;
    public float sprintSpeed = 2f;

    public Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

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

    }
}
