using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    public InputAction DashAction;
    public float forwardWalkSpeed = 1.0f;
    public float backwardWalkSpeed = 0.5f;
    public float forwardDashSpeed = 7.0f;
    public float backwardDashSpeed = 4.0f;
    public float dashCooldown = 4.0f;
    public float dashDuration = 2.0f;
    public bool facingRight = true;


    Rigidbody2D rb;
    
    Vector2 moveDirection;
    Vector2 dashDirection;
    bool shouldDash = false;
    float dashCooldownTimeLeft = 0f;
    float dashDurationTimeLeft = 0f;



    // Function in charge of player movement
    void HandleRBMovement()
    {
        Vector2 newPosition = new Vector2();
        if (!shouldDash)
        {
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
        }
        else if (shouldDash && dashDurationTimeLeft > 0f)
        {
            if (facingRight && dashDirection.x >= 0) newPosition = rb.position + (dashDirection * forwardDashSpeed * Time.deltaTime);
            else if (facingRight && dashDirection.x < 0) newPosition = rb.position + (dashDirection * backwardDashSpeed * Time.deltaTime);
            else if (!facingRight && dashDirection.x > 0) newPosition = rb.position + (dashDirection * backwardDashSpeed * Time.deltaTime);
            else if (!facingRight && dashDirection.x <= 0) newPosition = rb.position + (dashDirection * forwardDashSpeed * Time.deltaTime);
        }
        
        rb.MovePosition(newPosition);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveAction.Enable();
        DashAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = MoveAction.ReadValue<Vector2>();
        // Logic for begining dash
        if (DashAction.ReadValue<float>() > 0f && dashCooldownTimeLeft <= 0f) 
        { 
            // Sets bool to true and gets dashDirection from move inputs so system knows where to dash
            shouldDash = true;
            dashDirection = moveDirection;
            
            // Limits dash to either fully right or fully left (No diagnal dashes or mini dashes :P)
            if (facingRight && dashDirection.x >= 0f) dashDirection = Vector2.right;
            else if (facingRight && dashDirection.x < 0f) dashDirection = Vector2.left;
            else if (!facingRight && dashDirection.x <= 0f) dashDirection = Vector2.left;
            else dashDirection = Vector2.right;

            dashDurationTimeLeft = dashDuration;
            dashCooldownTimeLeft = dashCooldown;
        }
        // Countdown for dash cooldown and dash duration
        if (dashCooldownTimeLeft > 0f) dashCooldownTimeLeft -= Time.deltaTime;
        if (dashDurationTimeLeft > 0f) dashDurationTimeLeft -= Time.deltaTime;
        // Conclude dash
        if(dashDurationTimeLeft < 0f) shouldDash = false;
    }

    private void FixedUpdate()
    {
        // Logic for moving Rigidbody2D
        HandleRBMovement();
    }
}
