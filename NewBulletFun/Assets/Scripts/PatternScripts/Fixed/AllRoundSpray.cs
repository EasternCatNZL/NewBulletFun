using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllRoundSpray : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1.5f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet Object")]
    public GameObject bulletObject;

    [Header("Angle Control")]
    [Tooltip("Angle change per shot in spray")]
    public float angleChangePerShot = 1.0f;
    [Tooltip("Positive or negative (1 or -1)")]
    [Range(-1, 1)]
    public float rotationDirection = 1.0f;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngleTotal = 0.0f; //the current angle the bullet is angled at in regards to owner

    // Use this for initialization
    void Start () {
        //check if angle change per shot can cleanly divide by 360

        StartCoroutine(BulletSprayRoutine());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //bullet firing coroutine
    private IEnumerator BulletSprayRoutine()
    {
        while (true)
        {
            //get a random starting angle
            float angle = Random.Range(0.0f, 360.0f);
            //reset the angle total
            currentAngleTotal = 0.0f;

            //while current angle total not reached 360, keep spawning bullets
            while(currentAngleTotal < 360.0f)
            {
                //create a shot
                //get the current angle as a quaternion
                Quaternion currentRotation = new Quaternion();
                currentRotation.eulerAngles = new Vector3(0.0f, angle, 0.0f);
                //create a bullet clone, and orient it using the current angle
                GameObject bulletClone = Instantiate(bulletObject, transform.position, currentRotation);

                //change the angle between shots
                angle += angleChangePerShot;
                //add the amount angle changed to current angle total
                currentAngleTotal += angleChangePerShot;
            }

            //wait for next spray
            yield return new WaitForSecondsRealtime(timeBetweenSprays);
        }
        
    }
}
