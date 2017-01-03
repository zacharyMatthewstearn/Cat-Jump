using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	private Transform playerTransform = null;
	private float playerXPos = 0f;
	private float playerYPos = 0f;
	private float thisXPos = 0f;
	private float thisYPos = 0f;
	public float followThreshholdX = 0f;
	public float followThreshholdY = 0f;
	public float lerpSpeed = 0.1f;
	public float leftMostEdge = 0f;
	public float rightMostEdge = 0f;
	public float topMostEdge = 0f;
	public float bottomMostEdge = 0f;

	void Start () {
		playerTransform = GameObject.Find ("Player").transform;
	}

	void FixedUpdate () {
		playerXPos = playerTransform.position.x;
		playerYPos = playerTransform.position.y;
		thisXPos = transform.position.x;
		thisYPos = transform.position.y;

		if (thisXPos > leftMostEdge || thisXPos < rightMostEdge) {
			if(Mathf.Abs(playerXPos - thisXPos) >= followThreshholdX) {
				transform.position = Vector3.Lerp (transform.position, new Vector3 (playerXPos, transform.position.y, transform.position.z), lerpSpeed);
				if (transform.position.x < leftMostEdge) {
					transform.position = new Vector3 (leftMostEdge, transform.position.y, transform.position.z);
				}
				else if (transform.position.x > rightMostEdge) {
					transform.position = new Vector3 (rightMostEdge, transform.position.y, transform.position.z);
				}
			}
		}

		if (thisYPos > bottomMostEdge || thisYPos < topMostEdge) {
			if(Mathf.Abs(playerYPos - thisYPos) >= followThreshholdY) {
				transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x, playerYPos, transform.position.z), lerpSpeed);
				if (transform.position.y < bottomMostEdge) {
					transform.position = new Vector3 (transform.position.x, bottomMostEdge, transform.position.z);
				}
				else if (transform.position.y > topMostEdge) {
					transform.position = new Vector3 (transform.position.x, topMostEdge, transform.position.z);
				}
			}
		}
	}
}
