using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	float pitch;
	float yaw;
	int control;
	public int controlMax;
	public int maxHeight;

	// Use this for initialization
	void Start () {
		control = 1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			control = controlMax;
		}
		if(Input.GetKeyUp(KeyCode.LeftControl)){
			control = 1;
		}
		transform.Translate ((Input.GetAxis("Horizontal") * Time.deltaTime * 10) * control, 0, (Input.GetAxis("Vertical") * Time.deltaTime * 10) * control);
		if(Input.GetKey("space")){
			transform.position = new Vector3 (transform.position.x, (transform.position.y + (Time.deltaTime * 10  * control)), transform.position.z);
		}
		if (transform.position.y > maxHeight) {
			transform.position = new Vector3 (transform.position.x, maxHeight, transform.position.z);
		}
		if(transform.position.y < 0){
			transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
		}
		if(Input.GetMouseButton (2)){
			Cursor.lockState = CursorLockMode.Locked;
			yaw -= Input.GetAxis ("Mouse Y") * 2;
			if(yaw > 90){
				yaw = 90;
			}
			if(yaw < -90){
				yaw = -90;
			}
			pitch += Input.GetAxis ("Mouse X") * 2;
			transform.eulerAngles = new Vector3 (yaw, pitch, 0);
		}
		if (Input.GetMouseButtonUp (2)) {
			Cursor.lockState = CursorLockMode.None;
		}
	}
}
