using UnityEngine;
using System.Collections;

public class RigidFollowScript : MonoBehaviour {
	public Transform target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = target.position;
	}
}
