using UnityEngine;
using System.Collections;

public class PlayerNavController : MonoBehaviour {

	public int Damage = 10;
	public Vector3 targetPos;
	public GameObject Ground;
	public bool isPunch = false;
	UnityEngine.AI.NavMeshAgent _agent;

	Animator _anim;
	public Transform curTarget;
	void Start () {
	targetPos = transform.position;
	
	_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

	_anim = GetComponent<Animator>();

	}

	void Update () {
	
		if(Input.GetMouseButtonDown(1) && !isPunch)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Ground.GetComponent<Collider>().Raycast (ray,out hit, Mathf.Infinity))
			{
				targetPos = hit.point;
				curTarget = null;
			}
		}

		if(isPunch)
		{
			transform.LookAt(curTarget);
		}

		if(!isPunch){
		if(_agent.velocity == Vector3.zero)
        {
         GetComponent<Animator>().SetBool("stay",true);
        }
         if(_agent.velocity != Vector3.zero)
        {
         GetComponent<Animator>().SetBool("stay",false);
        }
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{

			isPunch = true;
			curTarget.GetComponent<AI_EnemyController>().Damage(Damage);
			targetPos = transform.position;
			_agent.velocity = Vector3.zero;
			_anim.SetBool("punch",true);
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
			_anim.SetBool("punch",false);
			isPunch = false;
		}
		_agent.SetDestination(targetPos);

	}

}
