using UnityEngine;
using System.Collections;

public class TimedHiddenPlatformTrigger : MonoBehaviour {
	public int duration = 10;
	private float time_remaining;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (ColliderIsAcive ()) {
			if (time_remaining < 0) {
				SetColliderEnabled (false);
			} else {
				time_remaining -= Time.deltaTime;
			}
		}
	}


	void ApplySpriteOf(int number){

	}

	void TriggeredByExploit(){
		SetChildRendererVisible ();
		SetColliderEnabled (true);
		time_remaining = duration;
	}

	void SetColliderEnabled(bool activity){
		gameObject.transform.GetChild(0).GetComponent<BoxCollider2D> ().enabled = activity;
	}

	void SetChildRendererVisible(){
		SpriteRenderer renderer = gameObject.transform.GetChild (0).GetComponent<SpriteRenderer> ();
		Color c = renderer.color;
		c.a = 1.0f;
		renderer.color = c;
	}

	bool ColliderIsAcive(){
		return gameObject.transform.GetChild (0).GetComponent<BoxCollider2D> ().enabled;
	}
}
