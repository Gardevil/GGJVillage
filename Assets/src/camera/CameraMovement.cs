﻿using UnityEngine;
using System.Collections;
using System;

public class CameraMovement : MonoBehaviour {

    public float WidthBound=100;
    public float HeightBound=100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float x = transform.position.x + Input.GetAxis("Horizontal");
        x = Math.Max(-WidthBound, Math.Min(WidthBound, x));

        /// cambiar position.y por position.z en el top down
        float z=transform.position.y+Input.GetAxis("Vertical");
        z = Math.Max(-HeightBound, Math.Min(HeightBound, z));

        //transform.position = new Vector3(x, transform.position.y, z);
        transform.position = new Vector3(x, z, transform.position.z);
	}
}