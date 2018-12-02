using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHologram : MonoBehaviour {

	public GameObject SpawnObject;
	RaycastHit hit;
	public LayerMask HitLayers;
	int Colliding;
	public float SpawnHeight;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, HitLayers)){
			transform.position = new Vector3(hit.point.x, SpawnHeight, hit.point.z);
		}
		if (Input.GetMouseButtonDown (1) && Colliding == 0) {
			Instantiate (SpawnObject, transform.position, Quaternion.identity);
			GameObject.Find ("MainCamera").GetComponent<Place> ().SpawningObject = false;
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter (Collider Col){
		if(Col.gameObject.layer != 2){
			Colliding++;
		}
	}

	void OnTriggerExit (Collider Col){
		if (Col.gameObject.layer != 2) {
			Colliding--;
		}
	}
}
