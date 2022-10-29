using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 500f;

    [SerializeField] private float jumpForce = 8.0f;

    private bool isGrounded;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private float deltaX;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody == null)
        {
            Debug.LogError($"Failed to start. {rigidBody.GetType()} not found!");
        }
        animator = GetComponent<Animator>();
        if (animator == null || !animator.isActiveAndEnabled)
        {
            Debug.LogError($"Failed to start. {animator.GetType()} not found!");
        }
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
    }

    private void FixedUpdate()
    {
        deltaX = Input.GetAxis("Horizontal");

        movement = new Vector2(deltaX * speed * Time.deltaTime, rigidBody.velocity.y);

        rigidBody.velocity = movement;
        animator.SetFloat("moveX", Mathf.Abs(deltaX));

        //changes the animation direction of the character
        if (!Mathf.Approximately(deltaX, 0.0f))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1.0f, 1.0f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Only for Sonic scene
        if (SceneManager.GetActiveScene().name == "Example 1")
        {
            animator.SetBool("isJumping", true);
        }
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rigidBody.velocity.y > -0.1 &&
            rigidBody.velocity.y < 0.1)
        {
            animator.SetBool("isJumping", false);
            isGrounded = true;
        }
    }

}
