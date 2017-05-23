using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InControl;


public class StartMenu : MonoBehaviour {

	public Text highScore;
	
	void Start()
	{
		if (!GameObject.Find("AudioManager"))
		{
			GameObject mainAudio = new GameObject();
			mainAudio.name = "AudioManager";
			mainAudio.transform.SetParent(transform.parent);
			AudioSource audio = mainAudio.AddComponent<AudioSource>();

			AudioClip main = Resources.Load<AudioClip>("Audio/mainTheme");
			audio.clip = main;
			audio.loop = true;
			audio.Play();
			DontDestroyOnLoad(mainAudio);
		}

		int score = PlayerPrefs.GetInt("Score", 0);
		if(score == 0) highScore.transform.parent.gameObject.SetActive(false);
		else
		{
			highScore.text = score.ToString();
		}
	}

	void Update()
	{
		var inputDevice = InputManager.ActiveDevice;
		if(inputDevice.AnyButtonIsPressed)
		{
			StartGame();
		}
	}

	public void StartGame()
	{
		SceneManager.LoadScene("Main");
	}
}
