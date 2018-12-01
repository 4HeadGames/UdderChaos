using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {
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
    private float rotation = 0;

    private Rigidbody body;

    void Start () {
        this.jumpTimer = this.randomJumpCooldown();
        this.velocityUpdateTimer = this.velocityUpdateCooldown;
        this.rotationUpdateTimer = this.rotationCooldown;

        this.body = GetComponent<Rigidbody>();
    }

    float randomJumpCooldown() {
        return Random.Range(0.5f, 5);
    }
	
	void Update () {
        this.jumpTimer -= Time.deltaTime;
        if (this.jumpTimer <= 0) {
            this.jumpTimer = this.randomJumpCooldown();
            this.body.velocity += new Vector3(0, 3, 0);
        }

        if (this.rotationTimer <= 0) {
            this.rotationUpdateTimer -= Time.deltaTime;
            if (this.rotationUpdateTimer <= 0) {
                this.rotationUpdateTimer = this.rotationCooldown;
                this.rotation = Random.Range(-5f, 5f);
                this.rotationTimer = this.rotationDuration;
            }
        }

        if (this.rotationTimer >= 0) {
            transform.Rotate(new Vector3(0, this.rotation, 0));
            this.rotationTimer -= Time.deltaTime;
        }

        this.velocityUpdateTimer -= Time.deltaTime;
        bool isJumping = this.body.velocity.y > 0;
        if (this.velocityUpdateTimer <= 0 && !isJumping) {
            this.velocityUpdateTimer = this.velocityUpdateCooldown;
            this.velocity = Random.Range(this.minVelocity, this.maxVelocity);
            Vector3 forward = transform.forward * this.velocity;
            this.body.velocity = new Vector3(
                forward.x,
                this.body.velocity.y,
                forward.z);
        }
    }
}
