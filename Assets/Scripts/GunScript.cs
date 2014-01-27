using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
	public GameObject BulletPrefab;
	public float RateOfFire;
	public float BulletSpeed;

	private float fireTimer;
	private float fireDelay;

	void Start()
	{
		fireTimer = 0f;
		fireDelay = 1f / RateOfFire;
	}
	
	void Update()
	{
		if (fireTimer > 0f)
			fireTimer -= Time.deltaTime;
	}

	public void Trigger()
	{
		if (fireTimer <= 0f)
		{
			Shoot();

			fireTimer = fireDelay;
		}
	}

	private void Shoot()
	{
		GameObject bullet = Instantiate(BulletPrefab, transform.position + transform.forward, Quaternion.identity) as GameObject;

		BulletScript bulletScript = bullet.GetComponent<BulletScript>();
		bulletScript.Movement = transform.forward * BulletSpeed;
	}
}
