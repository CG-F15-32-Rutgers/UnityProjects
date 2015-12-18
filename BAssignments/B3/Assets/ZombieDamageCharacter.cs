using UnityEngine;
using System.Collections;

public class ZombieDamageCharacter : MonoBehaviour {

	public int health = 100;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(health + "hp left");
		if(health == 0){
			Destroy(this);
		}
	}

	void OnCollisionEnter(Collision col){
		Debug.Log("colliding");
		if(col.gameObject.tag == "Zombie"){
			health-=5;
		}
	}
}
