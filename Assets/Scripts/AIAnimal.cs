using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimal : MonoBehaviour {
    private float jumpTimer;

    private float velocity = 2;
    private float minVelocity = 1;
    private float maxVelocity = 5;
    private float velocityUpdateTimer;
    private float velocityUpdateCooldown = 2;

    private float rotationUpdateTimer;
    private float rotationCooldown = 5;
    private float rotationTimer = 0;
    private float rotationDuration = 1;
    private float rotation;

    private Outline outline;
    private Rigidbody body;

    void Start() {
        outline = GetComponent<Outline>();
        outline.enabled = false;

        body = GetComponent<Rigidbody>();

        jumpTimer = randomJumpCooldown();
        velocityUpdateTimer = velocityUpdateCooldown;
        rotationUpdateTimer = rotationCooldown;

        rotation = Random.Range(0, 360);
    }

    void Update() {
        jumpTimer -= Time.deltaTime;
        if (jumpTimer <= 0) {
            jumpTimer = randomJumpCooldown();
            velocity = Random.Range(minVelocity, maxVelocity);
            Vector3 forward = transform.forward * velocity;
            body.velocity += new Vector3(
                forward.x,
                3,
                forward.z);
        }

        if (rotationTimer <= 0) {
            rotationUpdateTimer -= Time.deltaTime;
            if (rotationUpdateTimer <= 0) {
                rotationUpdateTimer = rotationCooldown;
                rotation = Random.Range(-5f, 5f);
                rotationTimer = rotationDuration;
            }
        }

        if (rotationTimer >= 0) {
            transform.Rotate(new Vector3(0, rotation, 0));
            rotationTimer -= Time.deltaTime;
        }
    }

    void OnMouseOver() {
        var minDistance = 5;
        var sqrDistance = minDistance * minDistance;
        var distance = Camera.main.transform.position - transform.position;
        if (distance.sqrMagnitude <= sqrDistance) {
            outline.enabled = true;
        }
    }

    void OnMouseExit() {
        outline.enabled = false;
    }

    float randomJumpCooldown() {
        return Random.Range(0.5f, 5);
    }
}
