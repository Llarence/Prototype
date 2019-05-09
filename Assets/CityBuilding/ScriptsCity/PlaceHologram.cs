using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHologram : MonoBehaviour {

	public GameObject SpawnObject;
	RaycastHit hit;
	public LayerMask HitLayers;
	int Colliding;
	public float SpawnHeight;
	public Color Red;
	public Color Green;
	public int costToBuy;

	// Use this for initialization
	void Start () {
		if(Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, HitLayers)){
			transform.position = new Vector3(hit.point.x, SpawnHeight, hit.point.z);
		}
	}

	// Update is called once per frame
	void Update () {
		if(Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, HitLayers)){
			transform.position = new Vector3(hit.point.x, SpawnHeight, hit.point.z);
		}
		if (Input.GetMouseButtonDown (1) && Colliding == 0) {
			if(GameObject.Find ("Manager").GetComponent<ManagerCity> ().Gold >= costToBuy){
				Instantiate (SpawnObject, transform.position, Quaternion.identity);
				GameObject.Find ("MainCamera").GetComponent<Place> ().SpawningObject = false;
				GameObject.Find ("Manager").GetComponent<ManagerCity> ().Gold -= costToBuy;
				if(Input.GetKey(KeyCode.LeftShift) == false){
					Destroy (gameObject);
				}
			}
		}
		if(Colliding == 0){
			foreach(Material material in GetComponent<MeshRenderer>().materials){
				material.color = Green;
			}
		}
		if(Colliding > 0){
			foreach(Material material in GetComponent<MeshRenderer>().materials){
				material.color = Red;
			}
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