using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashSheepMover : MonoBehaviour {

	//Moves the sheep back and forth in the Splashscreen

	private Vector3 pos1 = new Vector3(-10,-4.2f,0);
	private Vector3 pos2 = new Vector3(10,-4.2f,0);
	public float speed = 1.0f;

	void Update() {
		transform.position = Vector3.Lerp (pos1, pos2, Mathf.PingPong(Time.time*speed, 1.0f));

		if(Input.GetKeyDown(KeyCode.Return)){
			SceneManager.LoadScene("MountainVillage");
		}
	}

}
