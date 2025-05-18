using UnityEngine;

public class Monster : MonoBehaviour
{
	private GameObject player;

	private float lastTimeShooted;

	public int ShootIntervalInSeconds = 15;

	public GameObject bullet;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		if (lastTimeShooted + (float)ShootIntervalInSeconds < Time.time)
		{
			GameObject obj = Object.Instantiate(Resources.Load<GameObject>("bullet"));
			obj.transform.position = base.transform.position;
			obj.GetComponent<Bullet>().Target = player;
			lastTimeShooted = Time.time;
		}
		Object.Instantiate(bullet, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z), Quaternion.identity);
	}
}
