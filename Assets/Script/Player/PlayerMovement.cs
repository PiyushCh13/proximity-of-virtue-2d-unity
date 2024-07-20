using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public bool grounCheck;
    public Transform playerTransform;
    public Animator playerAnimator;
    public bool slideFwd;
    bool isSliding;
    public float slideUnit;
    public float timeToslide = .5f;
    public Transform shootArea;
    public GameObject glowBall;
    public float ballSpeed;
    public bool isAttacking;
    public Image healtFiller;
    public float currentHealth = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        BasicMove();
        Slide();

        if (Input.GetMouseButtonDown(0))
        {
            if (isSliding)
                return;

            rb.velocity = Vector3.zero;
            isAttacking = true;
            playerAnimator.Play("Attack");
        }
    }
    public void BasicMove()
    {
        if (isAttacking)
            return;
        if (isSliding)
            return;


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

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            float horizontal = Input.GetAxis("Horizontal");

            if (grounCheck)
            {
                rb.velocity = Vector2.right * horizontal * speed;
                playerTransform.eulerAngles = new Vector3(0, 0, 0);
                //spriteRendererP.flipX = false;

                playerAnimator.Play("Run");

            }
            else
            {
                speed = speed / 2;
                rb.velocity += Vector2.right * horizontal * speed * Time.deltaTime;
                playerTransform.eulerAngles = new Vector3(0, 0, 0);
            }
            slideFwd = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (grounCheck)
                playerAnimator.Play("Idle");
            rb.velocity = Vector2.zero;

        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            float horizontal = Input.GetAxis("Horizontal");


            if (grounCheck)
            {
                rb.velocity = Vector2.right * horizontal * speed;
                playerTransform.eulerAngles = new Vector3(0, 180, 0);
                playerAnimator.Play("Run");
            }

            else
            {
                speed = speed / 2;
                rb.velocity += Vector2.right * horizontal * speed * Time.deltaTime;
                playerTransform.eulerAngles = new Vector3(0, 180, 0);
            }
            slideFwd = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (grounCheck)
                playerAnimator.Play("Idle");
            rb.velocity = Vector2.zero;
            // cameraAnimator.Play("ZoomOut");

        }
    }
    public void Slide()
    {
        if (isAttacking)
            return;
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

    public void Attack()
    {

        GameObject ballShoot = Instantiate(glowBall, shootArea.position, glowBall.transform.rotation);
        ballShoot.GetComponent<BallShoot>().byPlayer = true;
        if (slideFwd)
            ballShoot.GetComponent<BallShoot>().ballSpeed = ballSpeed;
        else
            ballShoot.GetComponent<BallShoot>().ballSpeed = -ballSpeed;

    }

    public void StopAttack()
    {
        isAttacking = false;
    }


    public void HeathManger()
    {
        if (currentHealth >= 0.1f)
        {
            currentHealth--;
            StartCoroutine(ReduceHealth());
        }
        else
            Destroy(gameObject,.2f);
    }
    IEnumerator ReduceHealth()
    {
        float currHeath = currentHealth / 10;
        float time = 0;
        float tot = .5f;
        float fillAm = healtFiller.fillAmount;
        while (time < tot)
        {
            healtFiller.fillAmount = Mathf.Lerp(fillAm ,currHeath,time/tot);
            time += Time.deltaTime;
            yield return null;
        }

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
