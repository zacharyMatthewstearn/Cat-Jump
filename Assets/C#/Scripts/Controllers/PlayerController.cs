using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController : MonoBehaviour {
	
	public static PlayerController instance = null;

	private Rigidbody2D rb2d = null;
	private Animator animator = null;
	private SpriteRenderer spriteRenderer = null;
	private CapsuleCollider2D cc2d = null;

	private GameManager gameManager = null;
	private ScenesManager scenesManager = null;
	private SoundManager soundManager = null;
	private UIManager uiManager = null;

	private CameraController cameraController = null;
	private Vector2 respawnPosition = Vector2.zero;

	[SerializeField] private Transform groundCheck;
	[SerializeField] private Transform blockCheck_top;
	[SerializeField] private Transform blockCheck_bottom;
	[SerializeField] private ParticleSystem spawnParticles;
	[SerializeField] private LayerMask whatIsGround;

	[SerializeField] private float accelerationRate = 24f;
	[SerializeField] private float decelerationRate_default = 192f;
	[SerializeField] private float decelerationRate_falling = 48f;
	[SerializeField] private float decelerationRate_sliding = 24f;
	[SerializeField] private float maxSpeed = 6f;
	[SerializeField] private float jumpSpeed = 12f;
	[SerializeField] private float slideSpeedMultiplier = 1.5f;
	[SerializeField] private float overlapCircleRadius = 0.1f;
	[SerializeField] private float stopRunningThreshold = 0.5f;
	[SerializeField] private float stopSlidingThreshold = 0.1f;
	[SerializeField] private float respawnTimeBuffer = 0.2f;

	private bool facingRight = true;
	private bool hasPlayedThroughSlideAnimationOnce = false;
	private bool grounded = false;
	private bool blocked = false;
	private bool sliding = false;
	private float speed = 0f;

	public bool Respawning { get; private set; }
	public bool Grounded {
		get {
			return grounded;
		}
		private set {
			grounded = value;
			if(!grounded)
				Sliding = false;
		}
	}
	public bool Blocked {
		get {
			return this;
		}
		private set {
			blocked = value;
			if(blocked) {
				Speed = 0f;
			}
		}
              	}
	public bool Sliding {
		get {
			return sliding;
		}
		set {
			sliding = value;
			animator.SetBool("Sliding", sliding);
			if(!sliding) {
				hasPlayedThroughSlideAnimationOnce = false;
			}
		}
	}
	public float Speed {
		get {
			return speed;
		}
		set {
			speed = value;
			if(Mathf.Abs(speed) < stopSlidingThreshold && hasPlayedThroughSlideAnimationOnce)
				Sliding = false;
		}
	}


	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}


	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator> ();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		cc2d = gameObject.GetComponent<CapsuleCollider2D>();

		GameObject mainCamera = GameObject.Find("Main Camera");
		if(mainCamera)
			cameraController = mainCamera.GetComponent<CameraController>();

		gameManager = Managers_Persistent.gameManager;
		scenesManager = Managers_Persistent.scenesManager;
		soundManager = Managers_Persistent.soundManager;
		uiManager = Managers_Transient.uiManager;

		if (scenesManager)
			if (scenesManager.spawnPoint)
				respawnPosition = scenesManager.spawnPoint.transform.position;
	}

		
	void FixedUpdate () {
		if(!Respawning) {
			Grounded = Physics2D.OverlapCircle (groundCheck.position, overlapCircleRadius, whatIsGround);
		}

		if(Physics2D.OverlapCircle(blockCheck_bottom.position, overlapCircleRadius, whatIsGround) || Physics2D.OverlapCircle(blockCheck_top.position, overlapCircleRadius, whatIsGround)) {
			Blocked = true;
		}
		else
			Blocked = false;

		animator.SetBool ("Ground", Grounded);
		animator.SetFloat ("vSpeed", rb2d.velocity.y);
	}


	public void Move(int _source) {
		if ((_source > 0 && !facingRight) || (_source < 0 && facingRight))
			Flip ();

		switch(_source) {
		case -1:
			Accelerate(false);
			break;
		case 1:
			Accelerate(true);
			break;
		case -2:
			StartCoroutine(Slide(false));
			break;
		case 2:
			StartCoroutine(Slide(true));
			break;
		case 0:
			if(Grounded) {
				if(Sliding)
					Decelerate(decelerationRate_sliding);
				else
					Decelerate(decelerationRate_default);
			}
			else
				Decelerate(decelerationRate_falling);
			break;
		default:
			Debug.Log("Move() recieved invalid argument for 'int _source' == " + _source);
			break;
		}

		rb2d.velocity = new Vector2(Speed, rb2d.velocity.y);
		animator.SetFloat ("Speed", Mathf.Abs(Speed));
	}


	public void Jump() {
		if(Grounded) {
			Grounded = false;
			animator.SetBool ("Ground", false); // Duplicated for snappy responsiveness
			if (rb2d.velocity.y <= 0)
				soundManager.PlayRandomizedSFX(soundManager.GetJumpSounds());
			rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
		}
	}


	public void Die (bool _leftBodyBehind) {
		if(gameManager.livesCurrent > 1) {
			gameManager.DecrementLivesCurrent();
			uiManager.UpdateLivesCounter();

			if(gameManager.livesCurrent == 1) {
				uiManager.AddLivesCounterToFlashers();
			}

			Speed = 0;

			StartCoroutine(Respawn(_leftBodyBehind));
		}
		else {
			scenesManager.LoadLoseScreen();
		}
	}


	private void Accelerate(bool _goRight) {
		if(_goRight) {
			Speed += accelerationRate * Time.deltaTime;
			if (Speed > maxSpeed)
				Speed = maxSpeed;
		}
		else {
			Speed -= accelerationRate * Time.deltaTime;
			if (Speed < -maxSpeed)
				Speed = -maxSpeed;
		}
		Sliding = false;
	}


	private void Decelerate(float _rate) {
		if(Speed > 0) {
			Speed -= _rate * Time.deltaTime;
			if ((!Sliding && Speed < stopRunningThreshold) || (Sliding && Speed < stopSlidingThreshold))
				Speed = 0;
		}
		else if (Speed < 0) {
			Speed += _rate * Time.deltaTime;
			if ((!Sliding && Speed > -stopRunningThreshold) || (Sliding && Speed > -stopSlidingThreshold))
				Speed = 0;
		}
	}


	private void Flip() {
		facingRight = !facingRight;
		Vector3 localScale = transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}


	public void FlagCompletionOfFirstSlideAnim() {
		hasPlayedThroughSlideAnimationOnce = true;
	}


	public IEnumerator Slide (bool _right) {
		Sliding = true;

		Speed = maxSpeed * slideSpeedMultiplier;
		if(!_right)
			Speed *= -1;

		while(!hasPlayedThroughSlideAnimationOnce) {
			yield return null;
		}

		Speed = speed; // recheck to end slide after first time through anim
	}


	private IEnumerator Respawn(bool _leftBodyBehind) {
		Respawning = true;

		spriteRenderer.enabled = false;

		if(_leftBodyBehind) {
			cc2d.enabled = false;

			GameObject playerBody = (GameObject) Instantiate(Resources.Load("Dead Player"));
			playerBody.transform.position = transform.position;

			yield return new WaitForSeconds(respawnTimeBuffer);

			transform.position = respawnPosition;
			cc2d.enabled = true;
		}
		else
			transform.position = respawnPosition;

		yield return new WaitForSeconds(respawnTimeBuffer);

		while(cameraController.isMoving) {
			yield return null;
		}

		spawnParticles.Play();
		spriteRenderer.enabled = true;

		Respawning = false;
	}

}
