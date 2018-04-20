using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class OptionMenu : MonoBehaviour
{
    public Transform canvas;

    [SerializeField]
    public Slider textSizeSlider;
    public Button quitButton;

    public AudioSource musicSource;
    public Resolution[] resolutions;
    public GameSettings gameSettings;


    public void Return() 
    {
        Debug.Log("Player has returned to Main menu");

        DatabaseManager dm = DatabaseManager.mInstance;
        dm.setFontSize(PlayerPrefs.GetInt("font_size"));

        PlayerPrefs.Save();

        //Load home screen or start menu
        SceneManager.LoadScene("MainMenu");
    }

    public void OnTextSizeChange() 
    {
        Debug.Log("Player has changed text size");

        int size = (int)(textSizeSlider.value*47.5);

        PlayerPrefs.SetInt("font_size", size);

        AppHelper.updateFontSize();
    }

}
