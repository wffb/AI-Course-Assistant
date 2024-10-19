using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public Vector2 movementBoundaryX = new Vector2(-10f, 10f); // X-axis movement boundary
    public Vector2 movementBoundaryZ = new Vector2(-10f, 10f); // Z-axis movement boundary

    private Rigidbody rb;
    private float yaw = -70.0f;
    private float pitch = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = false;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the game window and hide it
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f); // Set initial rotation
    }

    void Update()
    {
        // Mouse look/rotation handling
        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch -= sensitivity * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -89f, 89f); // Limit pitch to prevent flipping
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    void FixedUpdate()
    {
        // Keyboard movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement on the horizontal plane
        Vector3 move = transform.right * x + transform.forward * z;
        move.y = 0; // Ensure no movement on the Y-axis

        rb.velocity = move * speed;

        // Clamp the player's position within the movement boundaries
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, movementBoundaryX.x, movementBoundaryX.y),
            rb.position.y,
            Mathf.Clamp(rb.position.z, movementBoundaryZ.x, movementBoundaryZ.y)
        );
    }
}
