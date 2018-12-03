using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHumanDetection : MonoBehaviour {
    private AIHuman aiHuman;

	void Start () {
        aiHuman = transform.parent.gameObject.GetComponent<AIHuman>();
	}
	
	void Update () {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Cow") {
            aiHuman.FoundPlayer(other.GetComponent<Player>());
        }
    }
}
