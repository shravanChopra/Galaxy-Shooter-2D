using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// power ups 
	[SerializeField] private bool tripleShotEnabled = false;
	[SerializeField] private bool speedBoostEnabled = false;

	[SerializeField] private float _speed = 5.0f;
	[SerializeField] private float _fireRate = 0.25f;
	
	[SerializeField] private GameObject _laserPrefab;

	private float _nextFireTime = 0.0f;
	
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
		if (speedBoostEnabled)
		{
			_speed *= 1.5f;
		}
		else 
		{
			_speed = 5.0f;
		}
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
			if (tripleShotEnabled)
			{
				// spawn the other two lasers at (+- 0.55, 0.08, 0) relative to current position
				Instantiate(_laserPrefab, transform.position + new Vector3(0.55f, 0.08f, 0f), Quaternion.identity);
				Instantiate(_laserPrefab, transform.position + new Vector3(-0.55f, 0.08f, 0f), Quaternion.identity);
			}

			Instantiate(_laserPrefab, transform.position + Vector3.up, Quaternion.identity);
			_nextFireTime = Time.time + _fireRate;
		}	
	}

	public void EnableTripleShot()
	{
		tripleShotEnabled = true;
		StartCoroutine(TripleShotPowerDownRoutine());
	}

	public void EnableSpeedBoost()
	{
		speedBoostEnabled = true;
		StartCoroutine(SpeedBoostPowerDownRoutine());
	}

	public void EnableShield()
	{
		
	}

	// Cool-down systems for powerUps
	public IEnumerator TripleShotPowerDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		tripleShotEnabled = false;
	}

	public IEnumerator SpeedBoostPowerDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		speedBoostEnabled = false;
	}
}
