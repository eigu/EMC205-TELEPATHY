using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerCamera playerCamera;

    [SerializeField] private float speed;

    private Rigidbody rb;

    private void Start()
    {
        playerCamera = GetComponent<PlayerCamera>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var movementInput = InputManager.Instance.GetMovementInput();

        if (movementInput == Vector2.zero)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        Vector3 cameraForward = playerCamera.cameraHolder.forward;
        Vector3 cameraRight = playerCamera.cameraHolder.right;

        Vector3 direction = (cameraForward * movementInput.y + cameraRight * movementInput.x).normalized;
        Vector3 velocity = direction * speed * Time.deltaTime;
        velocity.y = rb.velocity.y;

        rb.velocity = velocity;
    }
}
