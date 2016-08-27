using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuHider : MonoBehaviour {

	public GameObject panel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("escape")) {
			print ("Show menu");
			panel.SetActive (true);
			Pause ();
		}
	}

	void Pause() {
		Object[] objects = FindObjectsOfType (typeof(GameObject));
		Time.timeScale = 0.0f;
		// http://answers.unity3d.com/questions/7544/how-do-i-pause-my-game.html
	}

	public void HideMenu() {
		panel.SetActive (false);
		Time.timeScale = 1.0f;
	}
}
