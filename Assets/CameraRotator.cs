using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour {

	public float speed;
	public float cameraToCenter;

	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Manager").GetComponent<ManagerCivilization> ().start == 0) {
			transform.Rotate (0, speed * Time.deltaTime, 0);
		}
	}
}
