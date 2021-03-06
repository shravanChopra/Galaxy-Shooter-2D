﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	// y-bounds (6.4 and -6.4)
	// x-bounds (7.75 and -7.75)

	[SerializeField] private float _moveSpeed = 2.0f;
	[SerializeField] private GameObject _enemyExplosionPrefab;
	
	private UIManager _uiManager;
	private AudioManager _audioManager;

	// Use this for initialization
	void Start () 
	{
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// move down
		transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);

		// respawn if you're off the screen 
		if (transform.position.y < -6.4)
		{
			// reposition at top of screen with a randomized x position within screen bounds
			transform.position = new Vector3(Random.Range(-7.75f, 7.75f), 6.4f, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		// handle collisions with player
		if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();
			if (player != null)
			{
				player.TakeDamage();
				Explode();
			}
			
		}
		else if (other.tag == "Laser")
		{
			// Destroy both the laser and yourself
			Destroy(other.gameObject);
			Explode();
		}
	}

	private void Explode()
	{
		// play explosion sound
		_audioManager.PlayAudio(AudioManager.GameAudio.Explosion);

		Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
		_uiManager.UpdateScore();
		Destroy(gameObject);
	}

}
