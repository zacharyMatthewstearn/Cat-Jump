using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager {
	
	public ManagerStatus status {get; private set;}

	public int livesMax { get; private set; }
	public int livesCurrent { get; private set; }

	public void Startup() {
		Debug.Log("Game manager starting...");

		livesMax = 9;
		ResetLivesCurrent();

		status = ManagerStatus.Started;
	}


	public void ResetLivesCurrent() {
		livesCurrent = livesMax;
	}


	public void DecrementLivesCurrent() {
		livesCurrent--;
	}
}
