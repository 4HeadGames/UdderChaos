using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    public GameObject Animal;
    public DemonCowSacrifice DemonCow;
    public string AnimalName;
    public string NextLevel;
    public int SacrificesNeeded;
    public Text SacrificeCounter;
    public Text CaughtText;
    public RawImage ScreenFade;

    private Vector2 backLeftCorner = new Vector2(-15, 5);
    private Vector2 forwardRightCorner = new Vector2(15, 75);

    private int sacrificesMade = 0;
    private bool screenFading = false;
    private bool caught = false;

    void Start() {
        Store.AnimalName = AnimalName;
        Store.MissingSacrifices = SacrificesNeeded;

        updateSacrificeText();

        for (int i = 0; i < 20; i++) {
            Quaternion rotation = Random.rotation;
            rotation.x = 0;
            rotation.z = 0;
            rotation.w = 0;
            var newChicken = Instantiate(Animal,
                new Vector3(
                    Random.Range(backLeftCorner.x, forwardRightCorner.x),
                    2,
                    Random.Range(backLeftCorner.y, forwardRightCorner.y)),
                rotation);
            newChicken.name = "Chicken";
        }
    }

    void Update() {
        if (screenFading) {
            float a = Mathf.Min(1f, ScreenFade.color.a + 0.005f);
            ScreenFade.color = new Color(0, 0, 0, a);
            if (a >= 1f) {
                Store.NextLevel = NextLevel;
                SceneManager.LoadScene("Demon Rating", LoadSceneMode.Single);
            }
        }
    }

    public void Sacrifice(GameObject gameObject) {
        /*
        var demonCow = Instantiate(DemonCow,
            gameObject
            new Vector3(
                ,
                2,
                Random.Range(backLeftCorner.y, forwardRightCorner.y)),
            rotation);
            */
        sacrificesMade += 1;
        sacrificesMade = Mathf.Clamp(sacrificesMade, 0, SacrificesNeeded);

        Store.MissingSacrifices = SacrificesNeeded - sacrificesMade;
        if (sacrificesMade >= SacrificesNeeded) {
            screenFading = true;
        }

        updateSacrificeText();
    }

    public void PlayerCaught() {
        screenFading = true;
        setCaughtText();
    }

    private void setCaughtText() {
        CaughtText.text = "You've been caught!";
    }

    private void updateSacrificeText() {
        SacrificeCounter.text = AnimalName + " Sacrifices: " + sacrificesMade + " / " + SacrificesNeeded;
    }
}
