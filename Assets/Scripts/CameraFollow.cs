using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public float xMax = 0.2f; 
	public float yMax = 0.5f;

	// Update is called once per frame
	void Update () 
	{

		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
			xMax = 0.8f;
		} 
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
			xMax = 0.2f;
		} 


		if (target)
		{
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(xMax, yMax, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

	}


}