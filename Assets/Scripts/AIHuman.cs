using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHuman : MonoBehaviour {
    public float speed;
    public Vector2[] pathing;
    public LevelController levelController;

    private AudioSource audioSource;
    private AudioClip[] audioClips;
    private int pathIndex;
    private bool forward = true;

    private Player playerTarget;

    void Start() {
        // audioSource = GetComponent<AudioSource>();

        // audioClips = new AudioClip[] {};
    }

    void Update() {
        float step = speed * Time.deltaTime;
        float vertical = 0.2f;

        var targetPath = pathing[pathIndex];
        var targetPosition = new Vector3(targetPath.x, vertical, targetPath.y);

        if (playerTarget != null) {
            targetPosition = new Vector3(playerTarget.transform.position.x, vertical, playerTarget.transform.position.z);
            step *= 2;
        }

        var lookPos = targetPosition - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.3f);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        float minDistance = 4;
        if (playerTarget != null) {
            minDistance = 20;
        }
        var sqrDistance = minDistance * minDistance;
        var distance = transform.position - targetPosition;
        if (distance.sqrMagnitude < sqrDistance) {
            if (playerTarget == null) {
                nextPath();
            } else {
                levelController.PlayerCaught();
            }
        }
    }

    private void nextPath() {
        if (forward) {
            pathIndex += 1;
            if (pathIndex == pathing.Length) {
                forward = false;
                pathIndex = pathing.Length - 1;
            }
        } else {
            pathIndex -= 1;
            if (pathIndex == -1) {
                forward = true;
                pathIndex = 0;
            }
        }
    }

    public void FoundPlayer(Player player) {
        playerTarget = player;
    }
}
