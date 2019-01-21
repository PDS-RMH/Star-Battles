using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffectBlue1 : MonoBehaviour {

    public GameObject go;
    public Rigidbody rb;
    public Transform tr;
    public float laserSpeed = 100f;
       
	// Use this for initialization
	void Start () {

        rb = go.GetComponent<Rigidbody>();
        tr = go.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            Rigidbody clone;
            clone = Instantiate(rb, transform.position, transform.rotation);

            // Give the cloned object an initial velocity along the current
            // object's Z axis
            clone.velocity = transform.TransformDirection(Vector3.right * laserSpeed);

        }
	}
}
