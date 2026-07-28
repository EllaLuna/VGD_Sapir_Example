using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 500f;
    [SerializeField] private float jumpForce = 8.0f;

    private bool isGrounded = true;
    private Rigidbody2D rb;
    private Animator animator;

    float horizontalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on Player!");
        }

        if (animator == null)
        {
            Debug.LogError("Animator not found on Player!");
        }
    }
    void OnMove(InputValue inputValue)
    {
        Vector2 direction = inputValue.Get<Vector2>();
        horizontalInput = direction.x;
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Apply horizontal movement
        rb.linearVelocity = new Vector2(horizontalInput * speed , rb.linearVelocity.y);

        // Update animation speed
        animator.SetFloat("moveX", Mathf.Abs(horizontalInput));

        // Flip character based on direction
        if (!Mathf.Approximately(horizontalInput, 0f))
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1f, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Consider using tags or layers to check for valid ground
        if (Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        animator.SetBool("isJumping", true);
    }
}
