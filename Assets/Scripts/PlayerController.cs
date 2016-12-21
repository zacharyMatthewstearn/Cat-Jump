using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rb2d = null;
	private Animator animator = null;
	public Transform groundCheck = null;

	private bool facingRight = true;
	private bool grounded = false;
	private float groundRadius = 0.2f;


	public float maxSpeed = 10f;
	public float jumpForce = 20f;
	public LayerMask whatIsGround;

	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator> ();
	}


	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		animator.SetBool ("Ground", grounded);
		animator.SetFloat ("vSpeed", rb2d.velocity.y);

//		float move = Input.GetAxis ("Horizontal");
//		animator.SetFloat ("Speed", Mathf.Abs (move));
//
//		rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);
//		if ((move > 0 && !facingRight) || (move < 0 && facingRight))
//			Flip ();
	}

//	void Update() {
//		if (grounded && Input.GetAxis ("Vertical") > 0) {
//			animator.SetBool ("Ground", false); // Duplicated for snappy responsiveness
//			rb2d.AddForce(new Vector2(0, jumpForce));
//		}
//	}

	public void Move(int source) {
		if(source == -1 || source == 1) {
			rb2d.velocity = new Vector2(source * maxSpeed, rb2d.velocity.y);
			if ((source > 0 && !facingRight) || (source < 0 && facingRight))
				Flip ();
			animator.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x));
		}
		else {
			Debug.Log ("PLayer's 'Move' method recieved a bad value for 'source'!");
		}
	}

	public void Jump() {
		if(grounded) {
			animator.SetBool ("Ground", false); // Duplicated for snappy responsiveness
			rb2d.AddForce(new Vector2(0, jumpForce));
		}
	}

	private void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
