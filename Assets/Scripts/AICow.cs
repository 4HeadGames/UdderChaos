using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICow : MonoBehaviour {
    public float speed;

    private AudioSource audioSource;
    private AudioClip[] audioClips;
    private GameObject targetGrass;
    private bool eatingGrass = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();

        audioClips = new AudioClip[] {
            Resources.Load<AudioClip>("Sounds/Cow/1"),
            Resources.Load<AudioClip>("Sounds/Cow/2"),
            Resources.Load<AudioClip>("Sounds/Cow/3"),
            Resources.Load<AudioClip>("Sounds/Cow/4"),
            Resources.Load<AudioClip>("Sounds/Cow/5"),
            Resources.Load<AudioClip>("Sounds/Cow/6"),
            Resources.Load<AudioClip>("Sounds/Cow/7"),
            Resources.Load<AudioClip>("Sounds/Cow/8"),
        };
    }

	void Update () {
        if (Random.value > 0.999) {
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        }

        if (targetGrass == null) {
            var allGrass = GameObject.FindGameObjectsWithTag("Grass");
            if (allGrass.Length == 0) {
                return;
            }
            targetGrass = allGrass[Random.Range(0, allGrass.Length)];
        }

        float step = speed * Time.deltaTime;

        if (!eatingGrass) {
            var lookPos = targetGrass.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.3f);
            transform.position = Vector3.MoveTowards(transform.position, targetGrass.transform.position, step);
        }

        float minDistance = 4f;
        var sqrDistance = minDistance * minDistance;
        var distance = transform.position - targetGrass.transform.position;
        if (distance.sqrMagnitude < sqrDistance) {
            eatGrass(targetGrass);
        }
	}

    void eatGrass(GameObject grass) {
        eatingGrass = true;
        Destroy(grass);
        eatingGrass = false;
    }

}
