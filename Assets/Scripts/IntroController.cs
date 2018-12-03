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
    private RawImage screenFade;
    private bool screenFading = false;

    void Start () {
        screenFade = GameObject.Find("Screen Fade").GetComponent<RawImage>();
    }
	
	void Update () {
        if (screenFading) {
            float a = Mathf.Min(1f, screenFade.color.a + 0.005f);
            screenFade.color = new Color(0, 0, 0, a);
            if (a >= 1f) {
                SceneManager.LoadScene(Store.NextLevel, LoadSceneMode.Single);
            }
            return;
        }

        if (!demonCow.Talking()) {
            if (line < script.Length) {
                demonCow.SayText(script[line]);
                line += 1;
            } else {
                screenFading = true;
            }
        }
	}
}
