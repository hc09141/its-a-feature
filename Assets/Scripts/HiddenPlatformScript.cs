using UnityEngine;
using System.Collections;

public class HiddenPlatformScript : MonoBehaviour {

	private SpriteRenderer render;
	// Use this for initialization
	void Start () {
		render = GetComponent<SpriteRenderer> ();
		Color c = render.color;
		c.a = 0.0f;
		render.color = c;
	}

	// Update is called once per frame
	void Update () {
		float decrease = (1.0f - render.color.a) / 20.0f;
		float alpha = Mathf.Max (0f, render.color.a - decrease);
		Color c = render.color;
		c.a = alpha;
		render.color = c;
	}

	void TriggeredByExploit(){
		Color c = render.color;
		c.a = 0.99f;
		render.color = c;
	}

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			Color c = render.color;
			c.a = Mathf.Max (c.a, 0.2f);
			render.color = c;
		}
	}
}
