using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {

	private Outline outline;

	// Use this for initialization
	void Start () {
		outline = gameObject.GetComponent<Outline>();
		outline.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseOver() {
		outline.enabled = true;
	}

	void OnMouseExit() {
		outline.enabled = false;
	}

}
