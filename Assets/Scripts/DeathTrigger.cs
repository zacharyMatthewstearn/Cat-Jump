using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour {
	public int sceneToLoad = 3;

	void OnTriggerEnter2D (Collider2D other) {
		SceneManager.LoadScene (sceneToLoad);
	}
}
