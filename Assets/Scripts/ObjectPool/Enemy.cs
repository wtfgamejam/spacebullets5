using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : PooledObject {

	public static event System.Action OnHit = ()=>{};
	public static event System.Action OnHitPlayer = () => {};

	public Material rageMaterial;
	public Material clearMaterial;
	public GameObject rageParticle;
	public GameObject destroyParticle;

	float MoveSpeed = 50f;
	float RageSpeed = 15f;
	public Rigidbody Body { get; private set; }
	BoxCollider col;

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
		col = GetComponent<BoxCollider>();

		player = GameObject.FindGameObjectWithTag("Player");
		plane = GameObject.FindGameObjectWithTag("Plane");
	}

	void OnEnable()
	{
		attack = false;
		rageParticle.SetActive(false);
		destroyParticle.SetActive(false);
		col.enabled = true;
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
			StartCoroutine(PlayDeath());
			OnHitPlayer();
		}
		if (enteredCollider.CompareTag("Plane")) {
			//Debug.Log("Hit plane, ATTACK!");
			attack = true;
			SetMaterial(rageMaterial);
			rageParticle.SetActive(true);
			Body.velocity = Vector3.zero;
		}
	}

	void OnParticleCollision (GameObject other)
	{
		StartCoroutine(PlayDeath());
		Debug.Log("hit by bullet");
		OnHit();
	}

	IEnumerator PlayDeath()
	{
		col.enabled = false;
		rageParticle.SetActive(false);
		attack = false;
		SetMaterial(clearMaterial);
		destroyParticle.transform.SetParent(transform.parent);
		destroyParticle.SetActive(true);

		yield return new WaitForSeconds(1f);

		destroyParticle.transform.SetParent(transform, false);

		ReturnToPool();
	}

	void OnLevelWasLoaded () {
		ReturnToPool();
	}
}