using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioHandlerController : MonoBehaviour {

	public Text ButtonText;

	public void ToggleMusic() {
		if(AudioListener.volume == 0.0f) {
			AudioListener.volume = 1.0f;
			this.UpdateButtonText("off");
		}
		else if(AudioListener.volume == 1.0f) {
			AudioListener.volume = 0.0f;
			this.UpdateButtonText("on");
		}
	}

	private void UpdateButtonText(string value) {
		ButtonText.text = "Turn music " + value;
	}
}