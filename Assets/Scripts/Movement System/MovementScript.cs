using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MovementScript : MonoBehaviour, PlayerControls.IPlayerMovementActions
{
    // Serialized fields
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    [SerializeField] private Transform aimTransform;
    [SerializeField] private TrailRenderer tr;

    // Player controls
    private PlayerControls playerControls;

    // Movement variables
    private float horizontal;
    [SerializeField] private float speed;
    private bool doubleJump;
    [SerializeField] private float jumpingPower;
    [SerializeField] private float doubleJumpingPower;
    [SerializeField] private float maxFallSpeed;

    // Dash variables
    private bool canDash = true;
    public bool isDashing;
    private bool hasDashed;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;
    [SerializeField] private float afterDashTime;
    [SerializeField] private float dashPushingPower;

    // Coyote time variables
    private float coyoteTimeCounter;
    [SerializeField] private float coyoteTime;

    // Coyote time check variables
    private bool coyoteTimeCheckBool;
    private float coyoteTimeCheckCounter;
    [SerializeField] private float coyoteTimeCheck;

    // Jump buffer variables
    private float jumpBufferCounter;
    [SerializeField] private float jumpBufferTime;

    // Facing direction variable
    public bool isFacingRight = true;


    private void Awake()
    {
        // Initialize player controls
        playerControls = new PlayerControls();
        playerControls.PlayerMovement.SetCallbacks(this);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for coyote time (allowing jump shortly after leaving the ground)
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Check for coyote time check (used for directional dash)
        if (IsGrounded())
        {
            coyoteTimeCheckCounter = coyoteTimeCheck;
        }
        else
        {
            coyoteTimeCheckCounter -= Time.deltaTime;
        }

        // Set coyoteTimeCheckBool based on coyoteTimeCheckCounter
        coyoteTimeCheckBool = coyoteTimeCheckCounter > 0;

        // Flip the character based on the mouse position
        Flip();

        // Read horizontal input
        horizontal = UnityEngine.Input.GetAxis("Horizontal");
    }

    // Fixed update is used for physics-related calculations
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        // Apply horizontal movement
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        // Apply additional velocity when dashing and not grounded
        if (hasDashed == true && !IsGrounded())
        {
            float pushingPower = isFacingRight ? dashPushingPower : -dashPushingPower;
            rb.AddForce(new Vector2(pushingPower, rb.velocity.y));
        }

        // Limit the fall speed
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
    }

    // InputSystem callbacks
    public void OnMove(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isDashing)
        {
            return;
        }

        // Handle jump buffer to allow jumping a short time before hitting the ground
        if (context.performed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Check for initial jump or double jump
        if (context.performed && IsGrounded())
        {
            doubleJump = false;
        }

        // Handle actual jumping and double jumping
        if (jumpBufferCounter > 0f)
        {
            if (coyoteTimeCounter > 0f || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.velocity = new Vector2(rb.velocity.x, doubleJump ? doubleJumpingPower : jumpingPower);

                jumpBufferCounter = 0f;
                doubleJump = !doubleJump;
            }
        }

        // Cancel coyote time if jump is canceled mid-air
        if (context.canceled && !IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);

            coyoteTimeCounter = 0f;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        hasDashed = true;

        yield return new WaitForSeconds(afterDashTime);
        hasDashed = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    // Check if the character is grounded
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Flip the character's sprite based on the mouse position
    private void Flip()
    {
        Vector3 mousePosition = UnityEngine.Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);

        isFacingRight = mousePosition.x >= playerPosition.x;

        // Flip the character's sprite
        transform.localScale = new Vector3(isFacingRight ? 1.53f : -1.53f, 1.527423f, 1.504272f);
    }
}
