using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBobbing : MonoBehaviour {

	float offset;
	float offset2;
	float offset3;
	GameObject manager;
	public float strength;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Manager");
		offset = transform.position.x / (manager.GetComponent<ManagerCivilization>().xAmount * 2 + 1);
		offset2 = transform.position.z / (manager.GetComponent<ManagerCivilization>().zAmount * 2 + 1);
		offset3 = manager.GetComponent<ManagerCivilization> ().offset;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, (strength * 8 * Mathf.PerlinNoise(Time.time/30 + offset3, 0f) * Mathf.PerlinNoise(Time.time/3 + offset, Time.time/3 + offset2)) - 6.5f, transform.position.z);
	}
}