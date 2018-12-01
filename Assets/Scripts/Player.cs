using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class Player : MonoBehaviour {

	[SerializeField]
	private int hunger = 10;
	private Ray ray;
	private RaycastHit hit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)) {
			handleHit(hit);
		}
	}

	private void handleHit(RaycastHit hit) {
		if (Input.GetKeyDown(KeyCode.E)) {
			switch(hit.collider.name) {
				case "Grass":
					eatGrass();
					break;
				default:
					break;
			}
		}
	}

	private void eatGrass() {
		Destroy(hit.transform.gameObject);
		hunger += 2;
	}
=======
public class PlayerScript : MonoBehaviour {
    void Start() {
        
    }

    void Update() {

    }
>>>>>>> eb09dde8240d33a6c9c2875702608824b613ac22
}
