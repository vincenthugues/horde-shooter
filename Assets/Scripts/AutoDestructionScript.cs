using UnityEngine;
using System.Collections;

public class AutoDestructionScript : MonoBehaviour
{
	public float MaximumDistance;

	private GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update()
	{
		if (Vector3.Distance(transform.position, player.transform.position) > MaximumDistance)
			Destroy(gameObject);
	}
}
