using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoStorage : MonoBehaviour {

	public string cityName;
	public string inGameName;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
