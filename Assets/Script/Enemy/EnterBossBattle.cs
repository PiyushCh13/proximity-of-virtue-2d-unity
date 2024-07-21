using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBossBattle : MonoBehaviour
{
    public Camera mainCamera;
    public Camera battleCamera;
    
    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player")) 
        {
            mainCamera.enabled = false;
            battleCamera.enabled = true;
        }
    }
}
