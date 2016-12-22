using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {
	public int sceneToLoad = 0;

	void OnTriggerEnter2D (Collider2D other) {
		SceneManager.LoadScene (sceneToLoad);
	}
}
