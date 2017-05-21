using UnityEngine;

public class EnemySpawnerRing : MonoBehaviour {

	public int numberOfSpawners;

	public float radius, tiltAngle;

	public Material[] enemyMaterials;

	public EnemySpawner spawnerPrefab;

	void Awake () {
		for (int i = 0; i < numberOfSpawners; i++) {
			CreateSpawner(i);
		}
	}

	void CreateSpawner (int index) {
		Transform rotater = new GameObject("Rotater").transform;
		rotater.SetParent(transform, false);
		rotater.localRotation =
			Quaternion.Euler(0f, index * 360f / numberOfSpawners, 0f);

		EnemySpawner spawner = Instantiate<EnemySpawner>(spawnerPrefab);
		spawner.transform.SetParent(rotater, false);
		spawner.transform.localPosition = new Vector3(0f, 0f, radius);
		spawner.transform.localRotation = Quaternion.Euler(tiltAngle, 0f, 0f);

		spawner.enemyMaterial = enemyMaterials[index % enemyMaterials.Length];
	}
}