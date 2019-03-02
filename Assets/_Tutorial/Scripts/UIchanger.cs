using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIchanger : MonoBehaviour {

    public Text Instructions;
    public Text EndOfTutorial;
    public GameObject City;
    public float Stage;

    void Start()
    {
        Instructions.text = "Use the middle mouse button to move the camera";
      Stage = 1;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(2) && Stage == 1)
        {
            Instructions.text = "Use W A S and D to move around";
            Stage = 2;
        }
        if (Input.GetKey(KeyCode.W) && Stage == 2)
        {
            Stage = 3;
        }
        if (Stage == 3)
        {
            Instructions.text = "If you want to go faster then hold down the left control button while moving to go faster. Press the right arrow key to continue";
            Stage = 4;
        }
        if (Input.GetKey (KeyCode.RightArrow) && Stage == 4)
        {
            Instantiate(City);
            Instructions.text = "This is a city. If you longpress on it then you can go into it and edit it. Right now though we won't go into it. Press Space to continue.";
            Stage = 5;
        }
        if (Input.GetKey (KeyCode.RightArrow) && Stage == 5)
        {
            Instructions.text = "This is a warrior. You can move them and they also help you defend your cities. Click on it to select it.";
            Stage = 6;
        }
        if (Input.GetKey(KeyCode.RightArrow) && Stage == 6)
        {
            Instructions.text = "Finally, this is a settler. These characters make cities. Click the end button to go back to the main menu.";
            GameObject.Find("MainMenu").GetComponent<RectTransform>().Translate(225, 225, 0);
            EndOfTutorial.text = "End Tutorial";
        }
	}

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
