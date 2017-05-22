using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 50f;
	public Boundary boundary;
	public ParticleSystem gun;
	public float cooldownTime = 0.1f;

	float timeBetweenFire = 0f;
	Rigidbody body;
	AudioSource audioSource;

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		timeBetweenFire = 0f;

		body = GetComponent<Rigidbody>();

		float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
		Vector3 pMin = transform.InverseTransformPoint( Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance)) );
		Vector3 pMax = transform.InverseTransformPoint( Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance)) );
		boundary.xMin = pMin.x;
		boundary.xMax = pMax.x;
		boundary.yMin = pMin.y;
		boundary.yMax = pMax.y;
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		var inputDevice = InputManager.ActiveDevice;

		if(inputDevice == null)
		{
			moveHorizontal = inputDevice.LeftStickX;
			moveVertical = inputDevice.LeftStickY;
		}

		Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
		Vector3 locVel = movement * moveSpeed;
	   	body.velocity = transform.TransformDirection(locVel);
		//body.velocity = movement * moveSpeed;

		transform.localPosition = new Vector3
		(
			Mathf.Clamp(transform.localPosition.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp(transform.localPosition.y, boundary.yMin, boundary.yMax),
			transform.localPosition.z
		);
	}

	void Update()
	{
		var inputDevice = InputManager.ActiveDevice;
		if((Input.GetButton("Fire1") || inputDevice.RightTrigger.IsPressed)  && timeBetweenFire >= cooldownTime)
		{
			timeBetweenFire = 0f;
			gun.Emit(1);
			audioSource.Play();
		}

		timeBetweenFire += Time.deltaTime; 
	}	
}
