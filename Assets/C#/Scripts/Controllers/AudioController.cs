using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	[SerializeField] private AudioSource sfxSource;
	[SerializeField] private AudioSource musicSource;

	[SerializeField] private float lowPitchRange = 0.95f;
	[SerializeField] private float highPitchRange = 1.05f;

	[SerializeField] private AudioClip[] jumpSounds;


	void Start() {
		if(!musicIsPlaying)
			PlayMusic();
	}


	public bool musicIsPlaying {
		get {
			if(musicSource == null)
				return false;
			return musicSource.isPlaying;
		}
		private set {}
	}


	public void PlaySFX (AudioClip _clip) {
		sfxSource.clip = _clip;
		sfxSource.Play();
	}


	public void PlayMusic () {
		musicSource.Play();
	}


	public void PlayRandomizedSFX (params AudioClip[] _clips) {
		int randomIndex= Random.Range(0, _clips.Length);
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		sfxSource.pitch = randomPitch;
		sfxSource.clip = _clips[randomIndex];
		sfxSource.Play();
	}


	public AudioClip[] GetJumpSounds() {
		return jumpSounds;
	}
}
