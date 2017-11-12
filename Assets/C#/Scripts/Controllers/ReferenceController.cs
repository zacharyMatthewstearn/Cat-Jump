using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (InputManager))]
[RequireComponent (typeof (UIManager))]
[RequireComponent (typeof (InteractionManager))]

public class ReferenceController : MonoBehaviour {

	public static InputManager inputManager { get; private set; }
	public static UIManager uiManager { get; private set; }
	public static InteractionManager interactionManager { get; private set; }
	public static GameController gameController { get; private set; }
	public static AudioController audioController { get; private set; }

	private List<IGameManager> startSequence;

	private bool firstSceneLoad = true; 


	void Awake() {
		GameObject temp = null;

		temp = GameObject.Find("AudioController");
		if(temp) {
			audioController = temp.GetComponent<AudioController>();
			temp = null;
		}

		temp = GameObject.Find("GameController");
		if(temp) {
			gameController = temp.GetComponent<GameController>();
			temp = null;
		}
	}


	void Start() {
		StartCoroutine(StartupManagers());
		firstSceneLoad = false;
	}


	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}


	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}


	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if(!firstSceneLoad)
			StartCoroutine(StartupManagers());
	}


	private IEnumerator StartupManagers() {

		inputManager = GetComponent<InputManager>();
		uiManager = GetComponent<UIManager>();
		interactionManager = GetComponent<InteractionManager>();

		startSequence = new List<IGameManager>();
		startSequence.Add(inputManager);
		startSequence.Add(uiManager);
		startSequence.Add(interactionManager);

		foreach (IGameManager manager in startSequence) {
			manager.Startup();
		}

		yield return null;
	}
}
