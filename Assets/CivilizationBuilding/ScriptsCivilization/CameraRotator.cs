using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour {

	public bool cameraToCenter;
	public bool rotate;
	public float rotateSpeed;
	public GameObject MainCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (rotate == true) {
			transform.Rotate (0, rotateSpeed * Time.deltaTime, 0);
		}

		if (cameraToCenter == false) {
			MainCamera.transform.TransformPoint (-350, 100, 0);
			rotate = true;
		}

		if (cameraToCenter == true) {
			rotate = false;
		}
	}

	public void Resume (){
		cameraToCenter = true;
		GameObject.Find ("Start").GetComponent<RectTransform> ().Translate (0, 100000, 0);
	}

	public void startDown (){
		GameObject.Find ("Start").GetComponent<RectTransform> ().Translate (0, -100000, 0);
	}
}
