using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularStraightBullet : MonoBehaviour {

    [Header("Speed")]
    [Tooltip("Speed of bullet")]
    public float travelSpeed = 3.0f;

    private Rigidbody myRigid;

	// Use this for initialization
	void Start () {
        myRigid = GetComponent<Rigidbody>();
        myRigid.velocity = transform.forward * travelSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
