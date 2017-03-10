using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	private InteractionManager interactionManager = null;
	private GameObject player = null;


	void Start() {
		interactionManager = Managers_Transient.interactionManager;

		PlayerController playerController = PlayerController.instance;
		if(playerController)
			player = playerController.gameObject;
	}


	void OnTriggerEnter2D (Collider2D _other) {
		if (_other.gameObject == player) {
			interactionManager.SetObjectWithWhichToInteract(this);
		}
	}


	void OnTriggerExit2D (Collider2D _other) {
		if (_other.gameObject == player) {
			interactionManager.SetObjectWithWhichToInteract(null);
		}
	}


	public virtual void Interact() {
		
	}
}
