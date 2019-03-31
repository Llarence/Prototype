using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveCiv : MonoBehaviour {

	float pitch = 90;
	float yaw = 30;
	int control;
	public int controlMax;
	public int maxHeight;

	// Use this for initialization
	void Start () {
		control = 1;
		if (PlayerPrefs.GetInt ("CameraMove") == 0) {
			transform.eulerAngles = new Vector3 (35, 90, 0);
		}
	}
		
	// Update is called once per frame
	void FixedUpdate () {
		if(GameObject.Find("Manager").GetComponent<ManagerCivilization>().gameName != ""){
		if (Input.GetKeyUp ("j") && GetComponent<Camera> ().clearFlags == CameraClearFlags.Skybox) {
			GetComponent<Camera> ().clearFlags = CameraClearFlags.SolidColor;
			GetComponent<Camera> ().backgroundColor = Color.black;
		} else {
			if (Input.GetKeyUp ("j") && GetComponent<Camera> ().clearFlags == CameraClearFlags.SolidColor) {
				GetComponent<Camera> ().clearFlags = CameraClearFlags.Skybox;
			}
		}
		if (GetComponent<Camera> ().clearFlags == CameraClearFlags.SolidColor && GetComponent<Camera> ().backgroundColor == Color.white) {
			GetComponent<Camera> ().backgroundColor = Color.black;
			} else {
				if (GetComponent<Camera> ().clearFlags == CameraClearFlags.SolidColor && GetComponent<Camera> ().backgroundColor == Color.black) {
					GetComponent<Camera> ().backgroundColor = Color.white;
				}
			}
		}
		if (PlayerPrefs.GetInt ("CameraMove") == 1) {
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
		}
	}
}
