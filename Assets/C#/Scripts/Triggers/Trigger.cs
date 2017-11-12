using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
	protected GameObject player;
	protected PlayerController playerController;

	void Awake() {
		player = GameObject.Find("Player");
		if(player != null)
			playerController = player.GetComponent<PlayerController>();
	}

	void OnTriggerEnter2D (Collider2D _other) {
		TriggerOnEnter(_other.gameObject);
	}

	public virtual void TriggerOnEnter(GameObject _other) {

	}
}
