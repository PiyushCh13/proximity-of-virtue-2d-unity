using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum BossState
{
    boss_IdelState,
    boss_AttackState1,
    boss_ShieldState,
    boss_AttackStat2,
    boss_AttackStat3,
    boss_StunState,
};

public class Boss : MonoBehaviour
{
    public BossState bossState = BossState.boss_IdelState;
    Animator bossAnimator;
    public Transform playerTransform;
    public GameObject spinningStart;
    public Transform attack1Pos;
    public Transform attack2Pos;
    public Transform attack3Pos;
    public Transform centerePos;
    [SerializeField] float starTravelTime;
    public GameObject explosive;
    public int countForPowerUp;
    Coroutine creatHeavyAttackCo;
    public bool canAttack;
    [SerializeField] float timeToGenSpecialAttackBalls = .2f;
    public float currentHealth = 10;
    public float maxHealth = 10;
    public Image healtFiller;
    private void Start()
    {
        bossAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canAttack)
            AttackBasic();
        if (playerTransform == null)
            StopAllCoroutines();
    }
    int random = 0;

    public void HeavyAttack()
    {
        if (bossState == BossState.boss_IdelState)
        {

        }
    }

    IEnumerator CreatHeavyAttackCo(Transform p1, Transform p2, Transform p3)
    {
        bossAnimator.Play("Boss_UltimateAttack_1");
        float time = 0;

        Vector2 endPos = p1.position;
        GameObject go = Instantiate(spinningStart, centerePos.position, spinningStart.transform.rotation);
        while (time < timeToGenSpecialAttackBalls)
        {
            go.transform.position = Vector2.Lerp(centerePos.position, p1.position, time / timeToGenSpecialAttackBalls);
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        endPos = p2.position;
        GameObject go2 = Instantiate(spinningStart, centerePos.position, spinningStart.transform.rotation);
        while (time < timeToGenSpecialAttackBalls)
        {
            go2.transform.position = Vector2.Lerp(centerePos.position, p2.position, time / timeToGenSpecialAttackBalls);
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        endPos = p3.position;
        GameObject go3 = Instantiate(spinningStart, centerePos.position, spinningStart.transform.rotation);
        while (time < timeToGenSpecialAttackBalls)
        {
            go3.transform.position = Vector2.Lerp(centerePos.position, p3.position, time / timeToGenSpecialAttackBalls);
            time += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(HeavyAttackCo(go, go2, go3));
    }


    IEnumerator HeavyAttackCo(GameObject go, GameObject go2, GameObject go3)
    {
        float time = 0;
        Vector2 endPos = playerTransform.position;
        Vector2 startPos = go.transform.position;
        while (time < starTravelTime)
        {
            if (go)
                go.transform.position = Vector2.Lerp(startPos, endPos, time / starTravelTime);
            time += Time.deltaTime;
            yield return null;
        }
        Instantiate(explosive, new Vector2(endPos.x, endPos.y + .5f), explosive.transform.rotation);
        Destroy(go, .2f);

        //second
        time = 0;
        endPos = playerTransform.position;
        startPos = go2.transform.position;
        while (time < starTravelTime)
        {
            if (go2)
                go2.transform.position = Vector2.Lerp(startPos, endPos, time / starTravelTime);
            time += Time.deltaTime;
            yield return null;
        }
        Instantiate(explosive, new Vector2(endPos.x, endPos.y + .5f), explosive.transform.rotation);
        Destroy(go2, .2f);
        //third
        time = 0;
        endPos = playerTransform.position;
        startPos = go3.transform.position;
        while (time < starTravelTime)
        {
            if (go3)
                go3.transform.position = Vector2.Lerp(startPos, endPos, time / starTravelTime);

            time += Time.deltaTime;
            yield return null;
        }
        Instantiate(explosive, new Vector2(endPos.x, endPos.y + .5f), explosive.transform.rotation);
        Destroy(go3, .2f);

        creatHeavyAttackCo = null;
        countForPowerUp = 0;
    }
    public void AttackBasic()
    {
        if (bossState == BossState.boss_IdelState && bossState != BossState.boss_AttackStat3)
        {
            random++;
            if (random % 2 == 0)
            {
                bossState = BossState.boss_AttackState1;
                bossAnimator.Play("Boss_Attack_1");
                countForPowerUp++;
            }
            else
            {
                bossState = BossState.boss_AttackStat2;
                bossAnimator.Play("Boss_Attack_2");
                countForPowerUp++;
            }

        }
        if (countForPowerUp >= 3)
        {
            if (creatHeavyAttackCo == null)
            {
                bossState = BossState.boss_AttackStat3;
                creatHeavyAttackCo = StartCoroutine(CreatHeavyAttackCo(attack1Pos, attack2Pos, attack3Pos));
            }

        }
    }

    public void ChangeStateToIdle()
    {
        bossAnimator.Play("Boss_Idle");
        bossState = BossState.boss_IdelState;
    }


    public void BasicAttack1()
    {
        if (playerTransform)
            StartCoroutine(BasicAttackCo(attack1Pos, playerTransform));
    }
    public void BasicAttack2()
    {
        if (playerTransform)
            StartCoroutine(BasicAttackCo(attack2Pos, playerTransform));
    }
    IEnumerator BasicAttackCo(Transform startPos, Transform end)
    {
        float time = 0;
        Vector2 endPos = end.position;
        GameObject go = Instantiate(spinningStart, startPos.position, spinningStart.transform.rotation);
        while (time < starTravelTime)
        {
            if (go)
                go.transform.position = Vector2.Lerp(startPos.position, endPos, time / starTravelTime);
            time += Time.deltaTime;
            yield return null;
        }
        Instantiate(explosive, new Vector2(endPos.x, endPos.y + .5f), explosive.transform.rotation);
        Destroy(go, .2f);
    }
    public void BossHealth(float cc)
    {
        if (currentHealth >= 0.1f)
        {
            currentHealth -= cc;
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

}
