using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] float xRotationSpeed = 1.0f;
    public Text xAcceleration;
    public Text yAcceleration;
    public Text zAcceleration;
    public Text position;
    public GameObject go;


    private void Update()
    {
        ShowUIText();
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

}
