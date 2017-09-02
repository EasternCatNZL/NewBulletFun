using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSpray : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Shots")]
    public float timeBetweenShots = 0.02f;
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1.5f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet Object")]
    public GameObject bulletObject;
    [Tooltip("Number of bullets in each spray")]
    public int numBulletsPerSpray = 5;

    [Header("Angle Control")]
    [Tooltip("Angle change per shot in spray")]
    public float angleChangePerShot = 1.0f;
    [Tooltip("Angle change per spray")]
    public float angleChangePerSpray = 20.0f;
    [Tooltip("Positive or negative")]
    [Range(-1, 1)]
    public float rotationDirection = 1.0f;

    //control vars
    private int shotsFiredInSpray = 0;
    private float timeLastShotFired = 0.0f;
    private float timeLastSprayFired = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
