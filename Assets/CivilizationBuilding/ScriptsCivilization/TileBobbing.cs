using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBobbing : MonoBehaviour {

	float offset;
	float offset2;
	float offset3;
	GameObject Manager;

	// Use this for initialization
	void Start () {
		Manager = GameObject.Find ("Manager");
		offset = transform.position.x / (Manager.GetComponent<ManagerCivilization>().xAmount * 2 + 1);
		offset2 = transform.position.z / (Manager.GetComponent<ManagerCivilization>().zAmount * 2 + 1);
		offset3 = Manager.GetComponent<ManagerCivilization> ().offset;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, (12 * Mathf.PerlinNoise(Time.time/30 + offset3, 0f) * Mathf.PerlinNoise(Time.time/3 + offset, Time.time/3 + offset2)) - 7, transform.position.z);
	}
}