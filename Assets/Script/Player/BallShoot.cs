using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShoot : MonoBehaviour
{
    public float ballSpeed;
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
}
