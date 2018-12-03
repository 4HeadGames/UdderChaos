using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingController : MonoBehaviour {
    public Player player;
    public Camera camera;
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
		if (player.currentHunger == 0) {
            FieryHole();
        }
	}

    void FieryHole() {
        // fall through a fiery hole
        // Step 1 - lock player movement
        // Step 2 - Rotate player to be facing up
        // Step 3 - Pull player through ground
        // Step 4 - Reload scene

        player.canMove = false;
        player.GetComponent<Collider>().enabled = false;
        camera.GetComponent<MouseLook>().canRotate = false;
        var target = new Quaternion(-90.0f, player.transform.rotation.y, player.transform.rotation.z, player.transform.rotation.w);
        player.transform.Rotate(-90, player.transform.rotation.y, player.transform.rotation.z);
    }
}
