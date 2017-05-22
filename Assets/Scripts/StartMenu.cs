using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;


public class StartMenu : MonoBehaviour {

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
