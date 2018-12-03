﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingController : MonoBehaviour {
    public Player player;
    public AICow[] aiCows;
    public Grass grass;
    private float sacrificeMod;
	void Start () {
        var playerCollider = player.GetComponent<Collider>();
        
        sacrificeMod = player.lastSacrificeCount * 0.1f;

        var grassCount = 200;
        
        if (player.lastSacrificeCount < 5) {
            grassCount = Mathf.FloorToInt(200.0f * sacrificeMod);
        }

        for (int i = 0; i < grassCount; i++) {
            Quaternion rotation = Random.rotation;
            rotation.x = 0;
            rotation.z = 0;
            rotation.w = 0;
            var newGrass = Instantiate(grass,
                new Vector3(
                    Random.Range(-30, 30),
                    0,
                    Random.Range(-30, 30)),
                rotation);
            newGrass.name = "Grass";
        }

        for (int i = 0; i < 10; i++) {
            Quaternion rotation = Random.rotation;
            rotation.x = 0;
            rotation.z = 0;
            rotation.w = 0;
            var aiCow = aiCows[Random.Range(0, aiCows.Length)];
            var newAiCow = Instantiate(aiCow,
                new Vector3(
                    Random.Range(-30, 30),
                    2,
                    Random.Range(-30, 30)),
                rotation);
            newAiCow.name = "AI Cow";
        }
    }
	
	void Update () {
		
	}
}
