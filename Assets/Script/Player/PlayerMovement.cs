using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    Rigidbody2D rb;
    //[SerializeField] Camera cam;
    public Animator playerAnimator;

    [Header("Player_Settings")]
    public float speed;
    public float jumpForce;
    public bool groundCheck = true;
    public Transform playerTransform;
    public bool isAttacking;
    public float currentHealth = 10f;
    public float maxHealth = 10f;
    public float minZoom;
    public float maxZoom;
    public bool slideForward = true;
    bool isSliding;
    public float slideUnit;
    public float slideDuration = 0.5f;
    public Image healthFiller;
    public float healCurrentTime = 0f;
    public float healTimeDuration = 2.5f;
    public RawImage image;

    [Header("Player_Shooting")]
    public Transform shootArea;
    public GameObject glowBall;
    public float ballSpeed;

    [Header("HealBeam_Settings")]
    public LineRenderer healingBeamLineRenderer;
    bool healBeamOnce = false;
    [SerializeField] LayerMask healingBeamLayerMask;
    public EnemyMovement enemy;

    bool canFireTripleLaser = true;
    Vector3 attackDirection = Vector3.right;

    [Header("TripleLaser_Settings")]
    [SerializeField] float tripleLaserCooldown = 2f;

    [Header("SpellAttack_Settings")]
    [SerializeField] float spellParticle_Zoffset = -7.5f;
    [SerializeField] float spellAttackRadius = 15f;
    [SerializeField] GameObject spellParticle;

    [Header("Score_Settings")]
    public int hitWithoutDamage = 0;
    public int defeatWithoutDamage = 0;
    public int soulsCollected = 0;

    [Header("Special DashAttack Settings")]
    public int specialDashUnits;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healingBeamLineRenderer.SetPosition(0, shootArea.position);
        MusicManager.Instance.PlayMusic(MusicManager.Instance.mainLevel);
    }

    void Update()
    {
        if (!isSliding && !isAttacking)
        {
            HandleMovement();
            HandleJump();
            HandleSlide();
        }

        HandleAttacks();
        HandleHealingBeam();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            playerTransform.eulerAngles = horizontal > 0 ? Vector3.zero : new Vector3(0, 180, 0);
            slideForward = horizontal > 0;

            if (groundCheck)
            {
                rb.linearVelocity = Vector2.right * horizontal * speed;
                playerAnimator.Play("Run");
            }
            else
            {
                rb.linearVelocity += Vector2.right * horizontal * (speed / 2) * Time.deltaTime;
            }
        }
        else if (groundCheck)
        {
            playerAnimator.Play("Idle");
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck)
        {
            rb.AddForce(Vector2.up * jumpForce);
            groundCheck = false;
            playerAnimator.Play("Jump");
        }

        if (rb.linearVelocity.y < -0.5f && !groundCheck)
        {
            playerAnimator.Play("JumpDrop");
        }
    }

    void HandleSlide()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isSliding)
        {
            StartCoroutine(SlideCoroutine(slideForward ? slideUnit : -slideUnit));
        }
    }

    void HandleAttacks()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(TripleLaserCooldown());
        }

        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            rb.linearVelocity = Vector2.zero;
            playerAnimator.Play("Attack");
            attackDirection = slideForward ? Vector3.right : Vector3.left;
        }
    }

    void HandleHealingBeam()
    {
        if (enemy != null && enemy.isStuned)
        {
            if (Input.GetMouseButton(1))
            {
                healingBeamLineRenderer.gameObject.SetActive(true);
                healingBeamLineRenderer.positionCount = 2;
                healingBeamLineRenderer.SetPosition(0, shootArea.position);
                enemy.stanfillGo.gameObject.SetActive(false);

                healCurrentTime += Time.deltaTime;
                if (!healBeamOnce)
                {
                    Physics2D.Raycast(shootArea.position, enemy.transform.position - shootArea.position, healingBeamLayerMask);
                    healBeamOnce = true;
                }

                healingBeamLineRenderer.SetPosition(1, enemy.transform.position);

                if (healCurrentTime >= healTimeDuration)
                {
                    enemy.OnRecover();
                    ResetHealBeam();
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                Destroy(enemy.gameObject, 0.2f);
                ResetHealBeam();
            }
        }
    }

    void ResetHealBeam()
    {
        enemy = null;
        healCurrentTime = 0f;
        healBeamOnce = false;
        healingBeamLineRenderer.gameObject.SetActive(false);
        healingBeamLineRenderer.positionCount = 0;
    }

    IEnumerator SlideCoroutine(float unitToMove)
    {
        isSliding = true;
        playerAnimator.Play("Slide");

        Vector3 targetPos = transform.position + new Vector3(unitToMove, 0, 0);
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerAnimator.Play("Idle");
        isSliding = false;
    }

    IEnumerator TripleLaserCooldown()
    {
        if (canFireTripleLaser)
        {
            // You can enable actual triple laser here
            canFireTripleLaser = false;
            yield return new WaitForSeconds(tripleLaserCooldown);
            canFireTripleLaser = true;
        }
    }

    public void Attack()
    {
        GameObject ball = Instantiate(glowBall, shootArea.position, Quaternion.identity);
        var ballScript = ball.GetComponent<BallShoot>();
        ballScript.direction = attackDirection;
        ballScript.byPlayer = true;
        ballScript.ballSpeed = slideForward ? ballSpeed : -ballSpeed;
    }

    public void StopAttack() => isAttacking = false;

    public void HealthManager(float damage)
    {
        currentHealth = Mathf.Max(0f, currentHealth - damage);
        StartCoroutine(UpdateHealthUI());

        if (currentHealth <= 0f)
        {
            SceneManagement.Instance.LoadScene(image, SceneList.EndingScene.ToString());
        }
    }

    IEnumerator UpdateHealthUI()
    {
        float targetFill = currentHealth / maxHealth;
        float elapsed = 0f;
        float duration = 0.5f;
        float initialFill = healthFiller.fillAmount;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            healthFiller.fillAmount = Mathf.Lerp(initialFill, targetFill, elapsed / duration);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = true;
            speed = 5f;
            playerAnimator.Play("Idle");
        }
    }
}
