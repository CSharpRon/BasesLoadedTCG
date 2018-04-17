using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void searchForGame()
    {
        Debug.Log("Search for game");
        SceneManager.LoadScene("Game");
    }

    public void viewCollection()
    {
        Debug.Log("View collection");
        SceneManager.LoadScene("Collection");
    }

    public void viewStore()
    {
        Debug.Log("View store");
        SceneManager.LoadScene("Store");
    }

    public void viewOptions()
    {
        Debug.Log("View Options");
        SceneManager.LoadScene("Options");
    }

    public void exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
