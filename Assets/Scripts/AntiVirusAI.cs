using UnityEngine;
using System.Collections;

public class AntiVirusAI : MonoBehaviour {
	private Rigidbody2D rb;
	private SpriteRenderer render;
	// 1 for right, -1 for left
	public int direction = 1;
	public int speed = 3;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		render = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(rb.velocity.x) < 0.01f) {
			direction *= -1;
		}

		Color c = render.color;
		c.r = Mathf.Min (1f, c.r + 0.01f);
		c.b = Mathf.Min (1f, c.b + 0.01f);
		c.g = Mathf.Min (1f, c.g + 0.01f);
		render.color = c;
		if (c.r == 1.0f) {
			speed = 3;
		}

		rb.AddForce (new Vector2 (direction * speed, 0));
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Player") {
			print ("U DED");
		}
	}

	void TriggeredByExploit(){
		render.color = Color.black;
		speed = 8;
	}
}
