using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour {

    [SerializeField]
    public Text userName;
    public Text password;

    private void Awake()
    {

    }

    void Start()
    {
        
    }
    
    public void login()
    {
        PlayerPrefs.SetString("username", userName.text);
        SceneManager.LoadScene("MainMenu");
    }
    
    public void register()
    {
        PlayerPrefs.SetString("username", userName.text);
        SceneManager.LoadScene("MainMenu");
    }
}
