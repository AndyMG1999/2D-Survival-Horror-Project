using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    public float forwardWalkSpeed = 3.0f;
    public float backwardWalkSpeed = 2.5f;
    public bool facingRight = true;

    Rigidbody2D rb;
    Vector2 moveDirection;

    void HandleRBMovement()
    {
        Vector2 newPosition = new Vector2();
        if (facingRight)
        {
            if (moveDirection.x >= 0) newPosition = rb.position + (moveDirection * forwardWalkSpeed * Time.deltaTime);
            else if (moveDirection.x < 0) newPosition = rb.position + (moveDirection * backwardWalkSpeed * Time.deltaTime);
        }
        else
        {
            if (moveDirection.x > 0) newPosition = rb.position + (moveDirection * backwardWalkSpeed * Time.deltaTime);
            else if (moveDirection.x <= 0) newPosition = rb.position + (moveDirection * forwardWalkSpeed * Time.deltaTime);
        }
        rb.MovePosition(newPosition);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = MoveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Logic for moving Rigidbody2D
        HandleRBMovement();
    }
}
