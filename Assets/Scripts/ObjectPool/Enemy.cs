using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : PooledObject {

	public Material rageMaterial;

	float MoveSpeed = 50f;
	float RageSpeed = 15f;
	public Rigidbody Body { get; private set; }

	MeshRenderer[] meshRenderers;

	GameObject player;
	GameObject plane;

	public bool attack=false;

	public void SetMaterial (Material m) {
		for (int i = 0; i < meshRenderers.Length; i++) {
			meshRenderers[i].material = m;
		}
	}

	void Awake () {
		Body = GetComponent<Rigidbody>();
		meshRenderers = GetComponentsInChildren<MeshRenderer>();

		player = GameObject.FindGameObjectWithTag("Player");
		plane = GameObject.FindGameObjectWithTag("Plane");
	}

	void OnEnable()
	{
		attack = false;
	}

	void Update()
	{
		if(attack)
		{
			Body.transform.LookAt(player.transform.position);
			Body.position += transform.forward*RageSpeed*Time.deltaTime;
		}
		else
		{
			Body.transform.LookAt(plane.transform.position);
			Body.position += transform.forward*MoveSpeed*Time.deltaTime;
		}
	}

	void OnTriggerEnter (Collider enteredCollider) {
		if (enteredCollider.CompareTag("Player")) {
			ReturnToPool();
		}
		if (enteredCollider.CompareTag("Plane")) {
			Debug.Log("Hit plane, ATTACK!");
			attack = true;
			SetMaterial(rageMaterial);
			Body.velocity = Vector3.zero;
		}
	}

	void OnParticleCollision (GameObject other)
	{
		ReturnToPool();
		Debug.Log("hit by bullet");
	}

	void OnLevelWasLoaded () {
		ReturnToPool();
	}
}