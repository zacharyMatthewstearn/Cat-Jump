using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public AudioSource sfxSource;
	public AudioSource musicSource;
	public static SoundManager instance = null;

	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;


	public AudioClip[] JumpSounds;



	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}


	public void PlaySingle (AudioClip clip) {
		sfxSource.clip = clip;
		sfxSource.Play();
	}


	public void RandomizeSFX (params AudioClip[] clips) {
		int randomIndex= Random.Range(0, clips.Length);
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		sfxSource.pitch = randomPitch;
		sfxSource.clip = clips[randomIndex];
		sfxSource.Play();
	}
}
