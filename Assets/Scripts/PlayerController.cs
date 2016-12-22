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
	public LayerMask whatIsGround;

	private float speed = 0f;
	public float accelerationRate = 6f;
	public float decelerationRate = 12f;
	public float maxSpeed = 4f;
	public float jumpSpeed = 6f;


	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator> ();
	}
		
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		animator.SetBool ("Ground", grounded);
		animator.SetFloat ("vSpeed", rb2d.velocity.y);
	}

	public void Move(int source) {
		if(source == -1) {
			speed -= accelerationRate * Time.deltaTime;
			if (speed < -maxSpeed) {
				speed = -maxSpeed;
			}
//			speed = -maxSpeed;
		}
		else if(source == 1) {
			speed += accelerationRate * Time.deltaTime;
			if (speed > maxSpeed) {
				speed = maxSpeed;
			}
//			speed = maxSpeed;
		}
		else {
			if (speed > 0) {
				speed -= decelerationRate * Time.deltaTime;
				if (speed < 0.5f) {
					speed = 0;
				}
			}
			else if (speed < 0) {
				speed += decelerationRate * Time.deltaTime;
				if (speed > -0.5f) {
					speed = 0;
				}
			}
//			speed = 0;
		}

		rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

		if ((source > 0 && !facingRight) || (source < 0 && facingRight))
			Flip ();
		
		animator.SetFloat ("Speed", Mathf.Abs(speed));
	}

	public void Jump() {
		if(grounded) {
			animator.SetBool ("Ground", false); // Duplicated for snappy responsiveness
			rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
		}
	}

	private void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
