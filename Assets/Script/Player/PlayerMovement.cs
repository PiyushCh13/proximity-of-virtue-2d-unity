using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    Rigidbody2D rb;
    [SerializeField] Camera cam;
    public Animator playerAnimator;

    [Header("Player_Settings")]
    public float speed;
    public float jumpForce;
    public bool grounCheck;
    public Transform playerTransform;
    public bool isAttacking;
    public float currentHealth = 10;
    public float maxHealth = 10;
    public float minZoom;
    public float maxZoom;
    public bool slideFwd;
    bool isSliding;
    public float slideUnit;
    public float timeToslide = .5f;
    public Image healtFiller;
    public float healCurrentTime = 0.0f;
    public float healTimeDuration = 2.5f;

    [Header("Player_Shooting")]
    public Transform shootArea;
    public GameObject glowBall;
    public float ballSpeed;

    [Header("HealBeam_Settings")]
    public LineRenderer healingBeamLineRenderer;
    bool healBeamOnce = false;
    [SerializeField]
    LayerMask healingBeamLayerMask;
    public EnemyMovement enemy;


    bool canFireTripleLaser = true;
    Vector3 attackDirection = Vector3.zero;

    [Header("TripleLaser_Settings")]
    [SerializeField]
    float tripleLaserCooldown = 2.0f;

    [Header("SpellAttack_Settings")]
    [SerializeField]
    float spellParticle_Zoffset = -7.5f;
    [SerializeField]
    float spellAttackRadius = 15.0f;
    [SerializeField]
    GameObject spellParticle;

    [Header("Score_Settings")]
    public int hitWithoutDamage = 0;
    public int defeatWithoutDamage = 0;
    public int soulsCollected = 0;

    [Header("Special DashAttack Settings")]
    public int specialDashUnits;


    void Start()
    {
        attackDirection = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
        healingBeamLineRenderer.SetPosition(0, shootArea.position);
        slideFwd = true;
        grounCheck = true;
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {

        BasicMove();
        if (Input.GetKeyDown(KeyCode.K))
        {
            //SpellAttack();
            TripleLaserFire();
            //SpecialDashAttack();
        }


        Slide();
        HealingBeamAttack();

        if (Input.GetMouseButtonDown(0))
        {
            if (isSliding)
                return;

            rb.velocity = Vector3.zero;
            isAttacking = true;
            playerAnimator.Play("Attack");
            attackDirection = Vector3.right;
            //  Attack();
        }
    }

    private void SpellAttack()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        {
            print("SpellAttack");
            {
                Instantiate(spellParticle, enemy.gameObject.transform.position + new Vector3(0.0f, 0.0f, spellParticle_Zoffset), transform.rotation);
            }
        }
    }

    private void TripleLaserFire()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        {
            if (canFireTripleLaser)
            {
                // TripleLaser();
                StartCoroutine(TripleLaserCooldown());
            }
        }
    }

    IEnumerator TripleLaserCooldown()
    {
        yield return new WaitForSeconds(tripleLaserCooldown);
        canFireTripleLaser = false;
        yield return new WaitForSeconds(tripleLaserCooldown);
        canFireTripleLaser = true;
    }

    private void HealingBeamAttack()
    {
        if (enemy != null)
        {
            if (enemy.isStuned)
            {
                if (Input.GetMouseButton(1))
                {
                    healingBeamLineRenderer.gameObject.SetActive(true);
                    healingBeamLineRenderer.positionCount = 2;
                    healingBeamLineRenderer?.SetPosition(0, shootArea.position);
                    enemy.stanfillGo.gameObject.SetActive(false);
                    healCurrentTime += Time.deltaTime;
                    Raycast_Heal();
                    if (healCurrentTime >= healTimeDuration)
                    {
                        enemy.OnRecover();
                        enemy = null;
                        healCurrentTime = 0;

                        healBeamOnce = false;
                        healingBeamLineRenderer.gameObject.SetActive(false);
                        healingBeamLineRenderer.positionCount = 0;
                        return;
                    }

                }
                if (Input.GetMouseButtonUp(1))
                {
                    Destroy(enemy.gameObject, .2f);
                    enemy = null;
                    healCurrentTime = 0;
                    healBeamOnce = false;
                    healingBeamLineRenderer.gameObject.SetActive(false);
                    healingBeamLineRenderer.positionCount = 0;
                }
            }

        }
    }

    public void BasicMove()
    {
        PlayerCamZoom();

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

        if (rb.velocity.y < -0.5f && !grounCheck)
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
            // rb.velocity = Vector2.zero;

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
            // rb.velocity = Vector2.zero;
            // cameraAnimator.Play("ZoomOut");

        }
    }
    public void Slide()
    {
        if (isAttacking)
            return;

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (!isSliding)
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

    IEnumerator SpecialDashCo(float unitToMove)
    {
        isSliding = true;
        float time = 0;

        playerAnimator.Play("Slide");
        Vector3 startPos = transform.position;
        while (time < timeToslide)
        {
            time += Time.deltaTime;
            transform.position =
            Vector3.Lerp(transform.position, new Vector3(enemy.gameObject.transform.position.x + unitToMove, transform.position.y, 0), time / timeToslide);
            yield return null;
        }

        transform.position = new Vector3(enemy.gameObject.transform.position.x + unitToMove, transform.position.y, 0);

        if (enemy != null)
        {
            var collider = enemy.gameObject?.GetComponent<Collider2D>();
            collider.isTrigger = false;
        }

        playerAnimator.Play("Idle");
        isSliding = false;

    }


    public void Attack()
    {

        print("AttackDirection " + attackDirection.magnitude.ToString());
        //Condition if attackdirection is zero
        if (attackDirection.magnitude < 1)
        {
            attackDirection = Vector3.right;
        }

        GameObject ballShoot = Instantiate(glowBall, shootArea.position, glowBall.transform.rotation);
        ballShoot.GetComponent<BallShoot>().direction = attackDirection;
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


    public void HeathManger(float cc)
    {
        if (currentHealth >= 0.1f)
        {
            currentHealth  -= cc;
            StartCoroutine(ReduceHealth());
        }
        else
            Destroy(gameObject, .2f);
    }
    IEnumerator ReduceHealth()
    {
        float currHeath = currentHealth / maxHealth;
        float time = 0;
        float tot = .5f;
        float fillAm = healtFiller.fillAmount;
        while (time < tot)
        {
            healtFiller.fillAmount = Mathf.Lerp(fillAm, currHeath, time / tot);
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


    RaycastHit2D rayCast;
    private void Raycast_Heal()
    {
        if (enemy != null)
        {
            if (!healBeamOnce)
            {
                rayCast = Physics2D.Raycast(shootArea.position, enemy.gameObject.transform.position - shootArea.transform.position, healingBeamLayerMask);
                print("Raycast Object " + rayCast.collider.gameObject.name);
                print("Calling Raycast_Heal");
                healBeamOnce = true;
            }


            healingBeamLineRenderer.SetPosition(1, enemy.transform.position);
            if (healingBeamLineRenderer.GetPosition(0) == Vector3.zero)
            {
                healingBeamLineRenderer.enabled = false;
            }

        }

    }

    //private void TripleLaser()
    //{
    //    attackDirection = Vector3.right;
    //    Attack();
    //    attackDirection = Vector3.up + Vector3.right;
    //    attackDirection.Normalize();
    //    Attack();
    //    attackDirection = Vector3.down + Vector3.right;
    //    attackDirection.Normalize();
    //    Attack();
    //}

    private void Boss_Heal()
    {
        //Instantiate 5 droid
        // then heal
    }

    private void Random_Attack()
    {
        int rand = UnityEngine.Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                TripleLaserFire();
                break;
            case 1:
                SpellAttack();
                break;
            case 2:
                SpecialDashAttack();
                break;
        }

    }

    private void SpecialDashAttack()
    {
        if (isAttacking)
            return;
        if (grounCheck)
        {
            if (enemy != null)
            {
                var collider = enemy.gameObject?.GetComponent<Collider2D>();
                collider.isTrigger = true;
            }
            {
                if (slideFwd)
                {
                    StartCoroutine(SpecialDashCo(specialDashUnits));
                }
                else
                    StartCoroutine(SpecialDashCo(-specialDashUnits));
            }
        }
    }

    private void PlayerCamZoom()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.y) > 0.1f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, minZoom, Time.deltaTime * 2f);
        }
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, maxZoom, Time.deltaTime * 2f);
        }
    }

    //IEnumerator HealBeamFireCo(Vector3 startPos, Vector3 finishPos, float duration)
    //{
    //    float time = 0.0f;
    //    while (time < duration)
    //    {
    //        time += Time.deltaTime;
    //        startPos = Vector3.Lerp(startPos, finishPos, time / duration);
    //        healingBeamLineRenderer.positionCount = 1;
    //        healingBeamLineRenderer?.SetPosition(1, startPos);
    //        yield return null;
    //    }

    //    startPos = finishPos;
    //    healingBeamLineRenderer.positionCount = 0;

    //}


}
