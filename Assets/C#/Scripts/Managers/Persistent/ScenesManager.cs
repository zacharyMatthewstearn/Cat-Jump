using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour, IGameManager {
	
	public ManagerStatus status {get; private set;}

	[SerializeField] private string startScreen;
	[SerializeField] private string winScreen;
	[SerializeField] private string loseScreen;
	[SerializeField] private string[] levels;

	public GameObject spawnPoint { get; private set; }
	public bool isGameplayLevel { get; private set; }

	private PlayerController playerController;
	private GameManager gameManager;
	private int currentSceneIndex;
	private int livesCurrent;


	public void Startup() {
		Debug.Log("Scene manager starting...");

		gameManager = Managers_Persistent.gameManager;

		if(isGameplayLevel)
			PlayerController.instance.gameObject.SetActive(true);
		else
			PlayerController.instance.gameObject.SetActive(false);

		status = ManagerStatus.Started;
	}


	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}


	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}


	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		playerController = PlayerController.instance; // Moved from startup because this may resolve before startup completes, resultiing in null ref
		currentSceneIndex = System.Array.IndexOf(levels, SceneManager.GetActiveScene().name);

		if(currentSceneIndex != -1) {
			spawnPoint = GameObject.Find("Spawn Point");
			if(spawnPoint)
				playerController.transform.position = spawnPoint.transform.position;
			
			playerController.gameObject.SetActive(true);
			isGameplayLevel = true;
		}
		else {
			playerController.gameObject.SetActive(false);
			isGameplayLevel = false;
		}


	}


	public void LoadStartScreen() {
		SceneManager.LoadScene(startScreen);
		gameManager.ResetLivesCurrent();
	}


	public void LoadWinScreen() {
		SceneManager.LoadScene(winScreen);
		gameManager.ResetLivesCurrent();
	}


	public void LoadLoseScreen() {
		SceneManager.LoadScene(loseScreen);
		gameManager.ResetLivesCurrent();
	}


	public void LoadNextLevel() {
		if(currentSceneIndex < levels.Length - 1) {
			SceneManager.LoadScene(levels[currentSceneIndex + 1]);
		}
		else {
			LoadWinScreen();
		}
	}


	public void LoadLevelByIndex(int _index) {
		SceneManager.LoadScene(levels[_index]);
	}
}
