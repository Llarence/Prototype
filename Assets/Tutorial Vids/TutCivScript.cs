using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutCivScript : MonoBehaviour {

    public bool playTutorial;

    // Use this for initialization
    void Start() {
        GameObject.Find("Quad").GetComponent<VideoPlayer>().Pause();
        GameObject.Find("Quad").GetComponent<Transform>().transform.Rotate (0, 90, 0);
    }
	
	// Update is called once per frame
	void Update () {
       if (playTutorial == true)
        {
            GameObject.Find("Quad").GetComponent<Transform>().transform.Rotate(0, -90, 0);
            GameObject.Find("Quad").GetComponent<VideoPlayer>().Play();
        }
       if (playTutorial == false)
        {
            GameObject.Find("Quad").GetComponent<Transform>().transform.Rotate(0, 90, 0);
            GameObject.Find("Quad").GetComponent<VideoPlayer>().Stop();
        }
	}
    public void SpawnTutorialVid()
    {
        playTutorial = true;
    }
    public void OffTutorialVids()
    {
        playTutorial = false;
    }
}
