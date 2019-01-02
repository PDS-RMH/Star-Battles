using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

 //   [SerializeField] float xRotationSpeed = 1.0f;
    public Text xAcceleration;
    public Text yAcceleration;
    public Text zAcceleration;
    public Text position;
    public GameObject go;
    public Rigidbody rb;
    public float attitudeSensitivity = 1f;
    public float thrust = 10f;
    public Button resetRotations;

    // Variables to read and store the instantaneous rotation when button is pressed
    private float xAccelerationInstant;
    private float yAccelerationInstant;
    private float zAccelerationInstant;

    private void Start()
    {
        rb.GetComponent<Rigidbody>();
        resetRotations.onClick.AddListener(ResetRotationsFunction);
    }

    public void Update()
    {
        // Verify system that game is running on
#if UNITY_ANDROID
        AndroidPlayerControls();
#endif
        PCPlayerControls();

        xAccelerationInstant = Input.acceleration.x;
    }

    private void FixedUpdate()
    {
        ShowUIText();
    }


    // Reset rotations after button is pressed
    public void ResetRotationsFunction()
    {
        // X Input
        if (Input.acceleration.x > 0)
        {
            xAccelerationInstant = 0f - Input.acceleration.x;
        }

        else
        {
            xAccelerationInstant = 0f + Input.acceleration.x;
        }

    }

    void ShowUIText()
    {
        // X acceleration is 0 when level, and +90 when rotated 90 degrees counter clockwise
        xAcceleration.text = "X Acceleration: " + Input.acceleration.x.ToString();

        // y = 90 minus x (verify)
        yAcceleration.text = "Y Acceleration: " + Input.acceleration.y.ToString();

        //Attitude control
        zAcceleration.text = "Z Acceleration: " + Input.acceleration.z.ToString();
        position.text = "X Position" + this.transform.position.x.ToString();
        
    }

    public void AndroidPlayerControls()
    {
        go.transform.Rotate(Vector3.forward, Input.acceleration.z * attitudeSensitivity, Space.Self);
        go.transform.Rotate(Vector3.up, xAccelerationInstant * attitudeSensitivity, Space.Self);

        
    }

    public void PCPlayerControls()
        {
                if (Input.GetKey("w") == true)
        {
            rb.AddRelativeForce(0, -10f * thrust, 0, ForceMode.Force);
        }
        }
}