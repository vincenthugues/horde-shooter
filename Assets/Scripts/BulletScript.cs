using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
	public int Damage;
	public Vector3 Movement;

	void Start()
	{
	}

	void Update()
	{
		transform.Translate(Movement * Time.deltaTime);
	}
	
	private void OnTriggerEnter(Collider collider)
	{
		EnemyScript enemyScript = collider.gameObject.GetComponent<EnemyScript>();

		if (enemyScript != null)
			enemyScript.GetHit(Damage);

		if (collider.gameObject.tag != "Pickup item"
		    && collider.gameObject.tag != "Bullet")
			Destroy(gameObject);
	}
}
