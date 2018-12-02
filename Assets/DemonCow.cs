using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCow : MonoBehaviour {
    private AudioSource audioSource;
    private float intensity = 0;
    private float intensityCycleTime = 3;
    private float intensityMax = 0.6f;
    private bool increasingIntensity = false;
    private AudioClip[] audioClips;


    void Start () {
        audioSource = GetComponent<AudioSource>();

        audioClips = new AudioClip[] {
            Resources.Load<AudioClip>("Sounds/Demon Cow/1"),
            Resources.Load<AudioClip>("Sounds/Demon Cow/2"),
            Resources.Load<AudioClip>("Sounds/Demon Cow/3"),
            Resources.Load<AudioClip>("Sounds/Demon Cow/4"),
            Resources.Load<AudioClip>("Sounds/Demon Cow/5"),
            Resources.Load<AudioClip>("Sounds/Demon Cow/6"),
            Resources.Load<AudioClip>("Sounds/Demon Cow/7"),
            Resources.Load<AudioClip>("Sounds/Demon Cow/8"),
            Resources.Load<AudioClip>("Sounds/Demon Cow/9"),
            Resources.Load<AudioClip>("Sounds/Demon Cow/10"),
            Resources.Load<AudioClip>("Sounds/Demon Cow/11"),
            Resources.Load<AudioClip>("Sounds/Demon Cow/12"),
            Resources.Load<AudioClip>("Sounds/Demon Cow/13"),
            Resources.Load<AudioClip>("Sounds/Demon Cow/14"),
        };
    }

    void Update () {
        if (Random.value > 0.98) {
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length - 1)]);
        }

        if (increasingIntensity) {
            intensity += Time.deltaTime / intensityCycleTime * intensityMax;
            if (intensity >= intensityMax) {
                intensity = intensityMax;
                increasingIntensity = false;
            }
        } else {
            intensity -= Time.deltaTime / intensityCycleTime;
            if (intensity <= 0) {
                intensity = 0;
                increasingIntensity = true;
            }
        }

        var childRenderers = transform.root.GetComponentsInChildren<Renderer>();
        foreach (var renderer in childRenderers) {
           renderer.material.SetColor("_EmissionColor", new Color(intensity, 0, 0));
        }
    }
}
