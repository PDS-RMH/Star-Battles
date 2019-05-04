using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireMainGuns : MonoBehaviour {

    public Button fireButton;
    public Rigidbody rbProjectile;
    public Rigidbody rbLauncher;
    public float laserSpeed = 100f;


    void Start () {
        fireButton.onClick.AddListener(FireFunction);
    }
	
    public void FireFunction()
    {
        //Create clone of object using the position and rotation of the game object the script is attached to
        Rigidbody clone;
        clone = Instantiate(rbProjectile, transform.position, Quaternion.identity);

        // Give the cloned object an initial velocity along the current + laser speed velocity
        clone.velocity = transform.TransformDirection(rbLauncher.velocity + Vector3.right * laserSpeed);
    }
}
