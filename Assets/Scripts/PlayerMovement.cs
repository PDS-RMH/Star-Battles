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
        xAcceleration.text = Input.acceleration.x.ToString();
        position.text = "X Position" + this.transform.position.x.ToString();
        
    }

}
