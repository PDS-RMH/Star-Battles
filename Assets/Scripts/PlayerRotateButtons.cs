using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateButtons : MonoBehaviour {
    
    public bool isLeftRotationPressed = false;
    public bool isRightRotationPressed = false;
    public Rigidbody rb;
    public float torque = 10f;

    // Use this for initialization
    void Start () {
        rb.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isLeftRotationPressed)
        {
            rb.AddRelativeTorque(Vector3.left * torque);
        }
        if (isRightRotationPressed)
        {
            rb.AddRelativeTorque(-Vector3.left * torque);
        }
	}

    public void OnButtonDownLeftButton()
    {
        isLeftRotationPressed = true;
    }
    public void OnButtonUpLeftButton()
    {
        isLeftRotationPressed = false;
    }

    public void OnButtonDownRightButton()
    {
        isRightRotationPressed = true;
    }
    public void OnButtonUpRightButton()
    {
        isRightRotationPressed = false;
    }


}
