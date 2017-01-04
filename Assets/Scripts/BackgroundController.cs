using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {
	private Transform cameraTransform = null;

	void Start () {
		cameraTransform = GameObject.Find("Main Camera").transform;
	}


	void FixedUpdate () {
		transform.position = new Vector3(cameraTransform.position.x/(2.56f), cameraTransform.position.y/1.5f, transform.position.z);
	}
}
