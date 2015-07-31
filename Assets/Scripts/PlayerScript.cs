using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
	public int HitPoints;
	public float WalkingSpeed;
	public float SprintingSpeed;

	private List<GameObject> weapons = new List<GameObject>();
	private int currentWeapon = -1;
	private Vector3 aimingTarget = Vector3.zero;
	private GunScript weaponScript;

	void Start()
	{
	}
	
	void Update()
	{
		if (HitPoints > 0)
		{
			Vector3 displacement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

			float speed = Input.GetKey(KeyCode.LeftShift) ? SprintingSpeed : WalkingSpeed;

			if (displacement != Vector3.zero)
				transform.position += displacement * Time.deltaTime * speed;

			FaceTarget();

			float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
			if (mouseWheel != 0f)
				CycleWeapons(mouseWheel > 0f ? 1 : -1);

			if (Input.GetMouseButton(0))
				TriggerWeapon();

			Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
		}
		else
		{
			foreach (GameObject weapon in weapons)
				weapon.transform.parent = null;

			Destroy(gameObject);
		}
	}
	
	public void OnCollisionExit(Collision collision)
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	private void FaceTarget()
	{
		int layerMask = 1 << 10; // Collision mask with the ground's layer id
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f, layerMask))
		{
			aimingTarget = new Vector3(hit.point.x, transform.position.y, hit.point.z);
			transform.LookAt(aimingTarget);
		}
	}

	private void TriggerWeapon()
	{
		if (weaponScript != null)
			weaponScript.Trigger();
	}

	public void PickupWeapon(GameObject weapon)
	{
		if (weapon != null)
		{
			// Add the weapon to the list
			weapons.Add(weapon);
			// Make it a child object of the player
			weapon.transform.parent = transform;
			// Switch to the new weapon
			SwitchWeapons(weapons.Count - 1);
		}
	}

	public void SwitchWeapons(int weaponIndex)
	{
		// If a weapon is equiped, disable it
		if (currentWeapon != -1)
			weapons[currentWeapon].SetActive(false);

		currentWeapon = weaponIndex;
		// Enable the new weapon
		weapons[currentWeapon].SetActive(true);
		weaponScript = weapons[currentWeapon].GetComponent<GunScript>();
	}

	public void CycleWeapons(int direction)
	{
		if (direction > 0)
			SwitchWeapons((currentWeapon + 1) % weapons.Count);
		else if (direction < 0)
			SwitchWeapons(currentWeapon > 0 ? currentWeapon - 1 : weapons.Count - 1);
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

	public void AddAmmunition(int quantity)
	{
		if (quantity > 0 && weaponScript != null)
			weaponScript.Ammunition += quantity;
	}
}
