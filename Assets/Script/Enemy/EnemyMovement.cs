using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum EnemyType
{
    BASIC_ENEMY,
    CORRUPT_DROID,
    BOSS_DROID
}

public class EnemyMovement : MonoBehaviour
{
    public EnemyType enemyType = EnemyType.BASIC_ENEMY;
    public Transform wayPoint;
    public bool isPetrol;
    public bool isFwd;
    public Transform playerTransform;
    public float attackRadius;
    Coroutine stunCo;
    public float stanTime;
    public Transform stanfillGo;
    public Image fillBar;
    public bool isAttack;
    public Transform shootPlace;
    public GameObject glowBall;
    public float ballSpeed = 7;
    Coroutine attackCo;
    public float shootCooldown = 2;
    public int currentHeathCount = 0;
    public int MaxHeathCount = 3;
    private PlayerMovement playerRef;

    [Header("Stun_&_Recover Settings")]
    public bool isStuned;
    [SerializeField]
    GameObject recoveryParticle;
    float enlightenDuration = 3;

    private void Start()
    {
        playerRef = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (playerTransform == null)
            return;
        if (isPetrol)
            Patrol();
        else
        {
            if (transform.position.x < playerTransform.position.x)
                transform.eulerAngles = new Vector2(transform.eulerAngles.x, 0);
            else
                transform.eulerAngles = new Vector2(transform.eulerAngles.x, 180);
        }

        if (isAttack)
            Attack();
    }

    public void Patrol()
    {

        transform.position = Vector2.MoveTowards(transform.position, wayPoint.position, Time.deltaTime * 2);
        if (Vector2.Distance(transform.position, playerTransform.position) < attackRadius)
        {
            isPetrol = false;
            isAttack = true;
        }
        stanfillGo.LookAt(Camera.main.transform);
    }

    public void OffStunCo()
    {
        if(stunCo != null )
        {
           StopCoroutine(stunCo);
           stunCo = null;
        }
    }
    IEnumerator StunCo()
    {
        stanfillGo.gameObject.SetActive(true);
        isStuned = true;
        float time = 0;
        isAttack= false;
        isPetrol = false;
        while (time <= stanTime)
        {
            fillBar.fillAmount = Mathf.Lerp(0, 1, time / stanTime);
            time += Time.deltaTime;
            yield return null;
        }
        stanfillGo.gameObject.SetActive(false);
        Destroy(gameObject,.2f); 

    }

    public void Attack()
    {
        if (attackCo == null)
            attackCo = StartCoroutine(AttackCo());
        if (Vector2.Distance(transform.position, playerTransform.position) > attackRadius)
        {
            isPetrol = true;
            isAttack = false;
        }
    }
    IEnumerator AttackCo()
    {
        GameObject ballShoot = Instantiate(glowBall, shootPlace.position, glowBall.transform.rotation);
        ballShoot.GetComponent<BallShoot>().byEnemy = true;
        if (transform.position.x < playerTransform.position.x)
            ballShoot.GetComponent<BallShoot>().ballSpeed = ballSpeed;
        else
            ballShoot.GetComponent<BallShoot>().ballSpeed = -ballSpeed;
        yield return new WaitForSeconds(shootCooldown);
        attackCo = null;
    }
    public void GetDamage()
    {
        currentHeathCount++;
        if (currentHeathCount >= MaxHeathCount)
        {
            if (stunCo == null)
                stunCo = StartCoroutine(StunCo());
        }
    }

    public void OnDestroy()
    {
        if(playerRef != null)
        {
            playerRef.hitWithoutDamage++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Entered Player");
    }

    public void Defeat()
    {
        if (isStuned) 
        {
            OnRecover();
        }
        else
        {
            OnKill();
        }
    }
    public void OnRecover()
    {
        StartCoroutine(Enlightenment(enlightenDuration));
    }

    IEnumerator Enlightenment(float duration)
    {
        //PlayParticle
        recoveryParticle.SetActive(true);
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);

    }
    public void OnKill() 
    {

    }
}
