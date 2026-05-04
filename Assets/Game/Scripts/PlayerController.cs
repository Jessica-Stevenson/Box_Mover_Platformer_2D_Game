using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //Player movement settings
    public float moveSpeed = 6f;
    public float acceleration = 10f;
    public float jumpForce = 9f;
    public float fallMultiplier = 3.5f;
    public float lowJumpMultiplier = 2f;
    public float coyoteTime = 0.15f;
    private float coyoteCounter;
    public float jumpBufferTime = 0.15f;
    private float jumpBufferCounter;

    //Core components
    private Rigidbody2D rb;
    private PlayerControls controls;
    public Animator animator;

    //Input + state
    private Vector2 moveInput;
    private bool isGrounded;

    //Facing direction
    private bool facingRight = true;

    void Awake()
    {
        //Get Rigidbody component
        rb = GetComponent<Rigidbody2D>();

        //Create input system instance
        controls = new PlayerControls();

        //Movement input
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        //Jump input
        controls.Player.Jump.performed += ctx => jumpBufferCounter = jumpBufferTime;

    }

    void OnEnable()
    {
        //Enable input system when object is active
        controls.Player.Enable();
    }

    void OnDisable()
    {
        //Disable input system when object is inactive
        controls.Player.Disable();
    }

    void Update()
    {
        //Jump buffer countdown
        jumpBufferCounter -= Time.deltaTime;

        //Coyote time countdown
        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        //Try to jump if buffered
        if (jumpBufferCounter > 0f && coyoteCounter > 0f)
        {
            DoJump();
            jumpBufferCounter = 0f;
        }

        //Animation
        float animSpeed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", animSpeed);

        //Flip
        if (moveInput.x > 0 && !facingRight)
            Flip();
        else if (moveInput.x < 0 && facingRight)
            Flip();
    }

    void FixedUpdate()
    {
        //Smooth horizontal movement (acceleration)
        float targetSpeed = moveInput.x * moveSpeed;
        float smoothSpeed = Mathf.Lerp(rb.linearVelocity.x, targetSpeed, acceleration * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector2(smoothSpeed, rb.linearVelocity.y);

        //Faster fall when descending
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        //Better jump feel
        if (rb.linearVelocity.y > 0 && !controls.Player.Jump.IsPressed())
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void DoJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void Flip()
    {
        //Reverse facing direction
        facingRight = !facingRight;

        //Flip sprite on X axis
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            coyoteCounter = coyoteTime;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}