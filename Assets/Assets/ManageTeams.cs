using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Team
{
    public string TeamName;
    public List<Player> players;
    // put the other team variables here

    public Team(string name)
    {
        this.TeamName = name;
        players = new List<Player>();
    }

    public void addPlayer(Player p)
    {
        if(players.Contains(p))
        {
            Debug.Log("Player already on team");
            // and tell the user 
        }
        else
        {
            players.Add(p);
        }
    }

    public void removePlayer(Player p)
    {
        if(players.Contains(p))
        {
            players.Remove(p);
        }
        else
        {
            Debug.Log("Player already not on team");
            // and tell the user 
        }
    }
}

public class ManageTeams : MonoBehaviour {

    public Dropdown dropdown;
    public Dropdown playerDropdown;
    public Text selectedName;
    public Text newTeamName;
    public List<Team> teamList;
    public List<string> teamNames;
    public Team currentTeam;

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


    public void viewCollection()
    {
        Debug.Log("View collection");
        SceneManager.LoadScene("Collection");
    }

    public void next()
    {
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

    public void addNewTeam()
    {
        Team t = new Team(newTeamName.text);
        teamList.Add(t);
        // update database here

        List<string> name = new List<string>() { newTeamName.text };
        dropdown.AddOptions(name);
    }

    public void addToTeam()
    {
        currentTeam.addPlayer(currentPlayer);
    }

    public void removeFromTeam()
    {
        currentTeam.removePlayer(currentPlayer);
    }

    public void dropdownIndexChanged(int index)
    {
        selectedName.text = teamNames[index];
        for(int i = 0; i < teamList.Count; i++)
        {
            if(teamList[i].TeamName.Equals(selectedName.text))
            {
                currentTeam = teamList[i];
            }
        }

        playerDropdown.ClearOptions();
        List<string> pNames = new List<string>();
        for (int i = 0; i < currentTeam.players.Count; i++)
        {
            pNames.Add(currentTeam.players[i].PlayerName);
        }
        playerDropdown.AddOptions(pNames);
    }
    
    public void getTeamNames()
    {
        for (int i = 0; i < teamList.Count; i++)
        {
            teamNames.Add(teamList[i].TeamName);
        }
    }

    public void update()
    {
        Debug.Log("update");
        pImage = currentPlayer.PlayerImage;
        nameText.text = currentPlayer.PlayerName;
        rarityText.text = "Rarity: " + currentPlayer.PlayerRarity;
        strengthText.text = "Strength: " + currentPlayer.PlayerStrength.ToString();
        staminaText.text = "Stamina: " + currentPlayer.PlayerStamina.ToString();
        speedText.text = "Speed: " + currentPlayer.PlayerSpeed.ToString();
        styleText.text = "Style: " + currentPlayer.PlayerStyle.ToString();
        accuracyText.text = "Accuracy: " + currentPlayer.PlayerAccuracy.ToString();
    }

    // Use this for initialization
    void Start()
    {
        playerIndex = 0;
        teamNames = new List<string>();
        playerList = new List<Player>();
        teamList = new List<Team>();
        //playerList = getPlayers(); // database stuff
        //teamList = getTeams(); // database stuff
        getTeamNames();
        dropdown.AddOptions(teamNames);
        currentPlayer = playerList[playerIndex];
        update();
    }
}