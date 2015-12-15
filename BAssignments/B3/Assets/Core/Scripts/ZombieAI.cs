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

	// Use this for initialization
	void Start () {
		theRigidBody = GetComponent<Rigidbody> ();
	//	tr_Player = GameObject.FindGameObjectWithTag ("Character").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		distance = Vector3.Distance (target.position, transform.position);
		if (distance < lookAtDistance)
		{
			//Debug.Log("looing at person");
			lookAt();
		}
		if (distance < attackRange)
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
		Debug.Log("moving toward person");
		//theRigidBody.AddForce (transform.forward * moveSpeed);
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}
}
