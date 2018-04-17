using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour {

    public void viewMainMenu()
    {
        Debug.Log("viewMainMenu");
        SceneManager.LoadScene("MainMenu");
    }
}
