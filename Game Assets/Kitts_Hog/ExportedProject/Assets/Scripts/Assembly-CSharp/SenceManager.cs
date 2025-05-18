using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceManager : MonoBehaviour
{
	private int nextToLoad;

	private void Start()
	{
		nextToLoad = SceneManager.GetActiveScene().buildIndex + 1;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		SceneManager.LoadScene(nextToLoad);
	}

	public void StartLevel()
	{
		SceneManager.LoadScene(1);
	}

	public void LoadLevel()
	{
		SceneManager.LoadScene(4);
	}
}
