using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : PooledObject {

	public float MoveSpeed = 15f;
	public Rigidbody Body { get; private set; }

	MeshRenderer[] meshRenderers;

	GameObject player;

	public void SetMaterial (Material m) {
		for (int i = 0; i < meshRenderers.Length; i++) {
			meshRenderers[i].material = m;
		}
	}

	void Awake () {
		Body = GetComponent<Rigidbody>();
		meshRenderers = GetComponentsInChildren<MeshRenderer>();

		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		Body.transform.LookAt(player.transform.position);
		Body.position += transform.forward*MoveSpeed*Time.deltaTime;
	}

	void OnTriggerEnter (Collider enteredCollider) {
		if (enteredCollider.CompareTag("Player")) {
			ReturnToPool();
		}
	}

	void OnParticleCollision (GameObject other)
	{
//		if (col.collider.CompareTag("Kill Zone")) {
//			ReturnToPool();
//		}
		ReturnToPool();
		Debug.Log("hit by bullet");
	}

	void OnLevelWasLoaded () {
		ReturnToPool();
	}
}