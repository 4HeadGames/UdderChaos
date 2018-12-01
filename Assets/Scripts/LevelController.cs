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
            var newChicken = Instantiate(chicken,
                new Vector3(
                    Random.Range(backLeftCorner.x, forwardRightCorner.x),
                    0,
                    Random.Range(backLeftCorner.y, forwardRightCorner.y)),
                rotation);
            newChicken.name = "Chicken";
        }
    }

    void Update() {

    }

    public void Sacrifice(GameObject gameObject) {
        Destroy(gameObject);
    }
}
