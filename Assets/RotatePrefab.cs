//  Give a prefab random rotation in all three rotational DOF's
//  Rob Hill - Pixel Dot Studios, LLC 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePrefab : MonoBehaviour {

    private float xRotationRate;
    private float yRotationRate;
    private float zRotationRate;

    private void Start()
    {
        xRotationRate = Random.Range(-20f, 20f);
        yRotationRate = Random.Range(-20f, 20f);
        zRotationRate = Random.Range(-20f, 20f);
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(xRotationRate * Time.deltaTime, yRotationRate * Time.deltaTime, zRotationRate * Time.deltaTime);
	}
}
