﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public Vector3 rotationSpeed;

    // Use this for initialization
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
