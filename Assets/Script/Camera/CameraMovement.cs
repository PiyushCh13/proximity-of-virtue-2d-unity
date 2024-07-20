using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float lerpVal = 2;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        transform.position = Vector3.Lerp(transform.position,new Vector3(player.position.x,player.position.y,-10), lerpVal * Time.deltaTime);
       
    }
}
