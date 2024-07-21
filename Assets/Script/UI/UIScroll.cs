using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScroll : MonoBehaviour
{
    [SerializeField] private Rect amounttoScroll;
    [SerializeField] float xSpeed;

    private RawImage raw_Image;

    private void Awake()
    {
        raw_Image = GetComponent<RawImage>();
    }

    void Update()
    {
        amounttoScroll.position += new Vector2(xSpeed, 0f) * Time.deltaTime;
        raw_Image.uvRect = amounttoScroll;
    }
}
