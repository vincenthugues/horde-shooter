using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
	public int HitPoints;
	public float Speed;

	public EnemySpawnerScript spawner { get; set; }
	public GameObject target { get; set; }

	void Start()
	{
	}
	
	void Update()
	{
		if (HitPoints > 0)
			transform.Translate((target.transform.position - transform.position).normalized * Time.deltaTime * Speed);
		else
			Destroy(gameObject);
	}

	public void GetHit(int damage)
	{
		if (HitPoints > 0)
		{
			HitPoints -= damage;
		}
	}

	private void OnDestroy()
	{
		if (spawner != null)
			spawner.EnemyDied();
	}
}
