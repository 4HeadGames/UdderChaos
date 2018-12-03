using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FeedingController : MonoBehaviour {
    public Player player;
    public Camera camera;
    public DemonCowSacrifice DemonCow;
    public AICow[] aiCows;
    public Grass grass;

    private bool screenFading = false;
    private RawImage screenFade;
    private float sacrificeMod;
    private Color initialSkyTint = new Color(128 / 255f, 128 / 255f, 128 / 255f);
    private Color initialGroundColor = new Color(17 / 255f, 36 / 255f, 9 / 255f);
    private float initialSkyExposure = 1.3f;

    private float feedingTime = 10;
    private float preIntroFeedingTime = 15;

    void Start() {
        screenFade = GameObject.Find("Screen Fade").GetComponent<RawImage>();

        RenderSettings.skybox.SetColor("_SkyTint", initialSkyTint);
        RenderSettings.skybox.SetColor("_GroundColor", initialGroundColor);
        RenderSettings.skybox.SetFloat("_Exposure", initialSkyExposure);

        var playerCollider = player.GetComponent<Collider>();

        sacrificeMod = (Store.SacrificesNeeded - Store.MissingSacrifices) * 0.1f;

        var grassCount = 30;

        if (Store.MissingSacrifices > 0) {
            grassCount = Mathf.FloorToInt(200 * sacrificeMod);
        }

        for (int i = 0; i < grassCount; i++) {
            Quaternion rotation = Random.rotation;
            rotation.x = 0;
            rotation.z = 0;
            rotation.w = 0;
            var newGrass = Instantiate(grass,
                new Vector3(
                    Random.Range(-25, 25),
                    0,
                    Random.Range(-25, 25)),
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
                    Random.Range(-25, 25),
                    0.5f,
                    Random.Range(-25, 25)),
                rotation);
            newAiCow.name = "AI Cow";
        }

        if (Store.PreIntro) {
            GameObject.Find("HungerBar").GetComponent<RectTransform>().localScale = Vector3.zero;
            GameObject.Find("Hunger Label").GetComponent<RectTransform>().localScale = Vector3.zero;
        } else {
            GameObject.Find("Intro Text").GetComponent<RectTransform>().localScale = Vector3.zero;
        }
    }

    private string nextLevel;
    void Update() {
        if (screenFading || stareTime > 0) {
            if (screenFading) {
                float a = Mathf.Min(1f, screenFade.color.a + 0.005f);
                screenFade.color = new Color(0, 0, 0, a);
                if (a >= 1f) {
                    SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
                }
            } else if (stareTime > 0) {
                stareTime -= Time.deltaTime;
                if (stareTime <= 0) {
                    screenFading = true;
                }
            }
            return;
        }

        if (Store.PreIntro) {
            preIntroFeedingTime -= Time.deltaTime;
            if (preIntroFeedingTime <= 0) {
                GameObject.Find("Intro Text").GetComponent<RectTransform>().localScale = Vector3.zero;
                PopInMoogramal();
                Store.PreIntro = false;
                nextLevel = "Intro";
            }
        } else {
            feedingTime -= Time.deltaTime;
            if (feedingTime <= 0) {
                if (player.currentHunger == 0) {
                    FieryDeath();
                } else {
                    nextLevel = Store.NextLevel;
                    screenFading = true;
                }
            }
        }
    }

    private float stareTime;
    private float stareTotalTime = 4;

    void PopInMoogramal() {
        var rigidBody = player.GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;

        var demonCowPosition = player.transform.position + 12 * player.transform.forward;
        demonCowPosition.y = 2;
        demonCow = Instantiate(DemonCow,
            demonCowPosition,
            Quaternion.identity);
        demonCow.transform.LookAt(camera.transform);
        demonCow.SacrificeTarget = player.gameObject;
        demonCow.sacrificingPlayer = true;

        player.canMove = false;

        var mouseLook = camera.GetComponent<MouseLook>();
        mouseLook.canRotate = false;
        player.transform.LookAt(demonCow.transform);
        var xAngle = player.transform.localRotation.eulerAngles.y;
        mouseLook.xAngleRange = new Vector2(xAngle, xAngle);
        mouseLook.yAngleRange = new Vector2(5, 5);

        stareTime = stareTotalTime;
    }

    private bool startedFiery = false;
    private DemonCowSacrifice demonCow;

    void FieryDeath() {
        var mouseLook = camera.GetComponent<MouseLook>();

        if (!startedFiery) {
            foreach (var cow in GameObject.FindGameObjectsWithTag("AI Cow")) {
                Destroy(cow);
            }
            foreach (var grass in GameObject.FindGameObjectsWithTag("Grass")) {
                Destroy(grass);
            }

            GameObject.Find("HungerBar").GetComponent<RectTransform>().localScale = Vector3.zero;
            GameObject.Find("Hunger Label").GetComponent<RectTransform>().localScale = Vector3.zero;

            foreach (var cow in GameObject.FindGameObjectsWithTag("AI Cow")) {
                cow.GetComponent<AudioSource>().volume = 0;
            }
            var rigidBody = player.GetComponent<Rigidbody>();
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;

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
            player.transform.LookAt(demonCow.transform);
            var xAngle = player.transform.localRotation.eulerAngles.y;
            mouseLook.xAngleRange = new Vector2(xAngle, xAngle);
            mouseLook.yAngleRange = new Vector2(5, 5);

            stareTime = stareTotalTime;
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
            foreach (var fence in GameObject.FindGameObjectsWithTag("Fence")) {
                Destroy(fence);
            }
            var ground = GameObject.Find("Ground");
            Destroy(ground);

            var rigidBody = player.GetComponent<Rigidbody>();
            rigidBody.constraints = RigidbodyConstraints.None;
        }
    }
}
