using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : PooledObject {

	public static event System.Action OnHit = ()=>{};
	public static event System.Action OnHitPlayer = () => {};
	public static event System.Action<Vector3> OnDestroy = (v) => {};

	public Material rageMaterial;
	public GameObject rageParticle;

	float MoveSpeed = 50f;
	float RageSpeed = 15f;
	public Rigidbody Body { get; private set; }
	BoxCollider col;

	MeshRenderer[] meshRenderers;
	Material prevMaterial;
	Material currentMaterial;

	GameObject player;
	GameObject plane;

	public bool attack=false;

	public void SetMaterial (Material m) {
		if(currentMaterial!=null)prevMaterial = currentMaterial;
		currentMaterial = m;
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
			PlayDeath();
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

	void OnTriggerExit (Collider exitCollider)
	{
		if (exitCollider.CompareTag("Plane")) {
			//Debug.Log("Hit plane, ATTACK!");
			attack = false;
			SetMaterial(prevMaterial);
			rageParticle.SetActive(false);
			Body.velocity = Vector3.zero;
		}
	}

	void OnParticleCollision (GameObject other)
	{
		PlayDeath();
		Debug.Log("hit by bullet");
		OnHit();
	}

	void PlayDeath()
	{
		OnDestroy(transform.position);

		ReturnToPool();
	}

	void OnLevelWasLoaded () {
		ReturnToPool();
	}
}