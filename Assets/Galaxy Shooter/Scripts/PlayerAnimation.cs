using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isTurningLeft)				
		{
			PlayLeftTurnAnim();
		}
		else if (isTurningRight)					
		{
			PlayRightTurnAnim();
		}
		else											
		{
			PlayIdleAnim();
		}
	}

	private bool isTurningLeft
	{
		get	{ return (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)); }
	}

	private bool isTurningRight
	{
		get { return (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)); }
	}

	private bool isIdle
	{
		get
		{
			return Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)
				|| Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow);
		}
	}
	
	private void PlayLeftTurnAnim()
	{
		anim.SetBool("isTurningLeft", true);
		anim.SetBool("isTurningRight", false);
	}

	private void PlayRightTurnAnim()
	{
		anim.SetBool("isTurningRight", true);			
		anim.SetBool("isTurningLeft", false);
	}

	private void PlayIdleAnim()
	{
		anim.SetBool("isTurningRight", false);			
		anim.SetBool("isTurningLeft", false);
	}
}
