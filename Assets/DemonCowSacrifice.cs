using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCowSacrifice : MonoBehaviour {
    public GameObject SacrificeTarget;

    private float sacrificeDelay = 3;

    void Start () {
		
	}
	
	void Update () {
        sacrificeDelay -= Time.deltaTime;
        if (sacrificeDelay <= 0) {
            Destroy(SacrificeTarget);
            Destroy(this);
        }
	}
}
