using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireMainGuns : MonoBehaviour {

    public Button fireButton;

    public GameObject go;
    public Rigidbody rb;
    public Transform tr;
    public float laserSpeed = 100f;


    // Use this for initialization
    void Start () {
        fireButton.onClick.AddListener(FireFunction);
        rb = go.GetComponent<Rigidbody>();
        tr = go.GetComponent<Transform>();
    }
	
    public void FireFunction()
    {
        Rigidbody clone;
        clone = Instantiate(rb, transform.position, transform.rotation);

        // Give the cloned object an initial velocity along the current
        // object's Z axis
        clone.velocity = transform.TransformDirection(Vector3.right * laserSpeed);
    }

}
