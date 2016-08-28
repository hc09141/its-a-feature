using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AntiVirusAI : MonoBehaviour {
	private Rigidbody2D rb;
	private SpriteRenderer render;
	// 1 for right, -1 for left
	public int direction = 1;
	public int speed = 3;

	private float ground_scale;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		render = GetComponent<SpriteRenderer> ();
		ground_scale = transform.localScale.x * 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (TouchingGroundLeft () != TouchingGroundRight()) {
			rb.velocity = Vector2.zero;
			if (TouchingGroundLeft ()) {
				rb.AddForce (new Vector2 (-5, 0));
			} else {
				rb.AddForce (new Vector2 (5, 0));
			}
		}
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
		if (collision.gameObject.tag == "Player" && speed == 8) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}

	void TriggeredByExploit(){
		render.color = Color.black;
		speed = 8;
	}

	bool TouchingGroundLeft(){
		RaycastHit2D x = Physics2D.Raycast(transform.position, LeftDirectionVector(), 3f, LayerMask.GetMask("Ground"));
		return x;
	}

	bool TouchingGroundRight(){
		RaycastHit2D x = Physics2D.Raycast(transform.position, RightDirectionVector(), 3f, LayerMask.GetMask("Ground"));
		return x;
	}
	Vector2 LeftDirectionVector(){
		Vector2 lower_left = rb.position + new Vector2 (-0.5f, -0.5f);
		return (lower_left - rb.position).normalized;


	}

	Vector2 RightDirectionVector(){
		Vector2 lower_right = rb.position + new Vector2 (0.5f, -0.5f);
		return (lower_right - rb.position);

	}
}
