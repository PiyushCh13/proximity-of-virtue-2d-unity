using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum Menus 
{
    Settings,
    Control,
    MainMenu,
    Paused,
    InGame
}

public class UIManager : Singleton<UIManager>
{
    public void OpenMenu(GameObject desiredMenu , [Optional] GameObject InSceneMenuOne , [Optional] GameObject InSceneMenuTwo) 
    {
        desiredMenu.SetActive(true);

        if (InSceneMenuOne != null) { InSceneMenuOne.SetActive(false); }
        if(InSceneMenuTwo != null) { InSceneMenuTwo.SetActive(false); }
    }

    public void AnimateMenu(GameObject gameObject,Vector3 scale)
    {
        
    }
}
