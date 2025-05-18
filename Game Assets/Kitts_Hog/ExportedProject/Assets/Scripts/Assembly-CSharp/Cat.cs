using UnityEngine;

public class Cat : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed = 5f;

	private float dirX;

	private float dirY;

	private Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		dirX = Input.GetAxis("Horizontal") * moveSpeed;
		dirY = Input.GetAxis("Vertical") * moveSpeed;
	}

	private void FixedUpdate()
	{
		rb.velocity = new Vector2(dirX, dirY);
	}
}
