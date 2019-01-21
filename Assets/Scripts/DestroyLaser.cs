using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLaser : MonoBehaviour {

    public float TimeToLive = 0.5f;
    void Start()
    {
        Destroy(gameObject, TimeToLive);
    }
}
