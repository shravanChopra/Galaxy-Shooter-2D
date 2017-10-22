using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField] private float speed = 5.0f;
	
	// Use this for initialization
	void Start ()
	{
		transform.position = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Movement();
		
	}
	
	private void Movement()
	{
		// enable vertical and horizontal movement through user input
		transform.Translate(Vector3.right * speed * Input.GetAxis("Horizontal") * Time.deltaTime);
		transform.Translate(Vector3.up * speed * Input.GetAxis("Vertical") * Time.deltaTime);

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
}
