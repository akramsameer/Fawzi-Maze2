using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour {
	public float counterClockwise = -2.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this. transform.Rotate(0, Time.deltaTime + counterClockwise, 0);
	}
	void OnCollisionEnter(Collision col){
		Debug.Log ("jkh");
		//Collision.contacts
	}
	void OnTriggerEnter(){
		
	}
	void OnCollisionStay(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
	}
}
