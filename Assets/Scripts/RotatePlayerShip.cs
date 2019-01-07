using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerShip : MonoBehaviour {

    public Rigidbody rb;
    public float torque = 100;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    public void OnPress()
    {
        rb.AddTorque(Vector3.left * torque);
    }


}
