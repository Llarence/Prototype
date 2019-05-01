using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("CameraMove") == 0) {
			GameObject.Find ("Camera Move Style").GetComponentInChildren<Text> ().text = "Camera Move Style: Iso, Better Performace";
		} else {
			GameObject.Find ("Camera Move Style").GetComponentInChildren<Text> ().text = "Camera Move Style: Free, Worse Performace";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play () {
		SceneManager.LoadScene ("Civilization");
	}

    public void Settings() {
        GameObject.Find("Play").GetComponent<RectTransform>().Translate(0, 100000, 0);
        GameObject.Find("Settings/Tutorial").GetComponent<RectTransform>().Translate(0, 100000, 0);
        GameObject.Find("Quit").GetComponent<RectTransform>().Translate(0, 100000, 0);
        GameObject.Find("Tutorial").GetComponent<RectTransform>().Translate(0, -1000000, 0);
		GameObject.Find("Camera Move Style").GetComponent<RectTransform>().Translate(0, -1000000, 0);
        GameObject.Find("Back").GetComponent<RectTransform>().Translate(0, 1000000, 0);
		GameObject.Find("Story").GetComponent<RectTransform>().Translate(0, -1000000, 0);
    }

	public void Quit () {
		PlayerPrefs.Save ();
		Application.Quit();
	}

    public void Back()
    {
        GameObject.Find("Play").GetComponent<RectTransform>().Translate(0, -100000, 0);
        GameObject.Find("Settings/Tutorial").GetComponent<RectTransform>().Translate(0, -100000, 0);
        GameObject.Find("Quit").GetComponent<RectTransform>().Translate(0, -100000, 0);
        GameObject.Find("Tutorial").GetComponent<RectTransform>().Translate(0, 1000000, 0);
		GameObject.Find("Camera Move Style").GetComponent<RectTransform>().Translate(0, 1000000, 0);
        GameObject.Find("Back").GetComponent<RectTransform>().Translate(0, -1000000, 0);
		GameObject.Find("Story").GetComponent<RectTransform>().Translate(0, 1000000, 0);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

	public void SwitchCamMode (){
		if (PlayerPrefs.GetInt ("CameraMove") == 1) {
			PlayerPrefs.SetInt ("CameraMove", 0);
			GameObject.Find ("Camera Move Style").GetComponentInChildren<Text> ().text = "Camera Move Style: Iso, Better Performace";
		} else {
			PlayerPrefs.SetInt ("CameraMove", 1);
			GameObject.Find ("Camera Move Style").GetComponentInChildren<Text> ().text = "Camera Move Style: Free, Worse Performace";
		}
	}
}