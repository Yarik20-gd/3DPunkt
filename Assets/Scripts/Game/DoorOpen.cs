using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {

	public Animation anim;
	public bool isOpen;
	void OnTriggerStay(Collider other)
	{
		if(other.CompareTag("Player1"))
		{
			if(Input.GetKeyDown(KeyCode.F) && !isOpen)
			{
				anim.Play("DoorOpen");	
				isOpen = true;
			}
		}
	}
	
}
