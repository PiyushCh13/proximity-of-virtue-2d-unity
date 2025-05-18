using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
	public int Health = 100;

	public Text HealthText;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnGUI()
	{
		HealthText.text = Health.ToString();
	}
}
