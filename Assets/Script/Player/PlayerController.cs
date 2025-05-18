using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    Rigidbody2D rb;
    Animator anim;

    [Header("Player_Basic_Attributes")]
    public float speed;
    public float jumpForce;

    public bool groundCheck;

    [Header("Player_Dash")]
    public bool slideForward = true;
    bool isSliding;
    public float slideUnit;
    public float slideDuration = 0.5f;

    [Header("UI")]
    public RawImage image;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        MusicManager.Instance.PlayMusic(MusicManager.Instance.mainLevel);
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        anim.SetFloat("velocity", Mathf.Abs(horizontal));
    }


}