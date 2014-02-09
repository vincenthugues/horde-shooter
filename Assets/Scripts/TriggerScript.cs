using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour
{
	public GameObject Receiver;
	public string TriggerEnterMessage = "";
	public string TriggerStayMessage = "";
	public string TriggerExitMessage = "";
	public string TriggeringFlag = "Player";

	void Start()
	{
	}

	void Update()
	{
	}

	private void OnTriggerEnter(Collider collider)
	{
		CheckTrigger(collider, TriggerEnterMessage);
	}
	
	private void OnTriggerStay(Collider collider)
	{
		CheckTrigger(collider, TriggerStayMessage);
	}
	
	private void OnTriggerExit(Collider collider)
	{
		CheckTrigger(collider, TriggerExitMessage);
	}

	// Send the message to the receiver if the method's name was set,
	// and if no flag was set or the colliding object has the set flag
	private void CheckTrigger(Collider collider, string methodName)
	{
		if (methodName != ""
			&& (TriggeringFlag != "" || collider.tag == TriggeringFlag))
			Receiver.SendMessage(methodName);
	}
}
