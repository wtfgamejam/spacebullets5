using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaneFlip : MonoBehaviour {

	public static event System.Action<int> OnDimensionChange = (i) => {};

	public int dimension;
	public const int numDimensions = 4;

	Vector3 targetAngle;
	Vector3 currentAngle;
	float rotationSpeed = 90f;
	float currentLerpTime = 0f;
	float lerpTime = 0.25f;

	// Use this for initialization
	void OnEnable () {
		dimension = 0;
	}

	// Update is called once per frame
	void Update () {
		Vector3 axis = Vector3.zero;
		if(Input.GetKeyDown(KeyCode.Q))
		{
			targetAngle = new Vector3(0f,targetAngle.y-rotationSpeed,0f);
			currentLerpTime = 0f;
			ChangeDimension(-1);
		}
		if(Input.GetKeyDown(KeyCode.E))
		{
			targetAngle = new Vector3(0f,targetAngle.y+rotationSpeed,0f);
			currentLerpTime = 0f;
			ChangeDimension(1);
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

	void ChangeDimension(int value)
	{
		dimension += value;
		if(dimension<0) dimension = numDimensions-1;
		else if(dimension>=numDimensions) dimension = 0;

		OnDimensionChange(dimension);

		Debug.Log("Change Dimension to "+dimension.ToString());
	}
}
