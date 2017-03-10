using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (InputManager))]
[RequireComponent (typeof (ScenesManager))]
[RequireComponent (typeof (SoundManager))]
[RequireComponent (typeof (GameManager))]

public class Managers_Persistent : MonoBehaviour {
	
	public static Managers_Persistent instance = null;

	public static InputManager inputManager { get; private set; }
	public static ScenesManager scenesManager { get; private set; }
	public static SoundManager soundManager { get; private set; }
	public static GameManager gameManager { get; private set; }

	private List<IGameManager> startSequence;


	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		inputManager = GetComponent<InputManager>();
		scenesManager = GetComponent<ScenesManager>();
		soundManager = GetComponent<SoundManager>();
		gameManager = GetComponent<GameManager>();
	}


	void Start() {
		startSequence = new List<IGameManager>();
		startSequence.Add(inputManager);
		startSequence.Add(scenesManager);
		startSequence.Add(soundManager);
		startSequence.Add(gameManager);

		StartCoroutine(StartupManagers());
	}


	private IEnumerator StartupManagers() {
		foreach (IGameManager manager in startSequence) {
			manager.Startup();
		}

		yield return null;

		int numModules = startSequence.Count;
		int numReady = 0;

		while (numReady < numModules) {
			int lastReady = numReady;
			numReady = 0;

			foreach (IGameManager manager in startSequence)
				if (manager.status == ManagerStatus.Started)
					numReady++;

			if (numReady > lastReady)
				Debug.Log("Persistent manager progress: " + numReady + "/" + numModules);
		
			yield return null;
		}

		Debug.Log("All persistent managers started up");
	}
}
