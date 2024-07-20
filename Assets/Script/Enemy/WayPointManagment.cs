using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManagment : MonoBehaviour
{
    public Transform otherWay;
   public  Transform enemy;
    public float rotVal;
    public bool fwd;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("BasicEnemy"))
        {
            collision.gameObject.GetComponent<EnemyMovement>().wayPoint = otherWay;
            enemy = collision.gameObject.transform;
            enemy.eulerAngles = new Vector2(enemy.eulerAngles.x, rotVal);
            enemy.GetComponent<EnemyMovement>().isFwd = fwd;
        }
    }

}
