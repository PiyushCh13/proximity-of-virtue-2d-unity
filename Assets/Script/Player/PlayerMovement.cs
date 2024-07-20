using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public bool grounCheck;
    public SpriteRenderer spriteRendererP;
    public Animator playerAnimator;
    public bool slideFwd;
    bool isSliding;
    public float slideUnit;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        BasicMove();
        Slide();
    }
    public void BasicMove()
    {
        if (isSliding)
            return;
        float horizontal = Input.GetAxis("Horizontal");



        if (Input.GetKeyDown(KeyCode.Space) && grounCheck)
        {
            rb.AddForce(Vector2.up * jumpForce);
            grounCheck = false;
            playerAnimator.Play("Jump");

        }

        if (rb.velocity.y < 0)
        {
            playerAnimator.Play("JumpDrop");
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (grounCheck)
            {
                rb.velocity = Vector2.right * horizontal * speed;
                spriteRendererP.flipX = false;
                playerAnimator.Play("Run");

            }
            else
            {
                speed = speed / 2;
                rb.velocity += Vector2.right * horizontal * speed * Time.deltaTime;
                spriteRendererP.flipX = false;
            }
            slideFwd = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            playerAnimator.Play("Idle");
            rb.velocity = Vector2.zero;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (grounCheck)
            {
                rb.velocity = Vector2.right * horizontal * speed;
                spriteRendererP.flipX = true;
                playerAnimator.Play("Run");
            }

            else
            {
                speed = speed / 2;
                rb.velocity += Vector2.right * horizontal * speed * Time.deltaTime;
                spriteRendererP.flipX = true;
            }
            slideFwd = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            playerAnimator.Play("Idle");
            rb.velocity = Vector2.zero;
            
        }
    }
    public void Slide()
    {
        if (grounCheck)
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if (slideFwd)
                {
                    StartCoroutine(SlideCo(slideUnit));
                }
                else
                    StartCoroutine(SlideCo(-slideUnit));
            }
        }
    }

    IEnumerator SlideCo(float unitToMove)
    {
        isSliding = true;
        float time = 0;
        float timeToslide = .6f;
        playerAnimator.Play("Slide");
        Vector3 startPos = transform.position;
        while (time < timeToslide)
        {
            time += Time.deltaTime;
            transform.position =
            Vector3.Lerp(transform.position, new Vector3(transform.position.x + unitToMove, transform.position.y, 0), time / timeToslide);
            yield return null;
        }
        playerAnimator.Play("Idle");
        isSliding = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            grounCheck = true;
            playerAnimator.Play("Idle");
            speed = 5;
        }
    }
}
