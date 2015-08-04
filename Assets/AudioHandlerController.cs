using UnityEngine;
using System.Collections;

public class AudioHandlerController : MonoBehaviour {

	public void ToggleMusic() {
		if(AudioListener.volume == 0.0f) {
			AudioListener.volume = 1.0f;
		}
		else if(AudioListener.volume == 1.0f) {
			AudioListener.volume = 0.0f;
		}
	}
}
