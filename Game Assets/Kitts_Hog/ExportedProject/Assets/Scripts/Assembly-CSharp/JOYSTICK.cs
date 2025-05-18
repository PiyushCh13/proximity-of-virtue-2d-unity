using UnityEngine;

public class JOYSTICK : MonoBehaviour
{
	public Transform player;

	public float speed = 5f;

	private bool touchStart;

	private Vector2 pointA;

	private Vector2 pointB;

	public Transform circle;

	public Transform outerCircle;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
			circle.transform.position = pointA * -1f;
			outerCircle.transform.position = pointA * -1f;
			circle.GetComponent<SpriteRenderer>().enabled = true;
			outerCircle.GetComponent<SpriteRenderer>().enabled = true;
		}
		if (Input.GetMouseButton(0))
		{
			touchStart = true;
			pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
		}
		else
		{
			touchStart = false;
		}
	}

	private void FixedUpdate()
	{
		if (touchStart)
		{
			Vector2 vector = Vector2.ClampMagnitude(pointB - pointA, 2.1f);
			moveCharacter(vector * -1f);
			circle.transform.position = new Vector2(pointA.x + vector.x, pointA.y + vector.y) * -1f;
		}
		else
		{
			circle.GetComponent<SpriteRenderer>().enabled = false;
			outerCircle.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	private void moveCharacter(Vector2 direction)
	{
		player.Translate(direction * speed * Time.deltaTime);
	}
}
