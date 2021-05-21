using UnityEngine;
using System.Collections;

public class FireBallTrigger : MonoBehaviour {

	public float damage;
	public float speed;

	void Start()
	{
		Destroy(gameObject,10);
	}
	void Update()
	{
		transform.Translate(Vector3.forward*speed*Time.deltaTime);
	}
	void OnTriggerEnter(Collider Col)
	{
		if(Col.CompareTag("Player1"))
		{
			Col.gameObject.GetComponent<PlayerStats>().curLife -= damage;
			Destroy(gameObject);
		}
	}
}
