using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour {
    public DemonCow demonCow;
    private string[] script = {
        "Hello Pitiful Cow...",
        "Submit your tribute in my honor.",
        "I require blood sacrifices...",
        "Satisfy this thirst tonight... you've been warned",
    };
    private int line = 0;

	void Start () {
		
	}
	
	void Update () {
        if (!demonCow.Talking() && line < script.Length) {
            demonCow.SayText(script[line]);
            line += 1;
        }
	}
}
