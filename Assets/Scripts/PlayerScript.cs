using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	public int HitPoints;
	public float MovementSpeed;
	public GameObject Gun;

	private Vector3 aimingTarget = Vector3.zero;
	private GunScript gunScript;

	void Start()
	{
		if (Gun != null)
		{
			Gun.transform.parent = transform;
			gunScript = Gun.GetComponent<GunScript>();
		}
	}
	
	void Update()
	{
		if (HitPoints > 0)
		{
			Vector3 displacement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

			if (displacement != Vector3.zero)
				transform.position += MovementSpeed * Time.deltaTime * displacement;

			FaceTarget();

			if (Input.GetMouseButton(0))
				TriggerWeapon();

			Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
		}
		else
		{
			if (Gun != null)
				Gun.transform.parent = null;

			Destroy(gameObject);
		}
	}

	private void FaceTarget()
	{
		int layerMask = 1 << 10; // Ground's layer
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
		if (gunScript != null)
			gunScript.Trigger();
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
