using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScene : MonoBehaviour
{
    public RawImage image;
    void Update()
    {
        if (Input.anyKeyDown) 
        {
            SceneManagement.Instance.LoadScene(image, SceneList.MainMenuScreen.ToString());
        }
    }
}
