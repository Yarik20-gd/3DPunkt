using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AI_EnemyController : MonoBehaviour {
    public float maxHP = 100;
	public float curHP = 100; 
	public Vector3 offset;

	public GameObject FireBall;
	public float DistanceToFire;
	public float DistanceToWalk;
	public float DistanceToBite;
	public float DelayFire;
	public float DelayBite;
	public float DelayBetweenAnim;
	public GameObject Player;
	UnityEngine.AI.NavMeshAgent _agent;
	public Animator anim;
	public float curDistance;
	public bool isFire;
	public bool isBite;

	public GameObject enemyHP;
	
	public EnemyHP HPslider;

	void Start () {
	_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	Player = GameObject.FindGameObjectWithTag("Player1");
	GameObject hp = Instantiate(enemyHP,Vector3.zero,Quaternion.identity) as GameObject;
	hp.transform.SetParent(GameObject.Find("Canvas").transform);
	HPslider = 	hp.GetComponent<EnemyHP>();
	hp.transform.SetAsFirstSibling();
	hp.GetComponent<EnemyHP>().maxHP = maxHP;
	hp.GetComponent<EnemyHP>().curHP = curHP;
	hp.GetComponent<EnemyHP>().Enemy = gameObject;
	hp.GetComponent<EnemyHP>().offset = offset;
	}
	void Update () {

		if(HPslider.curHP <= 0)
		{
			_agent.enabled = false;
			anim.enabled = false;
			GetComponent<Rigidbody>().isKinematic = false;
			GetComponent<Rigidbody>().AddForce(Vector3.right * 300);
			Player.GetComponent<PlayerStats>().curExp += 250;
			Destroy(HPslider.gameObject);
			this.enabled = false;
		}

		float distance = Vector3.Distance(Player.transform.position,transform.position);
		curDistance = distance;
		if(distance > DistanceToFire)
		{
			return;
		}
		if(distance < DistanceToFire && distance > DistanceToWalk)
		{
			// Стрелять
			transform.LookAt(Player.transform);
			if(!isFire)
			{
				StartCoroutine(Fire());
			}
		}
		if(distance < DistanceToWalk && distance > DistanceToBite)
		{
			//Преследовать
			_agent.SetDestination(Player.transform.position);
			anim.SetBool("isFire",false);
			anim.SetBool("isBite",false);			
		}

		if(distance < DistanceToWalk && distance < DistanceToBite)
		{
			//Преследовать и кусать
			_agent.SetDestination(Player.transform.position);
			transform.LookAt(Player.transform);
			if(!isBite)
			{
				StartCoroutine(Bite());
			}		
		}
	}

	void FireBallR()
	{
		GameObject fB = Instantiate(FireBall,transform.position,transform.rotation) as GameObject;
	}
	IEnumerator Fire()
	{
		isFire = true;
		anim.SetBool("isFire",true);
		yield return new WaitForSeconds(DelayFire/2);
		FireBallR();
		yield return new WaitForSeconds(DelayFire/2);
		anim.SetBool("isFire",false);
		yield return new WaitForSeconds(DelayBetweenAnim);
		isFire = false;
	}
	IEnumerator Bite()
	{
		isBite = true;
		anim.SetBool("isBite",true);
		Player.GetComponent<PlayerStats>().curLife -= 5;
		yield return new WaitForSeconds(DelayBite);
		anim.SetBool("isBite",false);
		yield return new WaitForSeconds(DelayBetweenAnim);
		isBite = false;
	}
	void OnMouseDown()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Player.GetComponent<PlayerNavController>().curTarget = gameObject.transform;
		}
	}

	public void Damage(int dmg)
	{
		HPslider.curHP = HPslider.curHP - dmg;
	}

}
