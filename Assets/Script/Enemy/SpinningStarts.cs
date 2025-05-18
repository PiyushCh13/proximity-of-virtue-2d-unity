using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningStarts : MonoBehaviour
{
    [SerializeField] Transform spinnStar;
    public float giveDam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spinnStar.transform.Rotate(Vector3.forward * 200 * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.gameObject.name.Contains("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().HealthManager(giveDam);
            Destroy(gameObject);
        }
       
    }
}
