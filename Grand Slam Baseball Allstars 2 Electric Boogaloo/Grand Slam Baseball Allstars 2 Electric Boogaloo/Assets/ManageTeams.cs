using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class DisplayTeam
{
    public string TeamName;
    public List<Player> players;
    // put the other team variables here

    public DisplayTeam(string name)
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
    public List<DisplayTeam> teamList;
    public List<string> teamNames;
    public DisplayTeam currentTeam;

    public List<Player> playerList;
    public Player currentPlayer;
   
    [SerializeField]
    public int playerIndex;

    [SerializeField]
    public Image pImage;

    [SerializeField]
    public Text nameText;

    [SerializeField]
    public Text rarityText;

    [SerializeField]
    public Text strengthText;

    [SerializeField]
    public Text staminaText;

    [SerializeField]
    public Text speedText;

    [SerializeField]
    public Text styleText;

    [SerializeField]
    public Text accuracyText;

    public int? internalIndex;


    public void viewCollection()
    {
        Debug.Log("View collection");
        SceneManager.LoadScene("Collection");
    }

    public void next()
    {
        // Don't ask me why but the index was not being saved between hitting next and prev
        internalIndex = playerList.FindIndex(x => x.PlayerName.Equals(nameText.text));

        Debug.Log("next");
        if (internalIndex + 1 >= playerList.Count)
        {
            internalIndex = 0;
        }
        else
        {
            internalIndex++;
        }

        currentPlayer = playerList[(int)internalIndex];
        update();
    }

    public void previous()
    {

        internalIndex = playerList.FindIndex(x => x.PlayerName.Equals(nameText.text));
        Debug.Log("previous");
        if (internalIndex - 1 < 0)
        {
            internalIndex = playerList.Count - 1;
        }
        else
        {
            internalIndex--;
        }

        currentPlayer = playerList[(int)internalIndex];
        update();
    }

    public void addNewTeam()
    {
        DisplayTeam t = new DisplayTeam(newTeamName.text);
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
    void Start()
    {
        playerIndex = 0;
        teamNames = new List<string>();
        playerList = new List<Player>();
        teamList = new List<DisplayTeam>();
        playerList = getPlayers(); // database stuff
        //teamList = getTeams(); // database stuff
        getTeamNames();
        dropdown.AddOptions(teamNames);
        currentPlayer = playerList[playerIndex];
        update();
    }

    public List<Player> getPlayers()
    {
        PlayerPrefs.SetString("team_members", "Derek Jeter,Bill Hands,Danny DeVito,Don Mossi,Ed Mathews,Eddie Murray,Mace Windu,Mark McGwire,Ozzie Smith,Palm Tree");

        String members = PlayerPrefs.GetString("team_members");

        List<String> team = members.Split(',').ToList();

        return getPlayers(team);
    }

    public static List<Player> getPlayers(List<String> team) { 
        List<Player> players = new List<Player>();
        

        for (int i = 0; i < team.Count(); i++)
        {
            float PixelsPerUnit = 100.0f;
            Player player = new Player();
            string file;
            string name = team[i];
            string folder = name + "/";

            Texture2D thisTexture = new Texture2D(2, 2);
            Sprite sprite = new Sprite();
            Image img;

            file = folder + name;

            thisTexture = (Texture2D)Resources.Load("Cards/" + file);

            TextAsset stats = (TextAsset)Resources.Load("Cards/" + folder + "stats");

            sprite = Sprite.Create(thisTexture, new Rect(0, 0, thisTexture.width, thisTexture.height), new Vector2(0, 0), PixelsPerUnit);

            string raw = stats.text;

            string[] text = raw.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
            );

            player.PlayerSprite = sprite;
            //player.PlayerImage = img;
            player.PlayerName = text[0];
            player.PlayerStrength = Int32.Parse(text[2]);
            player.PlayerStamina = Int32.Parse(text[3]);
            player.PlayerSpeed = Int32.Parse(text[4]);
            player.PlayerStyle = Int32.Parse(text[5]);
            player.PlayerAccuracy = Int32.Parse(text[6]);
            players.Add(player);
        }

        return players;
    }
}