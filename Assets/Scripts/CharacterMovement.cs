using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f; // Degrees per second

    private Rigidbody rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // Movement (No Smoothing)
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    private void Update()
    {
        // Rotation (Look at Mouse Position)
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane plane = new Plane(Vector3.up, transform.position);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 lookPoint = ray.GetPoint(distance);
            Vector3 lookDirection = lookPoint - transform.position;
            lookDirection.y = 0f;

            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                Debug.DrawRay(transform.position, lookDirection * 2f, Color.green);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}