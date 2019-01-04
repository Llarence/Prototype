using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play () {
		SceneManager.LoadScene ("Civilization");
	}

	public void Settings () {
		GameObject.Find ("Play").GetComponent<RectTransform> ().Translate (0, 100000, 0);
		GameObject.Find ("Settings/Tutorial").GetComponent<RectTransform> ().Translate (0, 100000, 0);
		GameObject.Find ("Quit").GetComponent<RectTransform> ().Translate (0, 100000, 0);
		GameObject.Find ("Tutorial").GetComponent<RectTransform> ().Translate (0, -100000, 0);
		GameObject.Find ("Back").GetComponent<RectTransform> ().Translate (0, -100030, 0);
	}

	public void Quit () {
		Application.Quit();
	}

	public void Tutorial (){
		GameObject.Find ("Back").GetComponent<RectTransform> ().Translate (0, 100030, 0);
		GameObject.Find ("Tutorial").GetComponent<RectTransform> ().Translate (0, 100000, 0);
		SceneManager.LoadScene ("Tutorial");
	}

	public void Back () {
		GameObject.Find ("Play").GetComponent<RectTransform> ().Translate (0, -100000, 0);
		GameObject.Find ("Settings/Tutorial").GetComponent<RectTransform> ().Translate (0, -100000, 0);
		GameObject.Find ("Quit").GetComponent<RectTransform> ().Translate (0, -100000, 0);
		GameObject.Find ("Tutorial").GetComponent<RectTransform> ().Translate (0, 100000, 0);
		GameObject.Find ("Back").GetComponent<RectTransform> ().Translate (0, 100030, 0);
	}
}
