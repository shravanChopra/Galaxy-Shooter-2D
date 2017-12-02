using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	[SerializeField] private float speed = 3f;
	[SerializeField] private int powerupId;			// 0: tripleShot, 1: speedBoost, 2: shield

	// Update is called once per frame
	void Update ()
	{
		transform.Translate(Vector3.down * speed * Time.deltaTime);

		if (transform.position.y < -6.4)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// access the player and enable the tripleShot powerUp
		if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();
	
			if (player != null)
			{
				switch(powerupId)
				{
					case 0: 
						player.EnableTripleShot();
						break;
					case 1: 
						player.EnableSpeedBoost();
						break;
					case 2:
						player.EnableShield();
						break;
				}
			}
			// destroy this powerup
			Destroy(gameObject);
		}
	}
}
