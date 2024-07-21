using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneList
{
    BossFightScene,
    DialogueScreen,
    EndingScene,
    Level1,
    MainMenuScreen

}

public class SceneManagement : Singleton<SceneManagement>
{
    [SerializeField] public Scene prevScene;
    public void LoadScene(RawImage image, string sceneName)
    {
        prevScene = SceneManager.GetActiveScene();

        StartCoroutine(FadeToBlack(image, sceneName));

        if (prevScene != SceneManager.GetActiveScene() && prevScene != null)
        {
            SceneManager.UnloadSceneAsync(prevScene);
        }
    }

    IEnumerator FadeToBlack(RawImage image , string sceneName) 
    {
        image.gameObject.SetActive(true);
        image.canvasRenderer.SetAlpha(0.0f);
        image.CrossFadeAlpha(1f, 0.5f, true);
        yield return new WaitForSeconds(0.5f);
        image.canvasRenderer.SetAlpha(1.0f);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        yield return new WaitForSeconds(0.5f);
        image.CrossFadeAlpha(0f, 0.2f, true);
        image.gameObject.SetActive(false);
    }
}
