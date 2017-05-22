using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOver : MonoBehaviour {

	void OnEnable () {
		transform.localPosition = Vector3.zero;

		transform.DOLocalMoveZ(25f, 0.25f).SetEase(Ease.OutBack);
	}
	

	void OnDisable () {
		
	}
}
