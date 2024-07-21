using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] Transform ParallaxBackground_3;
    [SerializeField] Transform ParallaxBackground_2;
    [SerializeField] Transform ParallaxBackground_1;
    [SerializeField] Transform SolidBackground;
    [SerializeField] Transform player;

    [Header("Speed Manipulator")]
    [SerializeField] [Range(0, 1)] float ParallaxBackground_3_XSpeed;
    [SerializeField] [Range(0, 1)] float ParallaxBackground_3_YSpeed;
    [SerializeField][Range(0, 1)] float ParallaxBackground_2_XSpeed;
    [SerializeField][Range(0, 1)] float ParallaxBackground_2_YSpeed;
    [SerializeField][Range(0, 1)] float ParallaxBackground_1_XSpeed;
    [SerializeField][Range(0, 1)] float ParallaxBackground_1_YSpeed;
    [SerializeField][Range(0, 1)] float SolidBackground_YSpeed;
    [SerializeField][Range(0, 1)] float SolidBackground_XSpeed;

    [Header("Object Position")]
    [SerializeField] Vector3 ParallaxBackground_3_Offset;
    [SerializeField] Vector3 ParallaxBackground_2_Offset;
    [SerializeField] Vector3 ParallaxBackground_1_Offset;
    [SerializeField] Vector3 SolidBackground_Offset;

    Vector3 startPos_ParallaxBackground_1;
    Vector3 startPos_ParallaxBackground_2;
    Vector3 startPos_ParallaxBackground_3;
    Vector3 startPos_SolidBackground;

    [SerializeField] float min_Height_Offset;
    [SerializeField] float max_Height_Offset;

    // Start is called before the first frame update
    void Start()
    {
        startPos_ParallaxBackground_1 = ParallaxBackground_1.transform.position;
        startPos_ParallaxBackground_2 = ParallaxBackground_2.transform.position;
        startPos_ParallaxBackground_3 = ParallaxBackground_3.transform.position;
        startPos_SolidBackground = SolidBackground.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = new Vector2(player.transform.position.x, Mathf.Clamp(player.transform.position.y, min_Height_Offset, max_Height_Offset));
        ParallaxBackground_3.transform.position = startPos_ParallaxBackground_3 + new Vector3(distance.x * ParallaxBackground_3_XSpeed, distance.y * ParallaxBackground_3_YSpeed, distance.z) - ParallaxBackground_3_Offset;
        ParallaxBackground_2.transform.position = startPos_ParallaxBackground_2 + new Vector3(distance.x * ParallaxBackground_2_XSpeed, distance.y * ParallaxBackground_2_YSpeed, distance.z) - ParallaxBackground_2_Offset;
        ParallaxBackground_1.transform.position = startPos_ParallaxBackground_1 + new Vector3(distance.x * ParallaxBackground_1_XSpeed, distance.y * ParallaxBackground_1_YSpeed, distance.z) - ParallaxBackground_1_Offset;
        SolidBackground.transform.position = startPos_SolidBackground + new Vector3(distance.x * SolidBackground_XSpeed, distance.y * SolidBackground_YSpeed, distance.z) - SolidBackground_Offset;
    }
}
