using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (InteractionManager))]
[RequireComponent (typeof (UIManager))]

public class Managers_Transient : MonoBehaviour {
	
	public static InteractionManager interactionManager { get; private set; }
	public static UIManager uiManager { get; private set; }

	private List<IGameManager> startSequence;


	void Awake() {
		interactionManager = GetComponent<InteractionManager>();
		uiManager = GetComponent<UIManager>();
	}


	void Start() {
		startSequence = new List<IGameManager>();
		startSequence.Add(interactionManager);
		startSequence.Add(uiManager);
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
				Debug.Log("Transient manager progress: " + numReady + "/" + numModules);

			yield return null;
		}

		Debug.Log("All transient managers started up");
	}
}
