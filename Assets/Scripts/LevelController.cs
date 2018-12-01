using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    public Transform chicken;

    void Start() {
        for (int i = 0; i < 20; i++) {
            Quaternion rotation = Random.rotation;
            rotation.y = 0;
            rotation.w = 0;
            Instantiate(chicken,
                new Vector3(
                    Random.Range(5, 15),
                    0,
                    Random.Range(5, 15)),
                rotation);
        }
    }

    void Update() {

    }
}
