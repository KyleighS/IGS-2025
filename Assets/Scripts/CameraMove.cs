using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;
    public float sensitivity = 100f;
    public Transform player;

    void Start()
    {
        //locks cursor to center of screen and hides it
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
            mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.Rotate(Vector3.up * mouseX);

    }
}
