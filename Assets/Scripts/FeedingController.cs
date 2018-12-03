using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingController : MonoBehaviour {
    public Player player;
    public Camera camera;
    public DemonCowSacrifice DemonCow;
    public AICow[] aiCows;
    public Grass grass;

    private float sacrificeMod;
    private Color initialSkyTint = new Color(128 / 255f, 128 / 255f, 128 / 255f);
    private Color initialGroundColor = new Color(17 / 255f, 36 / 255f, 9 / 255f);
    private float initialSkyExposure = 1.3f;

    void Start() {
        RenderSettings.skybox.SetColor("_SkyTint", initialSkyTint);
        RenderSettings.skybox.SetColor("_GroundColor", initialGroundColor);
        RenderSettings.skybox.SetFloat("_Exposure", initialSkyExposure);

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

    void Update() {
        FieryDeath();
        if (player.currentHunger == 0) {
        }
    }

    private bool startedFiery = false;
    private float stareTime;
    private float stareTotalTime = 4;
    private DemonCowSacrifice demonCow;
    private Material skyBoxInitial;
    private Material skyBoxTarget;

    void FieryDeath() {
        var mouseLook = camera.GetComponent<MouseLook>();

        if (!startedFiery) {
            GameObject.Find("HungerBar").GetComponent<RectTransform>().localScale = Vector3.zero; ;
            GameObject.Find("Hunger Label").GetComponent<RectTransform>().localScale = Vector3.zero;

            foreach (var cow in GameObject.FindGameObjectsWithTag("AI Cow")) {
                cow.GetComponent<AudioSource>().volume = 0;
            }
            var rigidBody = player.GetComponent<Rigidbody>();
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;

            skyBoxInitial = RenderSettings.skybox;

            startedFiery = true;
            var demonCowPosition = player.transform.position + 12 * player.transform.forward;
            demonCowPosition.y = 2;
            demonCow = Instantiate(DemonCow,
                demonCowPosition,
                Quaternion.identity);
            demonCow.transform.LookAt(camera.transform);
            demonCow.SacrificeTarget = player.gameObject;
            demonCow.sacrificingPlayer = true;

            player.canMove = false;
            mouseLook.canRotate = false;
            mouseLook.xAngleRange = new Vector2(0, 0);

            stareTime = stareTotalTime;
        }

        if (demonCow != null) {
            mouseLook.yAngleRange = new Vector2(5, 5);
        }

        if (stareTime > 0) {
            stareTime -= Time.deltaTime;
            float lerpTime = (stareTotalTime - stareTime) / stareTotalTime;
            RenderSettings.skybox.SetColor("_SkyTint", 
                Color.Lerp(initialSkyTint, Color.black, lerpTime));
            RenderSettings.skybox.SetColor("_GroundColor",
                Color.Lerp(initialGroundColor, Color.black, lerpTime));
            RenderSettings.skybox.SetFloat("_Exposure",
                Mathf.Lerp(initialSkyExposure, 0, lerpTime));
        } else {
            foreach (var cow in GameObject.FindGameObjectsWithTag("AI Cow")) {
                Destroy(cow);
            }
            var ground = GameObject.Find("Ground");
            Destroy(ground);

            var rigidBody = player.GetComponent<Rigidbody>();
            rigidBody.constraints = RigidbodyConstraints.None;
        }
    }
}
