using UnityEngine;

public class obstracle : MonoBehaviour
{
	public GameObject player;

	public AudioSource HitSound;

	private void Start()
	{
		HitSound = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		HitSound.Play();
		if (HEALTHBAR.health >= 0f)
		{
			HEALTHBAR.health -= 20f;
		}
		if (HEALTHBAR.health < 0f)
		{
			Object.Destroy(player);
		}
	}
}
