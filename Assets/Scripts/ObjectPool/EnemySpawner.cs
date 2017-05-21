using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public FloatRange timeBetweenSpawns, scale, randomVelocity, angularVelocity;

	public float velocity;

	public Material enemyMaterial;

	public Enemy[] enemyPrefabs;

	float timeSinceLastSpawn;
	float currentSpawnDelay;

	void FixedUpdate () {
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn >= currentSpawnDelay) {
			timeSinceLastSpawn -= currentSpawnDelay;
			currentSpawnDelay = timeBetweenSpawns.RandomInRange;
			SpawnEnemy();
		}
	}

	void SpawnEnemy () {
		Enemy prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
		Enemy spawn = prefab.GetPooledInstance<Enemy>();

		spawn.transform.localPosition = transform.position;
		spawn.transform.localScale = Vector3.one * scale.RandomInRange;
		spawn.transform.localRotation = Random.rotation;

		spawn.Body.velocity = transform.up * velocity +
			Random.onUnitSphere * randomVelocity.RandomInRange;
		spawn.Body.angularVelocity =
			Random.onUnitSphere * angularVelocity.RandomInRange;

		spawn.SetMaterial(enemyMaterial);
	}
}