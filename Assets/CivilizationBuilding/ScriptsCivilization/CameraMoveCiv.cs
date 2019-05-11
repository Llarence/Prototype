using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveCiv : MonoBehaviour {

	float pitch = 90;
	float yaw = 30;
	int control;
	public int controlMax;
	public int maxHeight;
	public Material Robbie;
	Material Default;

	// Use this for initialization
	void Start () {
		Default = RenderSettings.skybox;
		control = 1;
		transform.eulerAngles = new Vector3 (55, 90, 0);
		if (PlayerPrefs.GetInt ("CameraMove") == 0) {
			transform.eulerAngles = new Vector3 (55, 90, 0);
		}
	}
		
	// Update is called once per frame
	void FixedUpdate () {
		if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().gameName != "") {
			if (Input.GetKeyDown ("j")) {
				RenderSettings.skybox = Robbie;
			}
			if (Input.GetKeyUp ("j")) {
				RenderSettings.skybox = Default;
			}
		}
		if (PlayerPrefs.GetInt ("CameraMove") == 1) {
			Camera.main.GetComponent<Camera> ().farClipPlane = 1000000;
			if (Input.GetKeyDown (KeyCode.LeftControl)) {
				control = controlMax;
			}
			if (Input.GetKeyUp (KeyCode.LeftControl)) {
				control = 1;
			}
			transform.Translate ((Input.GetAxis ("Horizontal") * Time.deltaTime * 40) * control, 0, (Input.GetAxis ("Vertical") * Time.deltaTime * 40) * control);
			if (Input.GetKey ("space")) {
				transform.position = new Vector3 (transform.position.x, (transform.position.y + (Time.deltaTime * 40 * control)), transform.position.z);
			}
			if (transform.position.y > maxHeight) {
				transform.position = new Vector3 (transform.position.x, maxHeight, transform.position.z);
			}
			if (Input.GetKey (KeyCode.LeftShift)) {
				transform.position = new Vector3 (transform.position.x, (transform.position.y - (Time.deltaTime * 40 * control)), transform.position.z);
			}
			if (transform.position.y < 0) {
				transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
			}
			if (Input.GetMouseButton (2)) {
				Cursor.lockState = CursorLockMode.Locked;
				yaw -= Input.GetAxis ("Mouse Y") * 2;
				if (yaw > 90) {
					yaw = 90;
				}
				if (yaw < -90) {
					yaw = -90;
				}
				pitch += Input.GetAxis ("Mouse X") * 2;
				transform.eulerAngles = new Vector3 (yaw, pitch, 0);
			}
		
			if (Input.GetMouseButtonUp (2)) {
				Cursor.lockState = CursorLockMode.None;
			}
		} else {
			transform.Translate ((Input.GetAxis ("Vertical") * Time.deltaTime * 40) * control, 0, (Input.GetAxis ("Horizontal") * Time.deltaTime * -40) * control, Space.World);
			if (Input.GetKeyDown (KeyCode.LeftControl)) {
				control = controlMax;
			}
			if (Input.GetKeyUp (KeyCode.LeftControl)) {
				control = 1;
			}
			Camera.main.GetComponent<Camera> ().fieldOfView -= Input.GetAxisRaw ("Mouse ScrollWheel") * 15;
			Camera.main.GetComponent<Camera> ().fieldOfView = Mathf.Clamp (Camera.main.GetComponent<Camera> ().fieldOfView, 10, 60);
			Camera.main.GetComponent<Camera> ().farClipPlane = 220;
		}
	}
}
