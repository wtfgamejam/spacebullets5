using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Hud : MonoBehaviour {
	public static event System.Action OnPlayerDeath = () =>{};

	public Text dimension;
	public Text score;
	public Slider health;
	public bool enableDeath = true;
	public GameObject gameOver;

	int hits;
	float hitPoints;

	void OnEnable()
	{
		gameOver.SetActive(false);
		hitPoints = 1f;
		hits = 0;
		PlaneFlip.OnDimensionChange += OnDimensionChange;
		Enemy.OnHit += OnHit;
		Enemy.OnHitPlayer += OnDamage;
	}

	void OnDisable()
	{
		PlaneFlip.OnDimensionChange -= OnDimensionChange;
		Enemy.OnHit -= OnHit;
		Enemy.OnHitPlayer -= OnDamage;
	}

	void OnDimensionChange (int value)
	{
		dimension.text = value.ToString();
	}

	void OnHit()
	{
		hits++;
		score.text = hits.ToString();
	}

	void OnDamage()
	{
		Camera.main.DOShakePosition(0.25f);
		hitPoints -= 0.05f;
		health.value = hitPoints;
		if(hitPoints <=0 && enableDeath)
		{
			StartCoroutine(GameOver());
		}
	}

	IEnumerator GameOver()
	{
		OnPlayerDeath();
		gameOver.SetActive(true);

		yield return new WaitForSeconds(3f);

		SceneManager.LoadScene("Start");
	}

}
