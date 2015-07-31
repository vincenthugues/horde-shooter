using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
	public GameObject BulletPrefab;
	public int Ammunition;
	public float RateOfFire;
	public float BulletSpeed;
	public float BulletSpawnDistance;
	public float Spread;

	private float fireTimer = 0f;
	private float fireDelay;
	private bool inInventory = false;

	void Start()
	{
		fireDelay = 1f / RateOfFire;
	}
	
	void Update()
	{
		if (fireTimer > 0f)
			fireTimer -= Time.deltaTime;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (!inInventory && collider.gameObject.tag == "Player")
		{
			PlayerScript playerScript = collider.gameObject.GetComponent<PlayerScript>();
			playerScript.PickupWeapon(gameObject);

			transform.position = collider.gameObject.transform.position;
			transform.localPosition += new Vector3(.28f, .0f, .6f);
			transform.rotation = collider.gameObject.transform.localRotation;
			
			inInventory = true;
			gameObject.GetComponent<Collider>().enabled = false;
		}
	}

	public void Trigger()
	{
		if (fireTimer <= 0f)
		{
			if (Ammunition > 0)
			{
				for (int i = 0; i < BulletPrefab.GetComponent<BulletScript>().ProjectilesPerShot; i++)
					Shoot();
				Ammunition--;
				fireTimer = fireDelay;
			}
		}
	}

	private void Shoot()
	{
		GameObject bullet = Instantiate(BulletPrefab, transform.position + transform.forward * BulletSpawnDistance, Quaternion.identity) as GameObject;

		BulletScript bulletScript = bullet.GetComponent<BulletScript>();
		bulletScript.Movement = transform.forward * BulletSpeed;
		bullet.transform.Rotate(Vector3.up, Random.Range(-Spread/2, Spread/2));
	}
}
