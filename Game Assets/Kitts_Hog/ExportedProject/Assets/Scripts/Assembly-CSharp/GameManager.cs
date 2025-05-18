using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject GameOverScene;

	private void Start()
	{
	}

	private void Update()
	{
		if (HEALTHBAR.health < 0f)
		{
			GameOverScene.SetActive(true);
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void BackToMainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void Level1()
	{
		SceneManager.LoadScene(1);
	}

	public void Level2()
	{
		SceneManager.LoadScene(2);
	}

	public void Level3()
	{
		SceneManager.LoadScene(3);
	}

	public void ExitMethod()
	{
		Application.Quit();
	}
}
