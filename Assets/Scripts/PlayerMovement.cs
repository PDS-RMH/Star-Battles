using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] float xRotationSpeed = 1.0f;

	// Update is called once per frame
	void Update () {

        Debug.Log(Input.acceleration.x);
        Debug.Log(Input.acceleration.y);
        Debug.Log(Input.acceleration.z);

        //transform.Rotate(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
    }
}
