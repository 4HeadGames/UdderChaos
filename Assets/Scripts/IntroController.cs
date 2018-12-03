using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {
    public DemonCow demonCow;
    private string[] script = {
        "Hello pitiful Mortal...",
        "I am Moogramal.",
        "Submit your tribute in my honor.",
        "I require blood sacrifices...",
        "Satisfy this thirst tonight... you've been warned",
    };
    private int line = 0;

	void Start () {
		
	}
	
	void Update () {
        if (!demonCow.Talking()) {
            if (line < script.Length) {
                demonCow.SayText(script[line]);
                line += 1;
            } else {
                SceneManager.LoadScene(Store.NextLevel, LoadSceneMode.Single);
            }
        }
	}
}
