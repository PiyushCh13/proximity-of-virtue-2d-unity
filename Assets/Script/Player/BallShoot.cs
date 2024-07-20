using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShoot : MonoBehaviour
{
    public float ballSpeed;
    public bool byEnemy;
    public bool byPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * ballSpeed * Time.deltaTime);
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("BasicEnemy") && byPlayer)
        {
            collision.gameObject.GetComponent<EnemyMovement>().GetDamage();
            Destroy(gameObject);
        }
        if (collision.gameObject.name.Contains("Player") && byEnemy)
        {
            collision.gameObject.GetComponent<PlayerMovement>().HeathManger();
            Destroy(gameObject);
        }
    }
}
