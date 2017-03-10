using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_Goal : Interactable {

	public override void Interact() {
		Managers_Persistent.scenesManager.LoadWinScreen();
	}
}
