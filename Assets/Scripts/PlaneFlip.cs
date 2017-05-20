using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaneFlip : MonoBehaviour {

	Vector3 rotation;
	float rotationSpeed = 90f;

	// Use this for initialization
	void Start () {
		rotation = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		float x = rotation.x;
		float y = rotation.y;
		if(Input.GetKeyDown(KeyCode.Q))
		{
			x += rotationSpeed;
		}
		if(Input.GetKeyDown(KeyCode.C))
		{
			x -= rotationSpeed;
		}
		if(Input.GetKeyDown(KeyCode.E))
		{
			y += rotationSpeed;
		}
		if(Input.GetKeyDown(KeyCode.Z))
		{
			y -= rotationSpeed;
		}

		rotation = new Vector3(x,y,0f);

		transform.DOKill();
		transform.DORotate(rotation, 0.5f, RotateMode.FastBeyond360);
	}
}
