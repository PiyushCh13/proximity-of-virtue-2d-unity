using UnityEngine;

public class movingobstracle : MonoBehaviour
{
	private Vector2 startPosition;

	private Vector2 newPosition;

	public int speed = 3;

	public int maxDistance = 2;

	private void Start()
	{
		startPosition = base.transform.position;
		newPosition = base.transform.position;
	}

	private void Update()
	{
		newPosition.x = startPosition.x + (float)maxDistance * Mathf.Sin(Time.time * (float)speed);
		base.transform.position = newPosition;
	}
}
