using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Inputs")]
    public InputAction MoveAction;
    public InputAction DashAction;

    [Header("Movement Stats")]
    public float forwardWalkSpeed = 1.0f;
    public float backwardWalkSpeed = 0.5f;
    public float forwardDashSpeed = 2.0f;
    public float backwardDashSpeed = 1.25f;
    public float forwardRunSpeed = 1.5f;
    public float dashCooldown = 3.0f;
    public float dashDuration = 0.35f;
    public bool facingRight = true;


    Rigidbody2D rb;
    
    Vector2 moveDirection;
    Vector2 dashDirection;
    bool shouldDash = false;
    bool shouldRun = false;
    float dashCooldownTimeLeft = 0f;
    float dashDurationTimeLeft = 0f;



    // Function in charge of player movement
    void HandleRBMovement()
    {
        Vector2 newPosition = rb.position;
        if (!shouldDash && !shouldRun)
        {
            if (facingRight)
            {
                if (moveDirection.x >= 0) newPosition += (forwardWalkSpeed * Time.deltaTime * moveDirection);
                else if (moveDirection.x < 0) newPosition += (backwardWalkSpeed * Time.deltaTime * moveDirection);
            }
            else
            {
                if (moveDirection.x > 0) newPosition += (backwardWalkSpeed * Time.deltaTime * moveDirection);
                else if (moveDirection.x <= 0) newPosition += (forwardWalkSpeed * Time.deltaTime * moveDirection);
            }
        }
        else if (shouldDash && !shouldRun && dashDurationTimeLeft > 0f)
        {
            if (facingRight && dashDirection.x >= 0) newPosition += (forwardDashSpeed * Time.deltaTime * dashDirection);
            else if (facingRight && dashDirection.x < 0) newPosition += (backwardDashSpeed * Time.deltaTime * dashDirection);
            else if (!facingRight && dashDirection.x > 0) newPosition += (backwardDashSpeed * Time.deltaTime * dashDirection);
            else if (!facingRight && dashDirection.x <= 0) newPosition += (forwardDashSpeed * Time.deltaTime * dashDirection);
        }
        else if (shouldRun && !shouldDash)
        {
            if (facingRight && moveDirection.x > 0) newPosition += (forwardRunSpeed * Time.deltaTime * Vector2.right);
            else if (!facingRight && moveDirection.x < 0) newPosition += (forwardRunSpeed * Time.deltaTime * Vector2.left);
        }

        rb.MovePosition(newPosition);
    }

    void DashOrRunLogic()
    {
        if (DashAction.WasPressedThisFrame() && dashCooldownTimeLeft <= 0f)
        {
            Debug.Log("Time to Dash!");
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
        else if (DashAction.IsPressed() && shouldDash == false && ((facingRight && moveDirection.x > 0) || (!facingRight && moveDirection.x < 0)))
        {
            Debug.Log("Time To Run!");
            shouldRun = true;
        }
        else if (DashAction.WasReleasedThisFrame() || (facingRight && moveDirection.x <= 0) || (!facingRight && moveDirection.x >=0))
        {
            shouldRun = false;
        }
        // Countdown for dash cooldown and dash duration
        if (dashCooldownTimeLeft > 0f) dashCooldownTimeLeft -= Time.deltaTime;
        if (dashDurationTimeLeft > 0f) dashDurationTimeLeft -= Time.deltaTime;
        // Conclude dash
        if (dashDurationTimeLeft < 0f) 
        { 
            shouldDash = false;
        }
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
        // Logic for starting and ending a dash or run (Dash only starts if dash button is pressed and dashCooldown is over)
        DashOrRunLogic();

    }

    private void FixedUpdate()
    {
        // Logic for moving Rigidbody2D
        HandleRBMovement();
    }
}
