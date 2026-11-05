using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 7f;
    public float jumpForce = 12f;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    bool grounded;
    float moveX;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnMove(InputValue value)
    {
        moveX = value.Get<Vector2>().x;
    }

    void OnJump()
    {
        if (Time.timeScale == 0f) return;

        if (grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            Sfx.I.Play(Sfx.I.jump);
        }
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        anim.SetFloat("Speed", Mathf.Abs(moveX));
        anim.SetBool("Grounded", grounded);

        if (moveX != 0)
            sr.flipX = moveX < 0;
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Ground")) grounded = true;
    }

    void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Ground")) grounded = false;
    }
}