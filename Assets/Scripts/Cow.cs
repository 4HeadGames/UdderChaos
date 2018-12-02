using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour {
    private GameObject targetGrass;
    private int index;
    private bool eatingGrass = false;
    public float speed;
	
    void Start () {
    }

	// Update is called once per frame
	void Update () {
        if (targetGrass == null) {
            var allGrass = GameObject.FindGameObjectsWithTag("Grass");
            if (allGrass.Length == 0) {
                return;
            }
            targetGrass = allGrass[Random.Range(0, allGrass.Length)];
        }

        float step = speed * Time.deltaTime;

        if (!eatingGrass) {
            var lookPos = targetGrass.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
            transform.position = Vector3.MoveTowards(transform.position, targetGrass.transform.position, step);
        }

        float minDistance = 4f;
        var sqrDistance = minDistance * minDistance;
        var distance = transform.position - targetGrass.transform.position;
        if (distance.sqrMagnitude < sqrDistance) {
            eatGrass(targetGrass);
        }
	}

    void eatGrass(GameObject grass) {
        eatingGrass = true;
        Destroy(grass);
        eatingGrass = false;
    }

}
