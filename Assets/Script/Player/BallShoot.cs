using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShoot : MonoBehaviour
{
    public float ballSpeed;
    public bool byEnemy;
    public bool byPlayer;
    public PlayerMovement player;
    public Vector2 direction = Vector2.right;
    // Start is called before the first frame update

    public BallShoot()
    {
        this.direction = Vector3.right;
    }
    public BallShoot(Vector2 direction)
    {
        this.direction = direction;
    }
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        Destroy(gameObject,3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * ballSpeed * Time.deltaTime);
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("BasicEnemy") && byPlayer)
        {
            if(player != null) 
            {
                player.enemy = collision.gameObject.GetComponent<EnemyMovement>();
            }

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
