using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThrustLighting : MonoBehaviour {

    public Slider thrustSlider;
    public float lightIntensityFactor = 0.2f;
    public Light lt;

	// Use this for initialization
	void Start () {
        thrustSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        lt.intensity = thrustSlider.value * lightIntensityFactor;
    }

}
