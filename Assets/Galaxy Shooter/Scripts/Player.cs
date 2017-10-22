using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField] private float _speed = 5.0f;
	[SerializeField] private float _fireRate = 0.25f;
	private float _nextFireTime = 0.0f;
	[SerializeField] private GameObject _laserPrefab;

	// Use this for initialization
	void Start ()
	{
		transform.position = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		ControlMovement();

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			Shoot();			
		}
	}	

	private void ControlMovement ()
	{
		// enable vertical and horizontal movement through user input
		transform.Translate(Vector3.right * _speed * Input.GetAxis("Horizontal") * Time.deltaTime);
		transform.Translate(Vector3.up * _speed * Input.GetAxis("Vertical") * Time.deltaTime);

		// set x bounds - let's wrap around!
		if (transform.position.x > 9.4f)
		{
			transform.position = new Vector3(-9.4f, transform.position.y, 0);
		}
		else if (transform.position.x < -9.4f)
		{
			transform.position = new Vector3(9.4f, transform.position.y, 0);
		}

		// set y bounds
		if (transform.position.y > 0) 
		{
			transform.position = new Vector3(transform.position.x, 0, 0);
		}
		else if (transform.position.y < -4.2f) 
		{
			transform.position = new Vector3(transform.position.x, -4.2f, transform.position.z);
		}		
	}
	private void Shoot ()
	{
		if (Time.time > _nextFireTime)
		{
			Instantiate(_laserPrefab, transform.position + Vector3.up, Quaternion.identity);
			_nextFireTime = Time.time + _fireRate;
		}	
	}
}
