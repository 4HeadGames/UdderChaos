﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;
	private float translation;
	private float strafe;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		transform.Translate(strafe, 0, translation);
	}
}
