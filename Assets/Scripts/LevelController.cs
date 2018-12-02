using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    public Transform chicken;
    public int SacrificesNeeded;
    public Text SacrificeCounter;
    public RawImage ScreenFade;

    private Vector2 backLeftCorner = new Vector2(-15, 5);
    private Vector2 forwardRightCorner = new Vector2(15, 75);

    private int sacrificesMade = 0;
    private bool screenFading = false;

    void Start() {
        updateSacrificeText();

        for (int i = 0; i < 20; i++) {
            Quaternion rotation = Random.rotation;
            rotation.x = 0;
            rotation.z = 0;
            rotation.w = 0;
            var newChicken = Instantiate(chicken,
                new Vector3(
                    Random.Range(backLeftCorner.x, forwardRightCorner.x),
                    0,
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
            }
        }
    }

    public void Sacrifice(GameObject gameObject) {
        Destroy(gameObject);
        sacrificesMade += 1;
        if (sacrificesMade >= SacrificesNeeded) {
            sacrificesMade = SacrificesNeeded;
            screenFading = true;
        }

        updateSacrificeText();
    }

    private void updateSacrificeText() {
        SacrificeCounter.text = "Chicken Sacrifices: " + sacrificesMade + " / " + SacrificesNeeded;
    }
}
