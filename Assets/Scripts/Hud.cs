using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

	public Text dimension;

	void OnEnable()
	{
		PlaneFlip.OnDimensionChange += OnDimensionChange;
	}

	void OnDisable()
	{
		PlaneFlip.OnDimensionChange -= OnDimensionChange;
	}

	void OnDimensionChange (int value)
	{
		dimension.text = value.ToString();
	}

}
