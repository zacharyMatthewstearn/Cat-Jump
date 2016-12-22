using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputController : MonoBehaviour {
	PlayerController playerController = null;

	Rect panelLeft;
	Rect panelRight;
	Rect panelGesture;

	private bool ready = false;


	void Start () {
		playerController = GameObject.Find ("Player").GetComponent<PlayerController>();

		panelLeft = GameObject.Find ("Panel_Left").GetComponent<RectTransform> ().rect;
		panelRight = GameObject.Find ("Panel_Right").GetComponent<RectTransform> ().rect;
		panelGesture = GameObject.Find ("Panel_Gesture").GetComponent<RectTransform> ().rect;

		panelLeft.position = GameObject.Find ("Panel_Left").transform.position;
		panelRight.position = GameObject.Find ("Panel_Right").transform.position;
		panelGesture.position = GameObject.Find ("Panel_Gesture").transform.position;
	}

	void Update () {
		if (Input.touchCount == 0)
			ready = true;
		if (Input.touchCount > 0 && ready) {
			int touchCount = Input.touchCount;
			bool iShouldMoveLeft = false;
			bool iShouldMoveRight = false;
			if (touchCount > 0) {
				for (int i = 0; i < touchCount; i++) {
					Vector2 touchPos = Input.GetTouch (i).position;
					if (panelLeft.Contains (touchPos)) {
						iShouldMoveLeft = true;
					}
					if (panelRight.Contains (touchPos)) {
						iShouldMoveRight = true;
					}

					if (panelGesture.Contains (touchPos)) {
						playerController.Jump ();
					}
				}
			}
			if (iShouldMoveLeft && !iShouldMoveRight)
				playerController.Move (-1);
			else if (iShouldMoveRight && !iShouldMoveLeft)
				playerController.Move (1);
			else
				playerController.Move (0);
		} else
			playerController.Move (0);
	}
}
