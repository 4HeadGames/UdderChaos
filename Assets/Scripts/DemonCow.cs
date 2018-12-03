﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonCow : MonoBehaviour {
    public Text text;
    public Player player;

    private AudioSource audioSource;
    private AudioClip[] audioClips;
    private float intensity = 0;
    private float intensityCycleTime = 3;
    private float intensityMax = 0.6f;
    private bool increasingIntensity = false;
    private Vector3 initialPosition;

    private float talkingDuration = 0;

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

        initialPosition = transform.position;
    }

    void Update () {
        if (talkingDuration > 0) {
            talkingDuration -= Time.deltaTime;
            if (Random.value > 0.98) {
                audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length - 1)]);
            }
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
        Color color = new Color(intensity, 0, 0);
        foreach (var renderer in childRenderers) {
           renderer.material.SetColor("_EmissionColor", color);
        }

        //transform.position = initialPosition + new Vector3(
        //    Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f));
        // transform.LookAt(player.transform);

        text.color = color;
    }

    public void SayText(string dialogue) {
        var words = dialogue.Split(' ').Length;
        talkingDuration = words * 1;

        text.text = dialogue;
    }

    public bool Talking() {
        return talkingDuration > 0;
    }
}
