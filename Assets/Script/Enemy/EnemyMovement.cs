using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{

    public Transform wayPoint;
    public bool isPetrol;
    public bool isFwd;
    public Transform playerTransform;
    public float attackRadius;
    Coroutine stanCo;
    public float stanTime;
    public Transform stanfillGo;
    public Image fillBar;
    public bool isAttack;
    public Transform shootPlace;
    public GameObject glowBall;
    public float ballSpeed = 7;
    Coroutine attackCo;
    public float shootCooldown=2;
    private void Update()
    {
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
    IEnumerator StanCo()
    {

        float time = 0;
        while (time <= stanTime)
        {
            fillBar.fillAmount = Mathf.Lerp(0, 1, time / stanTime);
            time += Time.deltaTime;
            yield return null;
        }
        stanfillGo.gameObject.SetActive(false);
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
        if (transform.position.x < playerTransform.position.x)
            ballShoot.GetComponent<BallShoot>().ballSpeed = ballSpeed;
        else
            ballShoot.GetComponent<BallShoot>().ballSpeed = -ballSpeed;
        yield return new WaitForSeconds(shootCooldown);
        attackCo = null;
    }

}
