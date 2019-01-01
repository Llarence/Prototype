using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCivilization : MonoBehaviour {

	public GameObject Mountain;
	public GameObject Grass;
	public GameObject Water;
	public GameObject City;
	public GameObject Warrior;
	public GameObject Settler;
	int x;
	int z;
	public float offset;
	public int xAmount;
	public int zAmount;
	bool NoPlayerCity = true;
	public int Turn;
	GameObject[] Units;
	public GameObject Text;
	public GameObject Text2;

	public void GenerateMap () {
		Destroy (Text);
		Destroy (Text2);
		offset = Random.Range (-100f, 100f);
		x = -xAmount * 10;
		z = -zAmount * 10;
		while (x < (xAmount * 10) + 10) {
			while (z < (zAmount * 10) + 10) {
				if (Mathf.PerlinNoise((offset + ((x + (float)(-xAmount * 10))/(5f * xAmount))), (offset + ((z + (float)(-zAmount * 10))/(5f * zAmount)))) < 0.5f) {
					Instantiate (Water, new Vector3 (x, -1f, z), Quaternion.identity);
				} else {
					if(Mathf.PerlinNoise((offset + ((x + (float)(-xAmount * 10))/(5f * xAmount))), (offset + ((z + (float)(-zAmount * 10))/(5f * zAmount)))) < 0.825f){
						Instantiate (Grass, new Vector3 (x, -4.5f, z), Quaternion.identity);
					} else {
						Instantiate (Mountain, new Vector3 (x, -0.5f, z), Quaternion.identity);
					}

				}
				z += 10;
			}
			z = -xAmount * 10;
			x += 10;
		}
		while (NoPlayerCity == true) {
			x = Random.Range (-xAmount, xAmount + 1) * 10;
			z = Random.Range (-zAmount, zAmount + 1) * 10;
			if(Mathf.PerlinNoise((offset + ((x + (float)(-xAmount * 10))/(5f * xAmount))), (offset + ((z + (float)(-zAmount * 10))/(5f * zAmount)))) < 0.825f && Mathf.PerlinNoise((offset + ((x + (float)(-xAmount * 10))/(5f * xAmount))), (offset + ((z + (float)(-zAmount * 10))/(5f * zAmount)))) > 0.5f){
				Instantiate (City, new Vector3(x, 2f, z), Quaternion.identity);
				Instantiate (Settler, new Vector3(x, 5f, z), Quaternion.identity);
				Instantiate (Warrior, new Vector3(x, 5f, z + 10), Quaternion.identity);
				Instantiate (Warrior, new Vector3(x, 5f, z - 10), Quaternion.identity);
				Instantiate (Warrior, new Vector3(x + 10, 5f, z), Quaternion.identity);
				Instantiate (Warrior, new Vector3(x - 10, 5f, z), Quaternion.identity);
				NoPlayerCity = false;
			}
		}
	}

	void Update(){
		Units = GameObject.FindGameObjectsWithTag("Unit");
		if(Input.GetMouseButton(0) || Input.GetMouseButton(1)){
			Camera.main.GetComponent<AudioSource>().Play(0);
		}
	}

	public void NextTurn (){
		Turn++;
	}
		
	public void CallSettle (){
		foreach (GameObject Unit in Units) {
			Unit.GetComponent<Unit> ().Settle ();
		}
	}
}