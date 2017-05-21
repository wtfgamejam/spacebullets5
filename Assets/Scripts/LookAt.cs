using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

	private Vector3 mousePos;
	private Vector3 screenPos;

 	private Vector3 locPos;
 	int dimension = 0;

 	void OnEnable()
 	{
 		PlaneFlip.OnDimensionChange += OnDimensionChange;
 	}

	void OnDisable()
 	{
 		PlaneFlip.OnDimensionChange -= OnDimensionChange;
 	}

 	void OnDimensionChange (int d)
 	{
 		dimension = d;
 	}

	void Update () {
		mousePos = Input.mousePosition;
		float x = mousePos.x;
		float y = mousePos.y;
		float z = 40;

		screenPos = Camera.main.ScreenToWorldPoint( new Vector3(x,y,z) );
		Vector3 shipPos = transform.position;
		if(dimension == 1)
		{
			screenPos = new Vector3(screenPos.z*-1f, screenPos.y, screenPos.x);
			shipPos = new Vector3(shipPos.z*-1f, shipPos.y, shipPos.x);
		}
		else if(dimension == 2)
		{
			screenPos = new Vector3(screenPos.x*-1f, screenPos.y, screenPos.z);
			shipPos = new Vector3(shipPos.x*-1f, shipPos.y, shipPos.z);
		}
		else if(dimension == 3)
		{
			screenPos = new Vector3(screenPos.z, screenPos.y, screenPos.x*-1f);
			shipPos = new Vector3(shipPos.z, shipPos.y, shipPos.x*-1f);
		}

		locPos = transform.InverseTransformPoint(screenPos);

		float degrees = Mathf.Atan2((screenPos.y - shipPos.y), (screenPos.x - shipPos.x))*Mathf.Rad2Deg;
		Vector3 eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, degrees);
		Quaternion q = new Quaternion();
		q.eulerAngles = eulerAngles;
		transform.rotation = q;

		//Debug.Log("mousePos: "+mousePos.ToString()+" screenPos: "+ screenPos.ToString()+" world: "+degrees.ToString());
	}
}
