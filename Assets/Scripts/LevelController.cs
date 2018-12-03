﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    public Player player;
    public GameObject[] Animals;
    public DemonCowSacrifice DemonCow;
    public string AnimalName;
    public string NextLevel;

    private GameObject animalSpawnRegion;
    private RawImage screenFade;
    private Text sacrificesCounter;
    private Text caughtText;

    private int sacrificesMade = 0;
    private bool screenFading = false;

    private DemonCowSacrifice spawnedDemonCow;

    void Start() {
        animalSpawnRegion = GameObject.Find("Animal Spawn Region");
        screenFade = GameObject.Find("Screen Fade").GetComponent<RawImage>();
        sacrificesCounter = GameObject.Find("Sacrifices Counter").GetComponent<Text>();
        caughtText = GameObject.Find("Caught Text").GetComponent<Text>();

        Store.AnimalName = AnimalName;
        Store.MissingSacrifices = Store.SacrificesNeeded;

        updateSacrificeText();

        for (int i = 0; i < 20; i++) {
            Quaternion rotation = Random.rotation;
            rotation.x = 0;
            rotation.z = 0;
            rotation.w = 0;
            var bounds = animalSpawnRegion.GetComponent<Collider>().bounds;
            var newChicken = Instantiate(
                Animals[Random.Range(0, Animals.Length)],
                new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    1f,
                    Random.Range(bounds.min.y, bounds.max.y)),
                rotation);
            newChicken.name = "Chicken";
        }
    }

    void Update() {
        if (screenFading) {
            float a = Mathf.Min(1f, screenFade.color.a + 0.005f);
            screenFade.color = new Color(0, 0, 0, a);
            if (a >= 1f) {
                Store.NextLevel = NextLevel;
                SceneManager.LoadScene("Demon Rating", LoadSceneMode.Single);
            }
        }
    }

    public void Sacrifice(GameObject gameObject) {
        if (spawnedDemonCow != null) {
            // Can only sacrifice one at a time.
            return;
        }
        var player = GameObject.Find("Player");
        var demonCowPosition = player.transform.position + 8 * player.transform.forward;
        demonCowPosition.y = 2;
        spawnedDemonCow = Instantiate(DemonCow,
            demonCowPosition,
            Quaternion.identity);
        spawnedDemonCow.transform.LookAt(gameObject.transform);
        spawnedDemonCow.SacrificeTarget = gameObject;
        sacrificesMade += 1;
        sacrificesMade = Mathf.Clamp(sacrificesMade, 0, Store.SacrificesNeeded);

        Store.MissingSacrifices = Store.SacrificesNeeded - sacrificesMade;
        if (sacrificesMade >= Store.SacrificesNeeded) {
            screenFading = true;
        }

        updateSacrificeText();
    }

    public void PlayerCaught() {
        screenFading = true;
        setCaughtText();
    }

    private void setCaughtText() {
        caughtText.text = "You've been caught!";
    }

    private void updateSacrificeText() {
        sacrificesCounter.text = AnimalName + " Sacrifices: " + sacrificesMade + " / " + Store.SacrificesNeeded;
    }
}
