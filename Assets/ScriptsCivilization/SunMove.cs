using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMove : MonoBehaviour {

	int RotateSpeed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.eulerAngles.x < -20){
			RotateSpeed = 100;
		}else{
			RotateSpeed = 5;
		}
		transform.Rotate(-Time.deltaTime * RotateSpeed, 0, 0);
	}
}
