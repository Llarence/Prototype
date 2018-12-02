using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBobbing : MonoBehaviour {

	float offset;

	// Use this for initialization
	void Start () {
		offset = Random.Range (1f, 10f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, (2 * Mathf.PerlinNoise(Time.time + offset, 0)) - 2.5f, transform.position.z);
	}
}
