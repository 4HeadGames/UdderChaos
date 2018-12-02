using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour {

    private GameObject[] grass;
    private int index;
    private bool eatingGrass;
    public float speed;
	
    void Start () {
        grass = GameObject.FindGameObjectsWithTag("Grass");
        index = 0;
        eatingGrass = false;
    }

	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;

        if (!eatingGrass && grass.Length > 0) {
            // transform.LookAt(grass[index].transform);
            transform.position = Vector3.MoveTowards(transform.position, grass[index].transform.position, step);
        }

        if (transform.position.x <= grass[index].transform.position.x + 2.0f ||
            transform.position.x >= grass[index].transform.position.x - 2.0f) {
                
            eatGrass();
        }
	}

    void eatGrass() {
        eatingGrass = true;
        Destroy(grass[index]);
        index += 1;
        eatingGrass = false;
    }

}
