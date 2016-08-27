using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D c){
		print ("GET ON ME DOOR");
		if (c.tag == "Player") {
			c.SendMessage ("DoorActive");
		}
	}

	void OnTriggerExit2D(Collider2D c){
		if (c.tag == "Player") {
			c.SendMessage ("DoorInactive");
		}
	}
}
