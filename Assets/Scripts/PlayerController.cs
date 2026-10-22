using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2D;
    public Animator animator;
    public float jumpBufferTime;
    public AudioSource jumpSound;
    public AudioSource dieSound;
    public AudioSource slideSound;
    private bool isGrounded;
    public float jumpSpeed;
    private int jumpCount;
    public float downSpeed;
    private Vector2 coliderSize;
    private float jumpBufferCounter;

    private void Start()
    {
        coliderSize = boxCollider2D.size;
    }
    private void Update()
    {
        SetColliderSize();
        Slide();
        Jump();
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if (jumpBufferCounter > 0 && jumpCount <= 1)
        {
            animator.SetBool("isSliding", false);
            rb.linearVelocityY = jumpSpeed;
            jumpCount++;
            jumpSound.Play();
            jumpBufferCounter = 0;
        }
    }
    private void Slide()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.linearVelocityY = -downSpeed;
            if (isGrounded)
            {
                animator.SetBool("isSliding", true);
                slideSound.Play();
            }

        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("isSliding", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform" && collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
            animator.SetBool("Grounded", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        animator.SetBool("Grounded", false);
    }
    private void SetColliderSize()
    {
        if (animator.GetBool("isSliding"))
        {
            boxCollider2D.size = new Vector2(1.3f, 0.7f);
        }
        else
        {
            boxCollider2D.size = coliderSize;
        }
    }
}
