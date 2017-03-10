using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour, IGameManager {
	
	public ManagerStatus status { get; private set; }

	private Interactable objectWithWhichToInteract = null;


	public void Startup() {
		Debug.Log("Interaction manager starting...");

		status = ManagerStatus.Started;
	}


	public bool AttemptInteraction() { // returns bool for future feedback purposes on input for invalid interaction
		if(objectWithWhichToInteract) {
			objectWithWhichToInteract.Interact();
			return true;
		}
		return false;
	}


	public void SetObjectWithWhichToInteract(Interactable _target) {
		objectWithWhichToInteract = _target;
	}
}
