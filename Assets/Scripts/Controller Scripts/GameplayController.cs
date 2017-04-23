using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

	public static GameplayController instance;

	[SerializeField]
	GameObject panel;

	[SerializeField]
	Slider airSlider, timeSlider;

	float tick = 5f;

	void Awake() {
		if (instance == null)
			instance = this;
	}

	void Update() {
		if (Time.timeScale == 1f) {

			airSlider.value -= tick * Time.deltaTime;
			timeSlider.value -= tick * Time.deltaTime;

			if (airSlider.value <= 0 || timeSlider.value <= 0) {
				GameOver ();
			}
		}
	}

	public void GameOver() {
		Time.timeScale = 0f;
		panel.SetActive (true);
	}

	public void Resume() {
		Time.timeScale = 1f;
		SceneManager.LoadScene ("Gameplay");
	}

	public void MainMenu() {
		Time.timeScale = 1f;
		SceneManager.LoadScene ("MainMenu");
	}
}
