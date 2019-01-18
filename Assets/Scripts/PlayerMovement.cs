using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour {

 //   [SerializeField] float xRotationSpeed = 1.0f;
    public Text xAcceleration;
    public Text yAcceleration;
    public Text zAcceleration;
    public Text position;
    public Text velocityText;
    public GameObject go;
    public Rigidbody rb;
    public float attitudeSensitivity = 1f;
    public float thrust = 1f;
    public Button resetRotations;
    public Slider thrustSlider;
    [SerializeField] float speedLimit = 100f;
    public float playerVelocity;

    // Variables to read and store the instantaneous rotation when button is pressed
    private float xAccelerationDelta;
 //   private float yAccelerationDelta;
    private float zAccelerationDelta;


    private void Start()
    {
        rb.GetComponent<Rigidbody>();
        resetRotations.onClick.AddListener(ResetRotationsFunction);
        xAccelerationDelta = -Input.acceleration.x;
 //       yAccelerationDelta = -Input.acceleration.y;
        zAccelerationDelta = -Input.acceleration.z;
    }


    public void FixedUpdate()
    {
        // Verify system that game is running on
#if UNITY_ANDROID
        AndroidPlayerControls();
#endif
        PCPlayerControls();

        SpeedLimit();

    }


    public void Update()
    {
        ShowUIText();
    }


    // Calculates and displays the velocity of the player, and limits the maximum velocity
    public void SpeedLimit()
    {
        playerVelocity = Mathf.Round(rb.velocity.magnitude * 10f) / 10f;
//        Debug.Log(playerVelocity);

        if(playerVelocity > speedLimit)
        {
            rb.velocity = rb.velocity.normalized * speedLimit;
            thrust = 0f;
        }
        else if(playerVelocity < speedLimit)
        {
            thrust = 1f;
        }
    }


    // Reset rotations after button is pressed
    public void ResetRotationsFunction()
    {
        xAccelerationDelta = -Input.acceleration.x;
//        yAccelerationDelta = -Input.acceleration.y;
        zAccelerationDelta = -Input.acceleration.z;

    }


    void ShowUIText()
    {
        // X acceleration is 0 when level, and +90 when rotated 90 degrees counter clockwise
        xAcceleration.text = "X Acceleration: " + Input.acceleration.x.ToString();

        // y = 90 minus x (verify)
        yAcceleration.text = "Y Acceleration: " + Input.acceleration.y.ToString();

        //Attitude control
        zAcceleration.text = "Z Acceleration: " + Input.acceleration.z.ToString();

        //Positional Text
        position.text = "X Position: " + this.transform.position.x.ToString();

        //Speed Text
        velocityText.text = "Speed: " + playerVelocity.ToString();

    }


    public void AndroidPlayerControls()
    {
        //Rotate player ship based on accelerometer inputs
        go.transform.Rotate(Vector3.forward, (Input.acceleration.z + zAccelerationDelta) * attitudeSensitivity, Space.Self);
        go.transform.Rotate(Vector3.up, (Input.acceleration.x + xAccelerationDelta) * attitudeSensitivity, Space.Self);

        //Reverse Thrust for Slider
        rb.AddRelativeForce(0, thrustSlider.value * -thrust, 0, ForceMode.Force);

    }


    public void PCPlayerControls()
        {
                if (Input.GetKey("w") == true)
        {
            rb.AddRelativeForce(0, -10f * thrust, 0, ForceMode.Force);
        }
        }
}