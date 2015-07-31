using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
	public int HitPoints;
	public float MovementSpeed;
	public float AttackRange;
	public int AttackDamage;
	public GameObject AmmoPickupPrefab;

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
				float distance = Vector3.Distance(transform.position, target.transform.position);

				if (attackTimer > 0f)
					attackTimer -= Time.deltaTime;
				
				if (distance > AttackRange)
				{
					Vector3 displacement = (target.transform.position - transform.position).normalized;
					if (displacement != Vector3.zero)
						transform.position += MovementSpeed * Time.deltaTime * displacement;
				}
				else if (Vector3.Distance(transform.position, target.transform.position) <= AttackRange
				    && target.tag == "Player"
				    && attackTimer <= 0f)
					Attack();
			}
			else //if (lastKnownTargetPosition != null)
				transform.Translate((lastKnownTargetPosition - transform.position).normalized * Time.deltaTime * MovementSpeed / 2);
			// else => Random movement?
		}
		else
		{
			if (Random.Range(0f, 1f) >= .75f)
				Instantiate(AmmoPickupPrefab, transform.position, Quaternion.identity);
			
			Destroy(gameObject);
		}
	}
	
	public void OnCollisionExit(Collision collision)
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
	
	private void OnDestroy()
	{
		if (spawner != null)
			spawner.EnemyDied();
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
}
