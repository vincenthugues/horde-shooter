using UnityEngine;
using System.Collections;

public class AmmoPickupScript : MonoBehaviour
{
	public int Quantity;

	void Start()
	{
	}
	
	void Update()
	{
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<PlayerScript>().AddAmmunition(Quantity);
			Destroy(gameObject);
		}
	}
}
