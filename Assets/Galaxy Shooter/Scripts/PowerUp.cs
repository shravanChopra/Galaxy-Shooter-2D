using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	[SerializeField] private float speed = 3f;
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate(Vector3.down * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// access the player and enable the tripleShot powerUp
		if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();
	
			if (player != null)
			{
				player.EnableTripleShot();
			}
			// destroy this powerup
			Destroy(gameObject);
		}
	}
}
