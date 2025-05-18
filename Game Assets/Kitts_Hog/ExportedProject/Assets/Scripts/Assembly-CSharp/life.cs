using UnityEngine;

public class life : MonoBehaviour
{
	public GameObject player;

	private void Start()
	{
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (HEALTHBAR.health < 100f)
		{
			HEALTHBAR.health += 40f;
			Object.Destroy(base.gameObject);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}
}
