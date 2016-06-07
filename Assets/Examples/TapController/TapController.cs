using UnityEngine;
using System.Collections;

public class TapController : MonoBehaviour {

	public Rigidbody rigidBody;
	public Vector2 jumpForce = new Vector2(0, 4f);

	void Update () {
		if (this.UserDidTapOnPhone ()) {
			this.AddForceToGameObject ();
		}
	}

	private void AddForceToGameObject() {
		this.rigidBody.velocity = Vector2.zero;
		this.rigidBody.AddForce(this.jumpForce, ForceMode.Impulse);
	}

	private bool UserDidTapOnPhone() {
		var didTap = false;
		foreach(Touch touch in Input.touches) {
			if(touch.phase == TouchPhase.Began) {
				didTap = true;
			}
		}
		return didTap;
	}
}