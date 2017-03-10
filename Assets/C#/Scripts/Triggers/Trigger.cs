using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
	protected GameObject player;

	void Awake() {
		player = PlayerController.instance.gameObject;
	}

	void OnTriggerEnter2D (Collider2D _other) {
		TriggerOnEnter(_other.gameObject);
	}

	public virtual void TriggerOnEnter(GameObject _other) {

	}
}
