using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Linq;

public class DatabaseManager : MonoBehaviour
{

    public List<User> users = new List<User>();
    public List<Teams> teams = new List<Teams>();

    public static User curUser = null;

    public static DatabaseManager mInstance = null;
    public List<string> visited = new List<string>();

    // When the game starts we want to load in all the players so we can filter through them later
    private void Awake()
    {
        users.Clear();
        teams.Clear();

        if (mInstance == null)
        {
            mInstance = this;
        }
        else if (mInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        // DEBUG

        string loggedInUser = PlayerPrefs.GetString("username");

        if(String.IsNullOrEmpty(loggedInUser))
        {
            loggedInUser = "dCarman";
        }

        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://grandslambaseball2.firebaseio.com/");
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri("https://grandslambaseball2.firebaseio.com/");

        DatabaseManager.mInstance.getPlayers(result =>
        {
            users = result;

            // Get the associated teams after we get the players
            if (!String.IsNullOrEmpty(users[0].teams))
            {
                User usr = users.Where(x => x.userName.Equals(loggedInUser)).First();
                curUser = usr;
                PlayerPrefs.SetString("user_teams", usr.teams);
                PlayerPrefs.SetInt("font_size", usr.prefFontSize);

                string[] temp = usr.teams.Split(',');
                

                List<string> teamNames = new List<string>(temp);

                DatabaseManager.mInstance.GetPlayers(teamNames, r =>
                    {
                        teams = r;
                    });
            }
        });

        PlayerPrefs.Save();
    }

    // Add a user to the database
    public void AddUser(User curUser)
    {
        string playerJSON = JsonUtility.ToJson(curUser);

        // If the user also has a team(s) attached, create new teams
        if (!String.IsNullOrEmpty(curUser.teams))
        {
            foreach (string name in curUser.teams.Split(','))
            {
                AddTeam(new Teams(name));
            }
        }

        FirebaseHelper.SetWithUUIDPlayers(curUser.userName).SetRawJsonValueAsync(playerJSON);
    }

    public void setFontSize(int size)
    {
        User usr = curUser;

        usr.prefFontSize = size;

        FirebaseHelper.Users().Child(PlayerPrefs.GetString("username")).UpdateChildrenAsync(usr.ToDict());
    }

    // Manually add a new team
    public void AddTeam(Teams team)
    {
        string teamJSON = JsonUtility.ToJson(team);

        FirebaseHelper.SetWithUUIDTeams(team.teamname).SetRawJsonValueAsync(teamJSON);
    }

    // Add a new player to the specified team
    public void AddPlayer(string team, string playerName)
    {
        Teams t = teams.Find(x => x.teamname.Equals(team));

        if (String.IsNullOrEmpty(t.players))
        {
            t.players = playerName;
        }
        else
        {
            string[] names = t.players.Split(',');

            // Only add a new player if they don't already exist
            if (!name.Contains(playerName))
            {
                t.players += "," + playerName;
            }
        }

        FirebaseHelper.Teams().Child(team).UpdateChildrenAsync(t.ToDict());
    }

    // Remove a player from the specified team
    public void RemovePlayer(string team, string playerName)
    {
        Teams t = teams.Find(x => x.teamname.Equals(team));

        string[] players = t.players.Split(',');

        players = players.Where(x => !x.Equals(playerName)).ToArray<string>();

        t.players = "";

        foreach(string p in players)
        {
            if (String.IsNullOrEmpty(t.players))
            {
                t.players = p;
            } else
            {
                t.players += "," + p;
            }
        }

        FirebaseHelper.Teams().Child(team).UpdateChildrenAsync(t.ToDict());
    }

    // Get all the players on a particular team
    public void GetPlayers(List<string> team, Action<List<Teams>> completionBlock)
    {
        FirebaseHelper.Teams().GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot teams = task.Result;

            List<Teams> teamList = new List<Teams>();
            Teams nTeam;
            foreach (DataSnapshot t in teams.Children)
            {

                var tDict = (IDictionary<string, object>)t.Value;

                nTeam = new Teams(tDict);

                if (team.Contains(nTeam.teamname))
                {
                    teamList.Add(nTeam);
                }

            }
            completionBlock(teamList);
        });
    }

    // Add a win to a team
    public void AddWin(string team)
    {
        Teams t = teams.Find(x => x.teamname.Equals(team));

        t.wins++;
        FirebaseHelper.Teams().Child(team).UpdateChildrenAsync(t.ToDict());
    }

    // Add a loss for a team. Sucks to be them...
    public void AddLoss(string team)
    {
        Teams t = teams.Find(x => x.teamname.Equals(team));

        t.losses++;

        FirebaseHelper.Teams().Child(team).UpdateChildrenAsync(t.ToDict());
    }

    // Get all the players
    public void getPlayers(Action<List<User>> completionBlock)
    {
        List<User> usrList = new List<User>();
        FirebaseHelper.Users().GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot users = task.Result;

            foreach (DataSnapshot userNode in users.Children)
            {
                var userDict = (IDictionary<string, object>)userNode.Value;

                User newUser = new User(userDict);

                usrList.Add(newUser);
            }
            completionBlock(usrList);
        });
    }
}

public class User
{
    public string userName;
    public string teams;
    public int prefFontSize = 0;

    public User(string username)
    {
        this.userName = username;
    }

    public User(string username, string teams)
    {
        userName = username;
        this.teams = teams;
    }

    public User(IDictionary<string, object> dict)
    {
        this.userName = dict["userName"].ToString();
        this.teams = dict["teams"].ToString();
        this.prefFontSize = Int32.Parse(dict["prefFontSize"].ToString());
    }

    public IDictionary<string, object> ToDict()
    {
        Dictionary<string, object> temp = new Dictionary<string, object>();

        temp["userName"] = userName;
        temp["teams"] = teams;
        temp["prefFontSize"] = prefFontSize;

        return temp;
    }
}

public class Teams
{
    public string teamname;
    public string players;
    public int wins = 0;
    public int losses = 0;

    public Teams(string teamname)
    {
        this.teamname = teamname;
        players = "";
    }

    public Teams(string teamname, string players)
    {
        this.teamname = teamname;
        this.players = players;
    }

    public Teams(IDictionary<string, object> dict)
    {
        this.teamname = dict["teamname"].ToString();
        this.players = dict["players"].ToString();
        Int32.TryParse(dict["wins"].ToString(), out this.wins);
        Int32.TryParse(dict["losses"].ToString(), out this.losses);
    }

    public IDictionary<string, object> ToDict()
    {
        Dictionary<string, object> temp = new Dictionary<string, object>();

        temp["teamname"] = teamname;
        temp["players"] = players;
        temp["wins"] = wins;
        temp["losses"] = losses;

        return temp;
    }
}
