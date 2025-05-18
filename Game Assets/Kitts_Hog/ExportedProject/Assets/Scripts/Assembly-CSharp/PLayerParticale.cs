using UnityEngine;

public class PLayerParticale : MonoBehaviour
{
	public GameObject PlayerParticale;

	public GameObject PlayerCollision;

	public AudioSource HitSound;

	private void Start()
	{
		HitSound = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "obs")
		{
			Object.Instantiate(PlayerCollision, base.transform.position, Quaternion.identity);
		}
		if (col.gameObject.tag == "life")
		{
			HitSound.Play();
		}
		else
		{
			Object.Instantiate(PlayerParticale, base.transform.position, Quaternion.identity);
		}
	}
}
