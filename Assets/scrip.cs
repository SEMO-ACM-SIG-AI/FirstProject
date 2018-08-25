using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrip : MonoBehaviour {

	public Animator anim;
	public float speed = 5.0f;
	public float jumpForce = 600.0f;
	public LayerMask ground;
	public Transform groundCheck;

	private float moveHorizontal = 0.0f, moveVertical = 0.0f;
	private Rigidbody2D rb2d;
	private bool onGround = false;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 scale = transform.localScale;
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			anim.Play ("Walk");
			if(scale.x > 0){
				scale.x *= -1;
				transform.localScale = scale;
			}
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			anim.Play ("Walk");
			if(scale.x < 0){
				scale.x *= -1;
				transform.localScale = scale;
			}
		} else if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.RightArrow)) {
			anim.Play ("Idle");
		}
		float velY = rb2d.velocity.y;
		onGround = Physics2D.Linecast (transform.position, groundCheck.position, ground);
		if (onGround && Input.GetKeyDown (KeyCode.UpArrow)) {
			velY = 0f;
			rb2d.AddForce (new Vector2 (0, jumpForce));
		}
	}

	void LateUpdate() {
		moveHorizontal = Input.GetAxis ("Horizontal");
		moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rb2d.AddForce (movement * speed * Time.deltaTime);
	}
}
