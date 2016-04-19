using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {
	public void ReloadScene() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
