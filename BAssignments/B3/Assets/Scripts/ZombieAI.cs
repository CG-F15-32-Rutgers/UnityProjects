using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {

	public Transform target;
	private float distance;
	public float lookAtDistance;
	public float attackRange;
	public float moveSpeed;
	public float Damping;
	Rigidbody theRigidBody;
	Renderer myRender;
	GameObject [] players;


	// Use this for initialization
	void Start () {
		theRigidBody = GetComponent<Rigidbody> ();
		 players = GameObject.FindGameObjectsWithTag("Character");
	//	tr_Player = GameObject.FindGameObjectWithTag ("Character").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float shortestDist = float.MaxValue;
		for( int i = 0; i < players.Length; i++ ) {
			float dist = Vector3.Distance( this.transform.position, players[i].transform.position );
			if( dist < shortestDist ) {
				target = players[i].transform;
				shortestDist = dist;
			}
		}

		//distance = Vector3.Distance (target.position, transform.position);
		if (shortestDist < lookAtDistance)
		{
			//Debug.Log("looing at person");
			lookAt();
		}
		if (shortestDist < attackRange)
		{
			attack ();
		}
	}
	void lookAt ()
	{
		Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
	}
	
	void attack ()
	{
		//Debug.Log("moving toward person");
        //theRigidBody.AddForce (transform.forward * moveSpeed);
        this.GetComponent<NavMeshAgent>().destination = target.position;
		//transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}
}
