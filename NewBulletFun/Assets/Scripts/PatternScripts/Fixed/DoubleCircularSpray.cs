﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCircularSpray : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Shots")]
    public float timeBetweenShots = 0.02f;
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1.5f;
    //[Tooltip("Shot Recharge Time")]
    //public float timeShotRecharge = 3.0f;

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
    [Tooltip("Positive or negative (1 or -1)")]
    [Range(-1, 1)]
    public float rotationDirection = 1.0f;

    //control vars
    private int shotsFiredInSpray = 0; //the number of shots fired in the current spray
    private float timeLastShotFired = 0.0f; //the time last single shot was fired
    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngle = 0.0f; //the current angle the bullet is angled at in regards to owner
    private bool canShootBullet = false; //checks whether bullet can be fired
                                         //private bool canShootSpray = false; //checks whether spray can begin

    // Use this for initialization
    void Start()
    {
        StartCoroutine(BulletSprayRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        //FireBullets();
        //ControlVars();
    }

    //Handles the shooting of bullets
    private void FireBullets()
    {
        //check if can fire bullet
        if (canShootBullet && Time.time > timeBetweenShots + timeLastShotFired)
        {
            //get the current angle as a quaternion
            Quaternion currentRotation = new Quaternion();
            currentRotation.eulerAngles = new Vector3(0.0f, currentAngle, 0.0f);
            //print(currentRotation.eulerAngles);
            //create a bullet clone, and orient it using the current angle
            GameObject bulletClone = Instantiate(bulletObject, transform.position, currentRotation);

            //create second bullet using 180 difference in angle
            currentRotation.eulerAngles = new Vector3(0.0f, currentAngle + 180.0f, 0.0f);
            //print(currentRotation.eulerAngles);
            //create a bullet clone, and orient it using the current angle
            GameObject bulletCloneTwo = Instantiate(bulletObject, transform.position, currentRotation);

            //set last shot fired time to now
            timeLastShotFired = Time.time;
            //increment the number of shots fired
            shotsFiredInSpray++;
            //increase the angle slightly
            currentAngle += angleChangePerShot * rotationDirection;

            print(shotsFiredInSpray);
        }
    }

    //Handles control vars each frame
    private void ControlVars()
    {
        //check if currently can fire bullets
        if (canShootBullet)
        {
            //check if all bullets in a spray have been fired
            if (shotsFiredInSpray == numBulletsPerSpray)
            {
                //stop more bullets from firing
                canShootBullet = false;
                //increase angle in prep of next spray
                currentAngle += angleChangePerSpray * rotationDirection;
            }
        }


        //check if time since last spray began has elasped
        if (Time.time > timeLastSprayFired + timeBetweenSprays)
        {
            //reset shots fired in spray to 0
            shotsFiredInSpray = 0;
            //set can shoot back to true
            canShootBullet = true;
            //set time of last spray beggining to now
            timeLastSprayFired = Time.time;
        }
    }

    //coroutine version of bullet spray
    private IEnumerator BulletSprayRoutine()
    {
        while (true)
        {
            //set time of last spray to now
            timeLastSprayFired = Time.time;

            //for the number of shots in a spray
            for (int i = 0; i < numBulletsPerSpray; i++)
            {
                //create a shot
                //get the current angle as a quaternion
                Quaternion currentRotation = new Quaternion();
                currentRotation.eulerAngles = new Vector3(0.0f, currentAngle, 0.0f);
                //create a bullet clone, and orient it using the current angle
                GameObject bulletClone = Instantiate(bulletObject, transform.position, currentRotation);

                //create second bullet using 180 difference in angle
                currentRotation.eulerAngles = new Vector3(0.0f, currentAngle + 180.0f, 0.0f);
                //create a bullet clone, and orient it using the current angle
                GameObject bulletCloneTwo = Instantiate(bulletObject, transform.position, currentRotation);

                //change the angle between shots
                currentAngle += angleChangePerShot * rotationDirection;

                //wait on self on next bullet delay
                //yield return new WaitForSecondsRealtime(timeBetweenShots);
            }
            //wait for the next spray
            yield return new WaitForSecondsRealtime(timeBetweenSprays);

            //increase angle in prep of next spray
            currentAngle += angleChangePerSpray * rotationDirection;
        }

    }
}
