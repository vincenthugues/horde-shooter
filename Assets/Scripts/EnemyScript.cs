using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
	public int HitPoints;
	public float Speed;
	public float AttackRange;
	public int AttackDamage;

	public EnemySpawnerScript spawner { get; set; }
	public GameObject target { get; set; }

	private float attackTimer = 0f;
	private Vector3 lastKnownTargetPosition;

	void Start()
	{
	}
	
	void Update()
	{
		if (HitPoints > 0)
		{
			if (target != null)
			{
				lastKnownTargetPosition = target.transform.position;

				transform.Translate((target.transform.position - transform.position).normalized * Time.deltaTime * Speed);

				if (attackTimer > 0f)
					attackTimer -= Time.deltaTime;

				if (Vector3.Distance(transform.position, target.transform.position) <= AttackRange
				    && target.tag == "Player"
				    && attackTimer <= 0f)
					Attack();
			}
			else //if (lastKnownTargetPosition != null)
				transform.Translate((lastKnownTargetPosition - transform.position).normalized * Time.deltaTime * Speed / 2);
			// else => Random movement?
		}
		else
			Destroy(gameObject);
	}

	private void Attack()
	{
		PlayerScript playerScript = target.GetComponent<PlayerScript>();

		if (playerScript != null)
		{
			playerScript.GetHit(AttackDamage);
			attackTimer = 1f;
		}
	}

	public void GetHit(int damage)
	{
		if (HitPoints > 0)
		{
			HitPoints -= damage;
		}
		
		if (HitPoints < 0)
			HitPoints = 0;
	}

	private void OnDestroy()
	{
		if (spawner != null)
			spawner.EnemyDied();
	}
}
