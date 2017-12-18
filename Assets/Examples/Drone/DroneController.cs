using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {

	public bool IsEnabled = true;
	public float Upforce;
	private Rigidbody _droneBody;
	private float _movementForvardSpeed = 500.0f;
	private float _tiltAmountForward = 0;
	private float _tiltVelocityForward;
	private float _wantedYaxisRotation;
	private float _currentYaxisRotation;
	private float _rotationAmount = 2.5f;
	private float _rotationVelocity;
	private Vector3 _velocityToSmoothDampToZero;
	private float _sideMovementAmount = 300.0f;
	private float _tiltAmountSideways;
	private float _tiltAmountVelocity;

	void Awake() {
		this.InitializeDrone ();
	}

	void FixedUpdate() {
		if (this.IsEnabled) {
			this.MoveDrone ();
		} else {
			this.Levitate ();
			this.ApplyMoveForceToDrone ();
		}
	}

	public void HaltFriendlyDrone() {
		this._droneBody.velocity = Vector3.zero;
		this._droneBody.angularVelocity = Vector3.zero;
	}

	private void MoveDrone () {
		this.MoveUpOrDown ();
		this.MoveForward ();
		this.Rotate ();
		this.Swerve ();
		this.ClampSpeed ();
		this.ApplyMoveForceToDrone ();
	}

	void ClampSpeed ()
	{
		if (Mathf.Abs (Input.GetAxis ("Vertical")) > 0.2f && Mathf.Abs (Input.GetAxis ("Vertical")) > 0.2f) {
			this._droneBody.velocity = Vector3.ClampMagnitude(this._droneBody.velocity, Mathf.Lerp(this._droneBody.velocity.magnitude, 10.0f, Time.deltaTime * 2f));
		}
		if (Mathf.Abs (Input.GetAxis ("Vertical")) > 0.2f && Mathf.Abs (Input.GetAxis ("Vertical")) < 0.2f) {
			this._droneBody.velocity = Vector3.ClampMagnitude(this._droneBody.velocity, Mathf.Lerp(this._droneBody.velocity.magnitude, 10.0f, Time.deltaTime * 2f));
		}
		if (Mathf.Abs (Input.GetAxis ("Vertical")) < 0.2f && Mathf.Abs (Input.GetAxis ("Vertical")) > 0.2f) {
			this._droneBody.velocity = Vector3.ClampMagnitude(this._droneBody.velocity, Mathf.Lerp(this._droneBody.velocity.magnitude, 10.0f, Time.deltaTime * 2f));
		}
		if (Mathf.Abs (Input.GetAxis ("Vertical")) < 0.2f && Mathf.Abs (Input.GetAxis ("Vertical")) < 0.2f) {
			this._droneBody.velocity = Vector3.SmoothDamp (this._droneBody.velocity, Vector3.zero, ref this._velocityToSmoothDampToZero, 095f);
		}
	}

	void Swerve ()
	{
		if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.2f) {
			this._droneBody.AddRelativeForce (Vector3.right * Input.GetAxis ("Horizontal") * this._sideMovementAmount);
			this._tiltAmountSideways = Mathf.SmoothDamp (this._tiltAmountSideways, 4 * Input.GetAxis ("Horizontal"), ref this._tiltAmountVelocity, 0.1f);
		} else {
			this._tiltAmountSideways = Mathf.SmoothDamp (this._tiltAmountSideways, 0, ref this._tiltAmountVelocity, 0.1f);
		}
	}

	void Rotate ()
	{
		if(Input.GetKey(KeyCode.A)) {
			this._wantedYaxisRotation -= this._rotationAmount;
		}
		if(Input.GetKey(KeyCode.D)) {
			this._wantedYaxisRotation += this._rotationAmount;
		}
		this._currentYaxisRotation = Mathf.SmoothDamp (this._currentYaxisRotation, this._wantedYaxisRotation, ref this._rotationVelocity, 0.25f);
	}

	private void ApplyMoveForceToDrone() {
		this._droneBody.AddRelativeForce (Vector3.up * this.Upforce);
		this._droneBody.rotation = Quaternion.Euler (new Vector3(this._tiltAmountForward, this._currentYaxisRotation, this._tiltAmountSideways));
	}

	private void MoveUpOrDown () {
		if (Input.GetKey (KeyCode.Q)) {
			this.Upforce = 450;
		} else if (Input.GetKey (KeyCode.E)) {
			this.Upforce = -450;
		} else {
			this.Levitate ();
		}
	}

	private void Levitate() {
		if(!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E)) {
			this.Upforce = this._droneBody.mass * Mathf.Abs(Physics.gravity.y);
		}
	}

	private void MoveForward ()
	{
		if (Input.GetAxis ("Vertical") != 0) {
			this._droneBody.AddRelativeForce (Vector3.forward * Input.GetAxis ("Vertical") * this._movementForvardSpeed);
			this._tiltAmountForward = Mathf.SmoothDamp (this._tiltAmountForward, 4 * Input.GetAxis ("Vertical"), ref this._tiltVelocityForward, 1f);
		}
	}

	private void InitializeDrone() {
		this._droneBody = this.GetComponent<Rigidbody> ();
	}
}
