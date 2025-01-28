using UnityEngine;

public class Movement : MonoBehaviour
{
    private float x;
    private float z;

    public CharacterController controller;
    public float speed = 15f;
    public float gravity = -9.8f;

    public Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
