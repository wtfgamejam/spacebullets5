using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

	private Vector3 mousePos;
 	private Vector3 screenPos;

	void Update () {
		mousePos = Input.mousePosition;
		screenPos = Camera.main.ScreenToWorldPoint( new Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z) );

		float degrees = Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x))*Mathf.Rad2Deg;
		Vector3 eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, degrees);
		Quaternion q = new Quaternion();
		q.eulerAngles = eulerAngles;
		transform.rotation = q;
	}
}
