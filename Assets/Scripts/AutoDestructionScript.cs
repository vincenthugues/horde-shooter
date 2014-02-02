using UnityEngine;
using System.Collections;

public class AutoDestructionScript : MonoBehaviour
{
	public float MaximumDistance;

	private GameObject player;
	private float timer = 0f;
	private float delay = 1f;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		timer = delay;
	}
	
	void Update()
	{
		timer -= Time.deltaTime;

		if (timer <= 0f)
		{
		    if (player != null && Vector3.Distance(transform.position, player.transform.position) > MaximumDistance)
				Destroy(gameObject);

			timer = delay;
		}
	}
}
