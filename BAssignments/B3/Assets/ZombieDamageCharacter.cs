using UnityEngine;
using System.Collections;

public class ZombieDamageCharacter : MonoBehaviour {

	public double health = 150;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(health + "hp left");
		if(health < 0){
			gameObject.SetActive(false);
		}
	}

	void OnTriggerStay(Collider col){
		Debug.Log("colliding");
		if(col.gameObject.tag == "Zombie"){
			health-=1;
		}
	}
}
