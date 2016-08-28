using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rb;	
	private ExploitMover exploit;
	private Animator anim;

	public static int MAX_SIZE = 200;
	public string nextScene;
	public int size = 0;
	private bool walkingRight = true;

	public int ACCELERATION = 1000;
	public int MAX_VELOCITY = 100;
	public int JUMP_FORCE = 1000;

	public AudioSource exitSound;

	private bool door_active = false;
	private bool completed;
	private bool transition;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		exploit = GetComponentInChildren<ExploitMover> ();
		DontDestroyOnLoad (exitSound);
	}
	
	// Update is called once per frame
	void Update () {		
		if (completed) {
			exploit.setComplete ();
		} else if (!Exploit ()) {
			move ();
			jump ();
		}
	}

	void jump () {
		if(Input.GetKeyDown("space") && rb.IsTouchingLayers(LayerMask.GetMask("Platform", "Ground"))){
			rb.AddForce (new Vector2(0,JUMP_FORCE));
			anim.SetBool ("isJumping", true);
		}
	}

	void move () {
		float horizontal = Input.GetAxis ("Horizontal");
		if (rb.velocity.x < MAX_VELOCITY) {
			rb.AddForce (new Vector2 (horizontal * ACCELERATION, 0));
		}
		Animation ();

	}

	bool Exploit() {
		if (Input.GetButton ("Fire1") && (size < MAX_SIZE || door_active)) {
			size++;
			rb.velocity = Vector2.zero;
			if (size > 500) {
				completed = true;
				exitSound.Play ();
			}
			return true;
		} else if(size > 0){
			size -= 2;
			rb.velocity = Vector2.zero;
			return true;
		}
		return false;
	}

	void Animation(){
		float horizontal = Input.GetAxis ("Horizontal");
		if (rb.IsTouchingLayers (LayerMask.GetMask ("Platform"))) {
			anim.SetBool ("isFalling", false);
			// 	anim.SetBool ("isJumping", false);
			if (horizontal > 0) {				
				anim.SetBool ("isWalkingLeft", false);
				anim.SetBool ("isIdle", false);
				anim.SetBool ("isWalkingRight", true);
				if (!walkingRight) {
					Flip ();
					walkingRight = true;
				}
			} else if (horizontal < 0) {
				anim.SetBool ("isWalkingRight", false);
				anim.SetBool ("isIdle", false);
				anim.SetBool ("isWalkingLeft", true);
				if (walkingRight) {
					Flip ();
					walkingRight = false;
				}
			} else {
				anim.SetBool ("isWalkingRight", false);
				anim.SetBool ("isWalkingLeft", false);
				anim.SetBool ("isIdle", true);
			}
		} else {
			anim.SetBool ("isWalkingRight", false);
			anim.SetBool ("isWalkingLeft", false);
			anim.SetBool ("isFalling", true);
		}
	}
		
	void Flip(){
		anim.transform.Rotate(0, 180, 0);
	}

	void DoorActive(){
		door_active = true;
	}

	void DoorInactive(){
		door_active = false;
	}

	public void Transition() {
		SceneManager.LoadScene (nextScene);
	}

	public int Size(){
		return size;
	}
}
