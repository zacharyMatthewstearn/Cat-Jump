using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour, IGameManager {
	
	public ManagerStatus status {get; private set;}

	private GameController gameManager = null;

	private Text livesCounter = null;

	[SerializeField] private List<Text> flashers;
	[SerializeField] private float flashSpeed = 0.04f;


	public void Startup() {
//		Debug.Log("UI manager starting...");

		gameManager = ReferenceController.gameController;

		GameObject temp = GameObject.Find("Lives Counter");
		if(temp) {
			livesCounter = temp.GetComponent<Text>();
			UpdateLivesCounter();
		}

		StartCoroutine(Flash());

		status = ManagerStatus.Started;
	}



	private IEnumerator Flash() {
		bool aToB = true;
		float alpha = 1f;

		while(true) {
			if (aToB) {
				alpha -= flashSpeed * Time.deltaTime;
				if (alpha <= 0) {
					alpha = 0;
					aToB = false;
				}
			}
			else {
				alpha += flashSpeed * Time.deltaTime;
				if (alpha >= 1) {
					alpha = 1;
					aToB = true;
				}
			}

			foreach(Text flasher in flashers)
				flasher.color = new Color(flasher.color.r, flasher.color.g, flasher.color.b, alpha);

			yield return null;
		}
	}


	public void UpdateLivesCounter() {
		livesCounter.text = gameManager.livesCurrent.ToString();
	}


	public void AddLivesCounterToFlashers() {
		flashers.Add(livesCounter);
	}

}
