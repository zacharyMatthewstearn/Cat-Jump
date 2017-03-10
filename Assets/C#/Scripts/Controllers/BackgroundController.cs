using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {
	
	private Transform cameraTransform = null;
//	private float divisorX = 2.56f;
//	private float divisorY = 1.5f;
	private float divisorX = 2f;
	private float divisorY = 2f;


	void Awake () {
		GameObject mainCamera = GameObject.Find("Main Camera");
		if (mainCamera)
			cameraTransform = mainCamera.transform;
	}


	void FixedUpdate () {
		transform.position = new Vector3(cameraTransform.position.x/divisorX, cameraTransform.position.y/divisorY, transform.position.z);
	}
}
