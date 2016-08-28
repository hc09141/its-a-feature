using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AntiVirusAI : MonoBehaviour {
	private Rigidbody2D rb;
	public SpriteRenderer nice;
	public SpriteRenderer evil;
	// 1 for right, -1 for left
	public int direction = 1;
	public int speed = 3;

	private float ground_scale;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		ground_scale = transform.localScale.x * 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (TouchingGroundLeft () != TouchingGroundRight()) {
			rb.velocity = Vector2.zero;
			if (TouchingGroundLeft ()) {
				rb.AddForce (new Vector2 (-5, 0));
				FlipLeft ();
			} else {
				rb.AddForce (new Vector2 (5, 0));
				FlipRight ();
			}
		}
		if (Mathf.Abs(rb.velocity.x) < 0.01f) {
			direction *= -1;
			if (direction >= 0) {
				FlipRight ();
			} else {
				FlipLeft ();
			}
				
		}

		Color n_c = nice.color;
		Color e_c = evil.color;
		n_c.a = Mathf.Min(1.0f, n_c.a + 0.01f);
		e_c.a = 1.0f - n_c.a;
		nice.color = n_c;
		evil.color = e_c;
		if (n_c.a == 1.0f) {
			speed = 3;
		}

		rb.AddForce (new Vector2 (direction * speed, 0));
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Player" && speed > 3) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}

	void FlipLeft(){
		evil.flipX = true;
		nice.flipX = true;
	}

	void FlipRight(){
		evil.flipX = false;
		nice.flipX = false;
	}

	void TriggeredByExploit(){
		speed = 8;
		Color n_c = nice.color;
		Color e_c = evil.color;
		n_c.a = 0.0f;
		e_c.a = 1.0f;
		nice.color = n_c;
		evil.color = e_c;
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
