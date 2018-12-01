using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private int hunger = 10;
    private Ray ray;
    private RaycastHit hit;

    public LevelController levelController;

    void Start() {

    }

    void Update() {
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
