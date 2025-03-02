using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private int initialSpeed = 8;
    private float speed;

    public float getSpeed()
    {
        return speed;
    }
    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private bool isJumping = false;
    [SerializeField] private Rigidbody rd;


    private PlayerInput playerInput;
    private InputAction moveRightAction;
    private InputAction moveLeftAction;
    private InputAction jumpAction;

    // Guarda el movimiento.
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        if (rd == null)
            rd = GetComponent<Rigidbody>();

        playerInput = GetComponent<PlayerInput>();

        if (playerInput == null)
        {
            Debug.LogError("PlayerInput no encontrado en el objeto.");
            return;
        }

        moveRightAction = playerInput.actions["MoveRight"];
        moveLeftAction = playerInput.actions["MoveLeft"];
        jumpAction = playerInput.actions["Jump"];

        if (moveRightAction == null) Debug.LogError("La acción 'MoveRight' no está definida en el Input System.");
        if (moveLeftAction == null) Debug.LogError("La acción 'MoveLeft' no está definida en el Input System.");
        if (jumpAction == null) Debug.LogError("La acción 'Jump' no está definida en el Input System.");

        speed = initialSpeed;
    }

    void Update()
    {
        DetectMovement();
        DetectJump();
        speed = speed + Time.deltaTime * 0.075f;
        Debug.Log("Speed: " + speed);
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    private void DetectJump()
    {
        if (jumpAction.ReadValue<float>() > 0 && !isJumping)
        {
            Debug.Log("Jump pressed!");
            isJumping = true;
            rd.AddForce(Vector3.up * 7, ForceMode.Impulse);
        }
    }

    private void DetectMovement()
    {
        moveDirection = Vector3.zero; // Reiniciar dirección

        if (moveRightAction != null && moveRightAction.ReadValue<float>() > 0)
            moveDirection += Vector3.right * 0.75f; // Así le quito un poco de velocidad que iba muy rápido.

        if (moveLeftAction != null && moveLeftAction.ReadValue<float>() > 0)
            moveDirection += Vector3.left * 0.75f;
    }

    private void ApplyMovement()
    {
        rd.linearVelocity = new Vector3(moveDirection.x * initialSpeed, rd.linearVelocity.y, speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
