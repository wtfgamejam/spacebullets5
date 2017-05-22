using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour {

	public Explosion[] explosionPrefab;

	void OnEnable()
	{
		Enemy.OnDestroy += SpawnExplosion;
	}

	void OnDisable()
	{
		Enemy.OnDestroy += SpawnExplosion;
	}


	void SpawnExplosion(Vector3 pos)
	{
		Explosion prefab = explosionPrefab[Random.Range(0, explosionPrefab.Length)];
		Explosion spawn = prefab.GetPooledInstance<Explosion>();

		spawn.transform.position = pos;
		spawn.Play();
	}
}
