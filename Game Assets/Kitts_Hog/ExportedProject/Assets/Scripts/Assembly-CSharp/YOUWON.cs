using UnityEngine;
using UnityEngine.SceneManagement;

public class YOUWON : MonoBehaviour
{
	public GameObject YouWon;

	private void Start()
	{
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		YouWon.SetActive(true);
	}

	public void End()
	{
		Application.Quit();
	}

	public void BackToMainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
