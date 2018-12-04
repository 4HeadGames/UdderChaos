using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farmer : MonoBehaviour {
	public Text text;
	private float talkingDuration = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (talkingDuration > 0) {
            talkingDuration -= Time.deltaTime;
        }
	}

	public void SayText(string dialogue) {
        var words = dialogue.Split(' ').Length;
        talkingDuration = words * 0.1f;

        text.text = dialogue;
    }

    public bool Talking() {
        return talkingDuration > 0;
    }
}
