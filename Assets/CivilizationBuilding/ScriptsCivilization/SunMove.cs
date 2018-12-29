using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.eulerAngles.x < 180){
			transform.Rotate(-Time.deltaTime * 1, 0, 0);
		}
		if(transform.eulerAngles.x > 180){
			transform.Rotate(-Time.deltaTime * 25, 0, 0);
		}
	}
}