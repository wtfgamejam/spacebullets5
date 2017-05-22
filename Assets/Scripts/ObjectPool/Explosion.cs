using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : PooledObject {

	public ParticleSystem explosionParticle;
	public AudioClip[] explodeSounds;

	AudioSource audioSource;

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void Play()
	{
		StartCoroutine(PlayExplosion());
	}

	IEnumerator PlayExplosion()
	{
		audioSource.PlayOneShot(explodeSounds[Random.Range(0,explodeSounds.Length)]);
		explosionParticle.Play();
		while(explosionParticle.IsAlive())
		{
			yield return null;
		}

		//Debug.Log("Return explosion");
		ReturnToPool();
	}

}
