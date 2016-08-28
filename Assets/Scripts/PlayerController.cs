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

	public bool hasExploit = true;
	public int ACCELERATION = 1000;
	public int MAX_VELOCITY = 100;
	public int JUMP_FORCE = 1000;
	public float IDLE_THRESHOLD = 2.0f;

	public AudioSource exitSound;
	private AudioSource jumpSound;
	private AudioSource land;

	private bool door_active = false;
	private bool completed = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		exploit = GetComponentInChildren<ExploitMover> ();
		foreach(AudioSource sound in GetComponents<AudioSource>()){
			switch (sound.clip.name) {
			case "Jump":
				jumpSound = sound;
				break;
			case "Land_2":
				land = sound;
				break;
			}
		}
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
			jumpSound.Play ();
			resetGroundMovement ();
		} else if (anim.GetBool("isJumping") && rb.velocity.y < -1f){
			anim.SetBool ("isJumping", false);
			anim.SetBool ("isFalling", true);
			resetGroundMovement ();
		} else if (anim.GetBool("isFalling") && rb.IsTouchingLayers(LayerMask.GetMask("Platform", "Ground"))){
			land.Play ();
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isJumping", false);
			anim.SetBool ("isFalling", false);
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
		if (Input.GetButton ("Fire1") && (size < MAX_SIZE || door_active) && hasExploit) {
			size++;
			rb.velocity = Vector2.zero;
			if (size > 350) {
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
		if (rb.IsTouchingLayers (LayerMask.GetMask ("Platform", "Ground")) && !anim.GetBool("isFalling")) {
			float velocity = rb.velocity.x;
			anim.SetBool ("isFalling", false);
			// 	anim.SetBool ("isJumping", false);
			if (velocity > IDLE_THRESHOLD) {				
				anim.SetBool ("isWalkingLeft", false);
				anim.SetBool ("isIdle", false);
				anim.SetBool ("isWalkingRight", true);
				FlipRight ();
			} else if (velocity < -IDLE_THRESHOLD) {
				anim.SetBool ("isWalkingRight", false);
				anim.SetBool ("isIdle", false);
				anim.SetBool ("isWalkingLeft", true);
				FlipLeft ();
			} else {
				anim.SetBool ("isWalkingRight", false);
				anim.SetBool ("isWalkingLeft", false);
				anim.SetBool ("isIdle", true);
			}
		} else {
			resetGroundMovement ();
		}
	}

	void FlipRight(){
		GetComponent<SpriteRenderer> ().flipX = false;
	}

	void FlipLeft(){
		GetComponent<SpriteRenderer> ().flipX = true;
	}

	void DoorActive(){
		door_active = true;
	}

	void DoorInactive(){
		door_active = false;
	}

	void resetGroundMovement(){
		anim.SetBool ("isWalkingRight", false);
		anim.SetBool ("isWalkingLeft", false);
		anim.SetBool ("isIdle", false);
	}

	public void Transition() {
		SceneManager.LoadScene (nextScene);
	}

	bool OnGround() {
		return anim.GetBool ("isIdle") || anim.GetBool ("isWalkingLeft") || anim.GetBool ("isWalkingRight");
	}

	public int Size(){
		return size;
	}
}
