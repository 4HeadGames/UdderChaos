using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int hunger = 10;
    public LevelController levelController;
    public float speed = 10.0f;

    private float translation;
    private float strafe;

    private Ray ray;
    private RaycastHit hit;

    void Start() {

    }

    void Update() {
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(strafe, 0, translation);

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            handleHit(hit);
        }
    }

    private void handleHit(RaycastHit hit) {
        if (Input.GetKeyDown(KeyCode.E)) {
            GameObject collision = getTopParent(hit.collider.gameObject);

            var minDistance = 5;
            var sqrDistance = minDistance * minDistance;
            var distance = Camera.main.transform.position - collision.transform.position;
            if (distance.sqrMagnitude > sqrDistance) {
                return;
            }

            switch (collision.name) {
                case "Grass":
                    eatGrass();
                    break;
                case "Chicken":
                    levelController.Sacrifice(collision);
                    break;
                default:
                    break;
            }
        }
    }

    private GameObject getTopParent(GameObject gameObject) {
        var parent = gameObject;
        while (parent.transform.parent != null) {
            parent = parent.transform.parent.gameObject;
        }

        return parent;
    }

    private void eatGrass() {
        Destroy(hit.transform.gameObject);
        hunger += 2;
    }
}
