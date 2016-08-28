using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	
	public void resume() {
		GetComponentInParent<MenuHider> ().HideMenu ();
	}

	public void restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		Time.timeScale = 1;
	}

	public void levelSelect() {
		SceneManager.LoadScene ("SplashScreen");
		Time.timeScale = 1;
	}

	public void quit() {
		Application.Quit ();
	}
}
