using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour {
	public DemonCow demonCow;
	public DemonCowSacrifice sacrifce;
	private DemonCowSacrifice spawnedDemonCow;
	public Farmer farmer;
	private string[] farmerScript = {
		"It was you all along.",
		"We saw you sneaking around the sheep pin.",
		"I don't know how you were making the animals disappear, but it won't happen again.",
		"We're putting you dow-",
		"WHAT IN TARNATION?!?!?"
	};
	private string[] demonScript = {
		"Mortal human. You think you can harm my children?",
		"You shall be the last sacrifice."
	};
	private int farmerLine = 0;
	private int demonLine = 0;

	private bool sacrificed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sacrificed) {
			farmer.text.text = "Thanks for playing :D";
		} else {
			if (!farmer.Talking() && farmerLine != 4) {
				if (farmerLine < farmerScript.Length) {
					farmer.SayText(farmerScript[farmerLine]);
					farmerLine += 1;
				}
			}

			if (farmerLine == 4 && !farmer.Talking() && !demonCow.Talking()) {
				farmer.text.text = "";
				if (demonLine == 2) {
					demonCow.text.text = "";
					farmer.SayText(farmerScript[farmerLine]);
					Sacrifice(GameObject.Find("Farmer 1"));
				}
				if (demonLine < demonScript.Length) {
					demonCow.SayText(demonScript[demonLine]);
					demonLine += 1;
				}
			}
		}
	}

	public void Sacrifice(GameObject gameObject) {
        if (spawnedDemonCow != null) {
            // Can only sacrifice one at a time.
            return;
        }
        
        var player = GameObject.Find("Player");
        var demonCowPosition = player.transform.position + 8 * player.transform.forward;
        demonCowPosition.y = 2;
        spawnedDemonCow = Instantiate(sacrifce,
            demonCowPosition,
            Quaternion.identity);
        spawnedDemonCow.transform.LookAt(gameObject.transform);
        spawnedDemonCow.SacrificeTarget = gameObject;
		sacrificed = true;
    }
}
