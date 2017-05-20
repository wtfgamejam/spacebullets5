using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaneFlip : MonoBehaviour {

	Vector3 targetAngle;
	Vector3 currentAngle;
	float rotationSpeed = 90f;
	float currentLerpTime = 0f;
	float lerpTime = 0.25f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector3 axis = Vector3.zero;
		if(Input.GetKeyDown(KeyCode.Q))
		{
			targetAngle = new Vector3(0f,targetAngle.y-rotationSpeed,0f);
			currentLerpTime = 0f;
		}
		if(Input.GetKeyDown(KeyCode.E))
		{
			targetAngle = new Vector3(0f,targetAngle.y+rotationSpeed,0f);
			currentLerpTime = 0f;
		}
 
        //increment timer once per frame
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime) {
            currentLerpTime = lerpTime;
        }
 
        //lerp!
		float perc = currentLerpTime / lerpTime;
		currentAngle = new Vector3(
			Mathf.LerpAngle(currentAngle.x, targetAngle.x, perc),
			Mathf.LerpAngle(currentAngle.y, targetAngle.y, perc),
			Mathf.LerpAngle(currentAngle.z, targetAngle.z, perc));
 
        transform.eulerAngles = currentAngle;

		//transform.Rotate(axis, rotationSpeed);
	}
}
