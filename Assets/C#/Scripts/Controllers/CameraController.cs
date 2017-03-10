using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Transform playerTransform = null;
	private Vector3 thisPosPrev = Vector3.zero;

	private float playerXPos = 0f;
	private float playerYPos = 0f;
	private float thisXPos = 0f;
	private float thisYPos = 0f;

	[SerializeField] private float lerpSpeed = 3f;
	[SerializeField] private float followThreshholdX = 0f;
	[SerializeField] private float followThreshholdY = 0f;
	[SerializeField] private float stillnessThreshold = 0.5f;
	[SerializeField] private float leftMostEdge = 0f;
	[SerializeField] private float rightMostEdge = 0f;
	[SerializeField] private float topMostEdge = 0f;
	[SerializeField] private float bottomMostEdge = 0f;

	public bool isMoving { get; private set; }


	void Awake () {
		isMoving = false;

		GameObject player = PlayerController.instance.gameObject;
		if(player)
			playerTransform = player.transform;
	}


	void FixedUpdate () {
		thisPosPrev = transform.position;

		playerXPos = playerTransform.position.x;
		playerYPos = playerTransform.position.y;
		thisXPos = transform.position.x;
		thisYPos = transform.position.y;


		if(Mathf.Abs(playerXPos - thisXPos) >= followThreshholdX) {
			transform.position = Vector3.Lerp (transform.position, new Vector3 (playerXPos, transform.position.y, transform.position.z), lerpSpeed * Time.deltaTime);

			if (transform.position.x < leftMostEdge) {
				transform.position = new Vector3 (leftMostEdge, transform.position.y, transform.position.z);
			}
			else if (transform.position.x > rightMostEdge) {
				transform.position = new Vector3 (rightMostEdge, transform.position.y, transform.position.z);
			}
		}

		if(Mathf.Abs(playerYPos - thisYPos) >= followThreshholdY) {
			transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x, playerYPos, transform.position.z), lerpSpeed * Time.deltaTime);

			if (transform.position.y < bottomMostEdge) {
				transform.position = new Vector3 (transform.position.x, bottomMostEdge, transform.position.z);
			}
			else if (transform.position.y > topMostEdge) {
				transform.position = new Vector3 (transform.position.x, topMostEdge, transform.position.z);
			}
		}
			
		// Broken for peak of 'bell curve' -- possibly moot issue
		isMoving = false;
		if(Vector3.Distance(transform.position, thisPosPrev) > stillnessThreshold * Time.deltaTime)
			isMoving = true;
	}
}
