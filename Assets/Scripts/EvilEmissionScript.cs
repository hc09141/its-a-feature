using UnityEngine;
using System.Collections;

public class EvilEmissionScript : MonoBehaviour {
	public GameObject parent;
	private bool wasEvil = false;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<ParticleSystem> ().loop = false;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = parent.transform.position;
		bool isEvil = parent.GetComponent<SpriteRenderer> ().color.a < 0.1f;
		gameObject.GetComponent<ParticleSystem> ().loop = isEvil;
		if (isEvil && !wasEvil) {
			gameObject.GetComponent<ParticleSystem> ().Play ();
			wasEvil = true;
		} else if(!isEvil){
			wasEvil = false;
		}
	}
}
