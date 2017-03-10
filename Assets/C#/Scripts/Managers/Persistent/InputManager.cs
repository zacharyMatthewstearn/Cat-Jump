using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IGameManager {
	
	public ManagerStatus status {get; private set;}

	private PlayerController playerController = null;
	private ScenesManager scenesManager = null;

	[SerializeField] private Rect panelLeft;
	[SerializeField] private Rect panelRight;
	[SerializeField] private Rect panelGesture;


//	#if !UNITY_EDITOR

//	private Vector2 touchOrigin = -Vector2.one;
//	private float tapThreshold = 5f;
//	private bool ready = false;

//	#endif



	public void Startup() {
		Debug.Log("Input manager starting...");

		playerController = PlayerController.instance;
		scenesManager = Managers_Persistent.scenesManager;

		if(scenesManager.isGameplayLevel) {
			GameObject tempLeft = GameObject.Find ("Panel_Left");
			GameObject tempRight = GameObject.Find ("Panel_Right");
			GameObject tempGesture = GameObject.Find ("Panel_Gesture");
			RectTransform tempRect = null;

			if(tempLeft) {
				tempRect = tempLeft.GetComponent<RectTransform>();
				if(tempRect)
					panelLeft = tempRect.rect;
				panelLeft.position = tempLeft.transform.position;
			}
			if(tempRight) {
				tempRect = tempRight.GetComponent<RectTransform>();
				if(tempRect)
					panelRight = tempRect.rect;
				panelRight.position = tempRight.transform.position;
			}
			if(tempGesture) {
				tempRect = tempGesture.GetComponent<RectTransform>();
				if(tempRect)
					panelGesture = tempRect.rect;
				panelGesture.position = tempGesture.transform.position;
			}
		}

		status = ManagerStatus.Started;
	}


	void Update () {
		
//		#if UNITY_EDITOR

		if(scenesManager.isGameplayLevel) {
			if(!playerController.Respawning) {
				if (Input.GetButtonDown ("Jump"))
					playerController.Jump ();
				if (Input.GetButtonDown ("Interact"))
					Managers_Transient.interactionManager.AttemptInteraction();
				if (Input.GetButtonDown ("Die"))
					playerController.Die(true);
			
				if ((Input.GetButtonDown("Slide Left") || Input.GetButtonDown("Slide Right")) && playerController.Grounded && !playerController.Sliding) {
					if (Input.GetButtonDown("Slide Right"))
						playerController.Move (2);
					else
						playerController.Move (-2);
				}
				else
					playerController.Move ((int)Input.GetAxisRaw ("Horizontal"));
			}
		}
		else
			if (Input.GetButtonDown ("Interact"))
				scenesManager.LoadLevelByIndex(0);

//		#else

//		if (Input.touchCount == 0)
//			ready = true;
//		if (scenesManager.isGameplayLevel) {
//			if (!playerController.Respawning) {
//				if (Input.touchCount > 0 && ready) {
//					int touchCount = Input.touchCount;
//					bool iShouldMoveLeft = false;
//					bool iShouldMoveRight = false;
//
//					if (touchCount > 0) {
//						for (int i = 0; i < touchCount; i++) {
//							Vector2 touchPos = Input.GetTouch (i).position;
//
//							if (panelLeft.Contains (touchPos))
//								iShouldMoveLeft = true;
//							if (panelRight.Contains (touchPos))
//								iShouldMoveRight = true;
//							if (panelGesture.Contains (touchPos))
//								GestureHandler(Input.touches[i]);
//						}
//					}
//
//					if (iShouldMoveLeft && !iShouldMoveRight)
//						playerController.Move (-1);
//					else if (iShouldMoveRight && !iShouldMoveLeft)
//						playerController.Move (1);
//					else
//						playerController.Move (0);
//				}
//				else
//					playerController.Move (0);
//			}
//		}
//		else if (Input.touchCount > 0 && ready)
//			scenesManager.LoadLevelByIndex(0);
		
//		#endif

	}



//	#if !UNITY_EDITOR

//	private void GestureHandler(Touch _myTouch) {
//		if(_myTouch.phase == TouchPhase.Began) {
//			touchOrigin = _myTouch.position;
//		}
//		else if(_myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0) { // using touchOrigin.x to reset touches (with impossible -1 value)
//			Vector2 touchEnd = _myTouch.position;
//			float xDifferential = touchEnd.x - touchOrigin.x;
//			float yDifferential = touchEnd.y - touchOrigin.y;
//			touchOrigin.x = -1;
//
//			if(Mathf.Abs(xDifferential) > tapThreshold || Mathf.Abs(yDifferential) > tapThreshold) {
//				if(Mathf.Abs(xDifferential) > Mathf.Abs(yDifferential)) {
//					if(xDifferential > 0)
//						playerController.Slide(true);
//					else
//						playerController.Slide(false);
//				}
//				else {
//					if(yDifferential > 0)
//						playerController.Jump();
//					else
//						playerController.Die(true);
//				}
//			}
//			else
//				Managers_Transient.interactionManager.AttemptInteraction();
//		}
//
//	}

//	#endif

}
