using UnityEngine;

public class DeletePlayerPrefs : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			PlayerPrefs.DeleteAll();
		}
	}
}
