using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour {

	public float speed;
	public float cameraToCenter;

	// Update is called once per frame
	void Update () {
		if (cameraToCenter == 0) {
			transform.Rotate (0, speed * Time.deltaTime, 0);
		} else {
			if (cameraToCenter == 1) {
				transform.Rotate (0, 0, 0);
			}
		}

	}

	void Start (){
		cameraToCenter = 0;

	}
}
