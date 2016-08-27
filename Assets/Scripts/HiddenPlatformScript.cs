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
		float alpha = Mathf.Max (0f, render.color.a - 0.05f);
		Color c = render.color;
		c.a = alpha;
		render.color = c;
	}

	void TriggeredByExploit(){
		Color c = render.color;
		c.a = 1.0f;
		render.color = c;
	}
}
