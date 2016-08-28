using UnityEngine;
using System.Collections;

public class TeleportToTarget : MonoBehaviour {

	public GameObject target;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col){
		if(col.tag == "Player"){
			if(Input.GetKeyDown(KeyCode.E)){
				//print("Hello");
				col.transform.position = target.transform.position;
			}
		}
	}

}


