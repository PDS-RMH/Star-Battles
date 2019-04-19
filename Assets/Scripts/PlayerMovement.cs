using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour {

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

    //Define Gyroscope Variables
    Gyroscope player_gyro;
    private float xGyroDelta;
    private float yGyroDelta;
    private float zGyroDelta;

    private float xGyroFinal;
    private float yGyroFinal;
    private float zGyroFinal;

    // Variables to read and store the instantaneous rotation when button is pressed
    private float xAccelerationDelta;
    private float zAccelerationDelta;


    private void Start()
    {
        rb.GetComponent<Rigidbody>();
        resetRotations.onClick.AddListener(ResetRotationsFunction);
        xAccelerationDelta = -Input.acceleration.x;
        zAccelerationDelta = -Input.acceleration.z;
        
        player_gyro = Input.gyro;
        player_gyro.enabled = true;

        xGyroDelta = -player_gyro.attitude.x;
        yGyroDelta = -player_gyro.attitude.y;
        zGyroDelta = -player_gyro.attitude.z;
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
        SceneBoundaries();
        playerVelocity = Mathf.Round(rb.velocity.magnitude * 10f) / 10f;
    }


    // Method to restrict players transform beyond limits of the scene
    public void SceneBoundaries()
    {
        
        /*
         * BUILD SECTION FOR PLAYER VELOCITY LIMITS IN Y-COORDINATES ...
         * METHOD 1:  FORCE IN Y-DIRECTION EQUAL TO MAGNITUDE BETWEEN Y=0 AND PLAYER COORDINATE
         * METHOD 2:  PLAYER VELOCITY.Y * 0.05 ABOVE CEILING AND BELOW FLOOR.  PLAYER MAY GET "STUCK".
        */
        
        // Ceiling Rebound Force
        
        /*
        if(go.transform.position.y > 200f || go.transform.position.y < -200f)
        {
            float reboundForce = (float)go.transform.position.y;
            rb.AddForce(0f, 0f, -reboundForce * 0.2f, ForceMode.Force);
        }
        */

    }


    // Reset rotations after button is pressed
    public void ResetRotationsFunction()
    {
        if (player_gyro.enabled == false)
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
        xAcceleration.text = "x Attitude: " + xGyroFinal;

        // y = 90 minus x (verify)
        yAcceleration.text = "y Attitude" + player_gyro.attitude.y.ToString();

        //Attitude control
        zAcceleration.text = "z Attitude: " + player_gyro.attitude.z.ToString();

        //Positional Text
//        position.text = "X Position: " + this.transform.position.x.ToString();

        //Display speed as string text
        if (playerVelocity >= (speedLimit * 0.99f))
        {
            velocityText.text = speedLimit.ToString();
        }
        else
        {
            if(thrustSlider.value >= 0.0f) // Change method to detect if negative velocity.  Local velocity instead of worldspace, verify correct axis.
            {
                velocityText.text = "Speed: " + playerVelocity.ToString();
                // Change color of slider based on slider value
            }
            else
            {
                velocityText.text = "Speed: -" + playerVelocity.ToString();
            }
        }
    }


    public void AndroidPlayerControls()
    {
        if (player_gyro.enabled == false)
        {
            //Attitude
            go.transform.Rotate(Vector3.forward, (player_gyro.attitude.x + xGyroDelta) * attitudeSensitivity, Space.Self);
            
            //Yaw
            go.transform.Rotate(Vector3.right, (player_gyro.attitude.y + yGyroDelta) * attitudeSensitivity, Space.Self);

            //Roll
            go.transform.Rotate(Vector3.up, (player_gyro.attitude.z + zGyroDelta) * attitudeSensitivity, Space.Self);

            //Reverse Thrust for Slider
            rb.AddRelativeForce(0, thrustSlider.value * -thrust, 0, ForceMode.Force);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speedLimit);
        }
        else
        {
            //Rotate player ship based on accelerometer inputs
            go.transform.Rotate(Vector3.forward, (Input.acceleration.z + zAccelerationDelta) * attitudeSensitivity, Space.Self);
            go.transform.Rotate(Vector3.up, (Input.acceleration.x + xAccelerationDelta) * attitudeSensitivity, Space.Self);
//            go.transform.Rotate(Vector3.right, -player_gyro.rotationRateUnbiased.y * 1.5f);

//            rb.AddTorque(Vector3.right * -player_gyro.rotationRateUnbiased.y * 1.5f);

            //Reverse Thrust for Slider
            rb.AddRelativeForce(0, thrustSlider.value * -thrust, 0, ForceMode.Force);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speedLimit);
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