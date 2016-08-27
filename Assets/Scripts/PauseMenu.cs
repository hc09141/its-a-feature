using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	
	public void resume() {
		GetComponentInParent<MenuHider> ().HideMenu ();
	}

	public void restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void levelSelect() {
		
	}

	public void quit() {
		Application.Quit ();
	}
}
