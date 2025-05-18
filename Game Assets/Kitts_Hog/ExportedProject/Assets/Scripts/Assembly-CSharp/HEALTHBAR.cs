using UnityEngine;
using UnityEngine.UI;

public class HEALTHBAR : MonoBehaviour
{
	private Image healthBar;

	private float maxHealth = 100f;

	public static float health;

	private void Start()
	{
		healthBar = GetComponent<Image>();
		health = maxHealth;
	}

	private void Update()
	{
		healthBar.fillAmount = health / maxHealth;
	}
}
