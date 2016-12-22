using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	private bool ready = false;
	public int sceneToLoad = 0;

	void Update () {
		if (Input.touchCount == 0)
			ready = true;
		if (Input.touchCount > 0 && ready)
			SceneManager.LoadScene (sceneToLoad);
	}
}
