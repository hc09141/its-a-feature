using UnityEngine;
using System.Collections;

public class HiddenPlatformTrigger : MonoBehaviour {

	// Triggers when exploit touches this
	void TriggeredByExploit(){
		gameObject.transform.GetChild(0).GetComponent<BoxCollider2D> ().enabled = true;
	}
}
