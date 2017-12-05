using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	// types of audio to play
	public enum GameAudio
	{
		Laser,
		Explosion,
		PowerUp
	};

	// reference to audioSource
	private AudioSource _audioSource;

	// references to all the audio files
	[SerializeField] private AudioClip _laserClip;
	[SerializeField] private AudioClip _explosionClip;
	[SerializeField] private AudioClip _powerUpClip;

	// Use this for initialization
	void Start () 
	{
		_audioSource = GetComponent<AudioSource>();
	}
	
	public void PlayAudio(GameAudio sound)
	{
		switch (sound)
		{
			case GameAudio.Laser: 
				_audioSource.clip = _laserClip;
				break;
			
			case GameAudio.Explosion:
				_audioSource.clip = _explosionClip;
				break;
			
			default:
				_audioSource.clip = _powerUpClip;
				break;
		}

		_audioSource.Play();
	}
}
