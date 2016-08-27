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
			// pause game
		}
	}

	public void HideMenu() {
		panel.SetActive (false);
		// reactivate game
	}
}
