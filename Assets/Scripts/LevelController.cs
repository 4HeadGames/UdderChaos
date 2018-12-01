using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    public Transform chicken;
    private Vector2 backLeftCorner = new Vector2(-15, 5);
    private Vector2 forwardRightCorner = new Vector2(15, 75);

    void Start() {
        for (int i = 0; i < 20; i++) {
            Quaternion rotation = Random.rotation;
            rotation.y = 0;
            rotation.w = 0;
            Instantiate(chicken,
                new Vector3(
                    Random.Range(this.backLeftCorner.x, this.forwardRightCorner.x),
                    0,
                    Random.Range(this.backLeftCorner.y, this.forwardRightCorner.y)),
                rotation);
        }
    }

    void Update() {

    }
}
