using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 500f;
    [SerializeField] private float jumpForce = 8.0f;

    private bool isGrounded = true;
    private Rigidbody2D rb;
    private Animator animator;

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

    private void Update()
    {
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

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

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
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
