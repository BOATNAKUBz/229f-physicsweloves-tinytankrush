using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 20f;
    public float maxSpeed = 10f;
    public float drag = 5f;

    public float xRange = 10f;

    private float horizontalInput;
    private float currentSpeed;

    private InputAction moveAction;
    private Rigidbody rb;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        horizontalInput = moveAction.ReadValue<Vector2>().x;

        // --- ความเร่ง ---
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            currentSpeed += horizontalInput * acceleration * Time.fixedDeltaTime;
        }
        else
        {
            // --- ความหน่วง ---
            currentSpeed = Mathf.Lerp(currentSpeed, 0, drag * Time.fixedDeltaTime);
        }

        // จำกัดความเร็ว
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        // อัปเดต velocity (แก้จาก linearVelocity → velocity)
        rb.velocity = new Vector3(currentSpeed, rb.velocity.y, rb.velocity.z);

        // จำกัดพื้นที่การเคลื่อนที่
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
            currentSpeed = 0;
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
            currentSpeed = 0;
        }
    }
}