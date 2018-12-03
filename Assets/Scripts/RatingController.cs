using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RatingController : MonoBehaviour {
    public DemonCow demonCow;

    private RawImage screenFade;
    private string[] script;
    private int line = 0;
    private bool screenFading = false;

    void Start() {
        screenFade = GameObject.Find("Screen Fade").GetComponent<RawImage>();

        switch (Store.MissingSacrifices) {
            case 0:
                script = new string[] {
                    "I am satisfied with how many " + Store.AnimalName + "s I received last night.",
                    "Your food will be plentiful tomorrow.",
                    "See me tomorrow for your next task..."
                };
                break;
            case 1:
                script = new string[] {
                    "You did sacrifice many " + Store.AnimalName + "s last night, but not enough.",
                    "I will take some grass, but there should still be enough to go around.",
                    "I will need more sacrifices tomorrow..."
                };
                break;
            case 2:
                script = new string[] {
                    "Three " + Store.AnimalName + "s? I expect more from you.",
                    "The grass will be scarce tomorrow.",
                    "Tomorrow will be another chance, if you make it..."
                };
                break;
            case 3:
                script = new string[] {
                    "Wow... Two " + Store.AnimalName + "s. Pitiful.",
                    "An equivilent amount of grass for three " + Store.AnimalName + "s must go.",
                    "I expect better next time..."
                };
                break;
            case 4:
                script = new string[] {
                    "One " + Store.AnimalName + ". Just one. You mock me?",
                    "In exchange I shall lay waste to your grass.",
                    "You'll see me again if you make it through the day...",
                };
                break;
            case 5:
                script = new string[] {
                    "No " + Store.AnimalName + " sacrifices. None. Did you even try?",
                    "I will not accept such intolerance. There will be no grass for any of you heathens.",
                    "I will see you suffering as you wither and join me in hell...",
                };
                break;
        }
    }

    void Update() {
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
