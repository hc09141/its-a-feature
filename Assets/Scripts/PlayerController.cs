﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rb;	
	private ExploitMover exploit;
	private Animator anim;

	public static int MAX_SIZE = 200;
	public string nextScene;
	private int size = 0;
	private bool walkingRight = true;

	public int ACCELERATION = 1000;
	public int MAX_VELOCITY = 100;
	public int JUMP_FORCE = 1000;
	public float IDLE_THRESHOLD = 2.0f;

	public AudioSource exitSound;

	private bool door_active = false;
	private bool completed = false;

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
			print("ima jump"); 	
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
		if (rb.IsTouchingLayers (LayerMask.GetMask ("Platform", "Ground"))) {
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
			anim.SetBool ("isWalkingRight", false);
			anim.SetBool ("isWalkingLeft", false);
			anim.SetBool ("isFalling", true);
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

	public void Transition() {
		SceneManager.LoadScene (nextScene);
	}

	public int Size(){
		return size;
	}
}
