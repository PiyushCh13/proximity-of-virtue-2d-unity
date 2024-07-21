using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour 
{
    [SerializeField] RawImage image;

    private void Start()
    {
        MusicManager.Instance.PlayMusic(MusicManager.Instance.menuSong);
    }

    public void StartGame() 
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        SceneManagement.Instance.LoadScene(image, SceneList.DialogueScreen.ToString());
    }

    public void Quit()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        Application.Quit();
    }
}
