using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<RectTransform>().Translate (Input.GetAxisRaw("Horizontal") * Time.deltaTime * 750, 0, 0);
	}
}
