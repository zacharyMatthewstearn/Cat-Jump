using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalFollow : MonoBehaviour {
	private Transform playerTransform = null;
	private float playerXPos = 0f;
	private float thisXPos = 0f;
	public float followThreshhold = 0f;
	public float lerpSpeed = 0.1f;
	public float leftMostEdge = 0f;
	public float rightMostEdge = 0f;

	void Start () {
		playerTransform = GameObject.Find ("Player").transform;
	}

	void FixedUpdate () {
		playerXPos = playerTransform.position.x;
		thisXPos = transform.position.x;

		if (thisXPos > leftMostEdge || thisXPos < rightMostEdge) {
			if(Mathf.Abs(playerXPos - thisXPos) >= followThreshhold) {
				transform.position = Vector3.Lerp (transform.position, new Vector3 (playerXPos, transform.position.y, transform.position.z), lerpSpeed);
				if (transform.position.x < leftMostEdge) {
					transform.position = new Vector3 (leftMostEdge, transform.position.y, transform.position.z);
				}
				else if (transform.position.x > rightMostEdge){
					transform.position = new Vector3 (rightMostEdge, transform.position.y, transform.position.z);
				}
			}
		}
	}
}
