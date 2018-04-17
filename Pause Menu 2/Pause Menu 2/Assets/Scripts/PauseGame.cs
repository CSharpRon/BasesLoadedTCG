using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class PauseGame : MonoBehaviour 
{

	public Transform canvas;
	public Slider musicVolumeSlider;
	public Button quitButton;

	public AudioSource musicSource;
	public Resolution[] resolutions;
	public GameSettings gameSettings;


	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Pause();
		}
			
		gameSettings = new GameSettings();

		musicVolumeSlider.onValueChanged.AddListener(delegate { OnMuisicVolumeChange(); });
		quitButton.onClick.AddListener (delegate { Quit (); });

	}

	public void  Pause()
	{
		if (canvas.gameObject.activeInHierarchy == false)
		{
			Debug.Log ("Player has Paused Game");
			canvas.gameObject.SetActive(true);
			Time.timeScale = 0;
		} 
		else
		{
			Debug.Log ("Player has Resumed Game");
			canvas.gameObject.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void OnMuisicVolumeChange()
	{
		Debug.Log ("Player has changed volume");
		musicSource.volume = gameSettings.musicVolume = musicVolumeSlider.value;
	}

	public void Quit()
	{
		Debug.Log ("Player has Quit Game");
		Time.timeScale = 1;

		//Load home screen or start menu
		SceneManager.LoadScene ("Start Menu");
	}
}
