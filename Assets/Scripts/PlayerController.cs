using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 0.05f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = transform.localPosition;
		float x = direction.x;
		float y = direction.y;
		if(Input.GetKey(KeyCode.W))
		{
			y += moveSpeed;
		}
		if(Input.GetKey(KeyCode.A))
		{
			x -= moveSpeed;
		}
		if(Input.GetKey(KeyCode.S))
		{
			y -= moveSpeed;
		}
		if(Input.GetKey(KeyCode.D))
		{
			x += moveSpeed;
		}

		transform.localPosition = new Vector3(x,y,direction.z);
	}
}
