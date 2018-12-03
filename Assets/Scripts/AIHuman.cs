using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHuman : MonoBehaviour {
    private LevelController levelController;
    private AudioSource audioSource;
    private AudioClip[] audioClips;
    private int pathIndex;
    private bool forward = true;

    private Player playerTarget;

    void Start() {
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
    }

    void Update() {
        float step = 10 * Time.deltaTime;
        float vertical = 0.2f;

        if (playerTarget == null) {
            return;
        }

        var targetPosition = new Vector3(playerTarget.transform.position.x, vertical, playerTarget.transform.position.z);

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
            levelController.PlayerCaught();
        }
    }

    public void FoundPlayer(Player player) {
        Debug.Log(player);
        playerTarget = player;
    }
}
