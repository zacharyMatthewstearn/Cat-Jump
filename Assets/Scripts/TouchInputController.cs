using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputController : MonoBehaviour {
	PlayerController playerController = null;

	Rect panelLeft;
	Rect panelRight;
	Rect panelGesture;

	GameObject M1 = null;
	GameObject M2 = null;
	GameObject M3 = null;


	void Start () {
		playerController = GameObject.Find ("Player").GetComponent<PlayerController>();

		panelLeft = GameObject.Find ("Panel_Left").GetComponent<RectTransform> ().rect;
		panelRight = GameObject.Find ("Panel_Right").GetComponent<RectTransform> ().rect;
		panelGesture = GameObject.Find ("Panel_Gesture").GetComponent<RectTransform> ().rect;

		panelLeft.position = GameObject.Find ("Panel_Left").transform.position;
		panelRight.position = GameObject.Find ("Panel_Right").transform.position;
		panelGesture.position = GameObject.Find ("Panel_Gesture").transform.position;

		M1 = GameObject.Find ("Mushroom_1");
		M2 = GameObject.Find ("Mushroom_2");
		M3 = GameObject.Find ("Mushroom_3");

		M1.SetActive (false);
		M2.SetActive (false);
		M3.SetActive (false);
	}

	void Update () {
		int touchCount = Input.touchCount;
		if (touchCount > 0)
		{
			for(int i = 0; i < touchCount; i++) {
				Vector2 touchPos = Input.GetTouch(i).position;

				if (panelLeft.Contains (touchPos)) {
//					M1.SetActive (true);
					playerController.Move(-1);
				}
//				else {
//					M1.SetActive (false);
//				}

				if (panelRight.Contains(touchPos)) {
//					M2.SetActive (true);
					playerController.Move(1);
				}
//				else {
//					M2.SetActive (false);
//				}

				if (panelGesture.Contains(touchPos)) {
//					M3.SetActive (true);
					playerController.Jump();
				}
//				else {
//					M3.SetActive (false);
//				}
			}
		}

//		if (Input.GetMouseButtonDown(0)) {
//			Vector3 clickPos = (Input.mousePosition);
//			if (panelLeft.Contains(clickPos))
//			{
//				M1.SetActive (!M1.activeSelf);
//			}
//			if (panelRight.Contains(clickPos))
//			{
//				M2.SetActive (!M2.activeSelf);
//			}
//			if (panelGesture.Contains(clickPos))
//			{
//				M3.SetActive (!M3.activeSelf);
//			}
//		}
	}


}
