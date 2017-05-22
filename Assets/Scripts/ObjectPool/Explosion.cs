using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : PooledObject {

	public ParticleSystem explosionParticle;

	public void Play()
	{
		StartCoroutine(PlayExplosion());
	}

	IEnumerator PlayExplosion()
	{
		explosionParticle.Play();
		while(explosionParticle.IsAlive())
		{
			yield return null;
		}

		Debug.Log("Return explosion");
		ReturnToPool();
	}

}
