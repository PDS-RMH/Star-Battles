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
    private float resetThrust;

    public Button resetRotations;
    public Slider thrustSlider;

    [SerializeField] float speedLimit = 100f;

    public float playerVelocity;
    //Define Gyroscope Variables
    Gyroscope player_gyro;
    private float xGyroDelta;
    private float yGyroDelta;
    private float zGyroDelta;

    // Variables to read and store the instantaneous rotation when button is pressed
    private float xAccelerationDelta;
    private float zAccelerationDelta;


    private void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.GetComponent<Rigidbody>();
        resetRotations.onClick.AddListener(ResetRotationsFunction);
        xAccelerationDelta = -Input.acceleration.x;
        zAccelerationDelta = -Input.acceleration.z;
        player_gyro = Input.gyro;
        player_gyro.enabled = true;

        xGyroDelta = -player_gyro.attitude.x;
        yGyroDelta = -player_gyro.attitude.y;
        zGyroDelta = -player_gyro.attitude.z;

        resetThrust = thrust;
    }


    public void FixedUpdate()
    {
        // Verify system that game is running on
#if UNITY_ANDROID
        AndroidPlayerControls();
#endif
        PCPlayerControls();
    }


    public void Update()
    {
        ShowUIText();
        SpeedLimit();

    }


    // Displays the velocity of the player, and limits the maximum velocity
    public void SpeedLimit()
    {
        playerVelocity = Mathf.Round(rb.velocity.magnitude * 10f) / 10f;

        if(playerVelocity >= speedLimit)
        {
            rb.velocity = rb.velocity.normalized * speedLimit;
        }
        else 
        {
            thrust = resetThrust;
        }

        /*
         * BUILD SECTION FOR PLAYER VELOCITY LIMITS IN Y-COORDINATES ...
         * METHOD 1:  FORCE IN Y-DIRECTION EQUAL TO MAGNITUDE BETWEEN Y=0 AND PLAYER COORDINATE
         * METHOD 2:  PLAYER VELOCITY.Y * 0.05 ABOVE CEILING AND BELOW FLOOR.  PLAYER MAY GET "STUCK".
        */
        
        // Ceiling Rebound Force
        
        if(go.transform.position.y > 200f || go.transform.position.y < -200f)
        {
            float reboundForce = (float)go.transform.position.y;
            rb.AddForce(0f, 0f, -reboundForce * 0.2f, ForceMode.Force);
        }
        

    }


    // Reset rotations after button is pressed
    public void ResetRotationsFunction()
    {
        if (player_gyro.enabled == true)
        {
            xGyroDelta = -player_gyro.attitude.x;
            yGyroDelta = -player_gyro.attitude.y;
            zGyroDelta = -player_gyro.attitude.z;
        }
        else
        {
            xAccelerationDelta = -Input.acceleration.x;
            zAccelerationDelta = -Input.acceleration.z;
        }
    }


    void ShowUIText()
    {
        // X acceleration is 0 when level, and +90 when rotated 90 degrees counter clockwise
        xAcceleration.text = "Attitude: " + player_gyro.attitude.ToString();

        // y = 90 minus x (verify)
        yAcceleration.text = "Rotation Rate: " + player_gyro.rotationRate.ToString();

        //Attitude control
        zAcceleration.text = "Rotatoin Rate UB: " + player_gyro.rotationRateUnbiased.ToString();

        //Positional Text
        position.text = "X Position: " + this.transform.position.x.ToString();

        //Speed Text
        if (playerVelocity >= (speedLimit * 0.99f))
        {
            velocityText.text = speedLimit.ToString();
        }
        else
        {
            velocityText.text = "Speed: " + playerVelocity.ToString();
        }
    }


    public void AndroidPlayerControls()
    {
        if (player_gyro.enabled == true)
        {
            go.transform.Rotate(Vector3.forward, (player_gyro.attitude.x + xGyroDelta) * attitudeSensitivity, Space.Self);
            go.transform.Rotate(Vector3.right, (player_gyro.attitude.y + yGyroDelta) * attitudeSensitivity, Space.Self);
            go.transform.Rotate(Vector3.up, (player_gyro.attitude.z + zGyroDelta) * attitudeSensitivity, Space.Self);

            //Reverse Thrust for Slider
            rb.AddRelativeForce(0, thrustSlider.value * -thrust, 0, ForceMode.Force);
        }
        else
        {
            //Rotate player ship based on accelerometer inputs
            go.transform.Rotate(Vector3.forward, (Input.acceleration.z + zAccelerationDelta) * attitudeSensitivity, Space.Self);
            go.transform.Rotate(Vector3.up, (Input.acceleration.x + xAccelerationDelta) * attitudeSensitivity, Space.Self);

            //Reverse Thrust for Slider
            rb.AddRelativeForce(0, thrustSlider.value * -thrust, 0, ForceMode.Force);
        }
    }


    public void PCPlayerControls()
        {
        /*
                if (Input.GetKey(KeyCode.W) == true)
                {
                    rb.AddRelativeForce(0, -10f * thrust, 0, ForceMode.Force);
                }
                if(Input.GetKey(KeyCode.R) == true)
                {
                    go.transform.Rotate(Vector3.forward, 2f, Space.Self);
                }
        */
        }
}