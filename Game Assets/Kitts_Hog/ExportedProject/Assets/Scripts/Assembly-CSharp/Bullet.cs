using UnityEngine;

public class Bullet : MonoBehaviour
{
	private float Speed = 3.5f;

	public GameObject Target;

	private void Start()
	{
	}

	private void Update()
	{
		if (Target != null)
		{
			float maxDistanceDelta = (float)Random.Range(1, 5) * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, Target.transform.position + new Vector3(0f, 0.5f, 0f), maxDistanceDelta);
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.name);
		player component = other.GetComponent<player>();
		if (component != null)
		{
			int num = component.Health - 1;
			if (num == 0)
			{
				Time.timeScale = 0f;
			}
			else
			{
				component.Health = num;
			}
			Object.Destroy(base.gameObject);
		}
	}
}
