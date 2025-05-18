using UnityEngine;

public class obs : MonoBehaviour
{
	public GameObject player;

	private void OnCollisionEnter2D(Collision2D col)
	{
		Object.Destroy(player);
	}
}
