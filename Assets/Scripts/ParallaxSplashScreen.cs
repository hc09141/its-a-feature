using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ParallaxSplashScreen : MonoBehaviour {

	public Transform[] backgrounds;
	private float[] parallaxScales;
	public float smoothing;
	public GameObject Doggy;

	private Transform cam;
	private Vector3 previousCamPos;

	// Use this for initialization
	void Start () {

		cam = Doggy.transform;

		previousCamPos = cam.position;

		parallaxScales = new float[backgrounds.Length];

		for(int i = 0; i < backgrounds.Length; i++){

			parallaxScales[i] = backgrounds[i].position.z * -1;

		}

	}

	// Update is called once per frame
	void LateUpdate () {

		if(Input.GetKeyDown(KeyCode.Return)){
			SceneManager.LoadScene("MountainVillage");
		}

		for(int i = 0; i < backgrounds.Length; i++){

			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime); 

		}

		previousCamPos = cam.position;

	}


}
