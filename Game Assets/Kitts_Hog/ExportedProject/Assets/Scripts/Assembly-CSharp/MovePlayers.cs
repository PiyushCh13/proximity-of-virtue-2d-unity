using UnityEngine;

public class MovePlayers : MonoBehaviour
{
	public float moveSpeed = 5f;

	public VJHandler jsMovement;

	private Vector3 direction;

	private float xMin;

	private float xMax;

	private float yMin;

	private float yMax;

	private void Update()
	{
		direction = jsMovement.InputDirection;
		if (direction.magnitude != 0f)
		{
			base.transform.position += direction * moveSpeed;
			base.transform.position = new Vector3(Mathf.Clamp(base.transform.position.x, xMin, xMax), Mathf.Clamp(base.transform.position.y, yMin, yMax), 0f);
		}
	}

	private void Start()
	{
		xMax = Screen.width - 50;
		xMin = 50f;
		yMax = Screen.height - 50;
		yMin = 50f;
	}
}
