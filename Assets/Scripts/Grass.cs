using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {
    private Outline outline;

    void Start() {
        outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }

    void Update() {

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

}
