using UnityEngine;
using System.Collections;

public class NavMeshAgentHelper : MonoBehaviour {

	public Transform target;
	UnityEngine.AI.NavMeshAgent agent;
	// Use this for initialization
	void Start () {
	agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Animator>().SetBool("OffMesh",agent.isOnOffMeshLink);
		agent.SetDestination(target.position);
	}
}
