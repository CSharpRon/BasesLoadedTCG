using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

//public class Player
//{
//    public string PlayerName;
//    public Sprite PlayerSprite;
//    public string PlayerRarity;
//    public int PlayerStrength;
//    public int PlayerStamina;
//    public int PlayerSpeed;
//    public int PlayerStyle;
//    public int PlayerAccuracy;

//    public Player()
//    {

//    }
//}

public class Collection : MonoBehaviour {

    public List<Player> playerList;
    public Player currentPlayer;
    public int playerIndex;
    public Image pImage;
    public Text nameText;
    public Text rarityText;
    public Text strengthText;
    public Text staminaText;
    public Text speedText;
    public Text styleText;
    public Text accuracyText;

    public void viewMainMenu()
    {
        Debug.Log("viewMainMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public void viewManageTeams()
    {
        Debug.Log("viewManageTeams");
        SceneManager.LoadScene("ManageTeams");
    }

    public void next()
    {
        // Don't ask me why but the index was not being saved between hitting next and prev
        playerIndex = playerList.FindIndex(x => x.PlayerName.Equals(nameText.text));

        Debug.Log("next");
        if (playerIndex + 1 >= playerList.Count)
        {
            playerIndex = 0;
        }
        else
        {
            playerIndex++;
        }

        currentPlayer = playerList[playerIndex];
        update();
    }

    public void previous()
    {

        playerIndex = playerList.FindIndex(x => x.PlayerName.Equals(nameText.text));
        Debug.Log("previous");
        if (playerIndex - 1 < 0)
        {
            playerIndex = playerList.Count - 1;
        }
        else
        {
            playerIndex--;
        }

        currentPlayer = playerList[playerIndex];
        update();
    }

    public void update()
    {
        Debug.Log("update");
        pImage.sprite = currentPlayer.PlayerSprite;
        nameText.text = currentPlayer.PlayerName;
        rarityText.text = "Rarity: " + currentPlayer.PlayerRarity;
        strengthText.text = "Strength: " + currentPlayer.PlayerStrength.ToString();
        staminaText.text = "Stamina: " + currentPlayer.PlayerStamina.ToString();
        speedText.text = "Speed: " + currentPlayer.PlayerSpeed.ToString();
        styleText.text = "Style: " + currentPlayer.PlayerStyle.ToString();
        accuracyText.text = "Accuracy: " + currentPlayer.PlayerAccuracy.ToString();
    }

    // Use this for initialization
    void Start () {
        List<String> team = new List<String>();

        PlayerPrefs.SetString("team_members", "Derek Jeter,Bill Hands,Danny DeVito,Don Mossi,Ed Mathews,Eddie Murray,Mace Windu,Mark McGwire,Ozzie Smith,Palm Tree");

        string members = PlayerPrefs.GetString("team_members");

        if (!String.IsNullOrEmpty(members))
        {
            team = members.Split(',').ToList();
        }

        playerIndex = 0;
        playerList = new List<Player>();

        playerList = ManageTeams.getPlayers(team);
        //playerList = getPlayers(); // database stuff
        currentPlayer = playerList[playerIndex];
        update();
    }
}
