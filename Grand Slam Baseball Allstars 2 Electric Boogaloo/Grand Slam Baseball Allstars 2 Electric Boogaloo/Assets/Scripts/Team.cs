using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Team : MonoBehaviour {
	public bool pitching;
	Player[] players;
	public Ball ball;
	public Team otherTeam;
	public int up4bat;
	public int strikes;
	public int outs;
	private bool startRun;
	private bool startTurn;
	public int score;
	public int swaps;

    //[SerializeField]
    //private Text Text2 = null;

	// Use this for initialization
	void Start () {
		up4bat = 0;
		strikes = 0;
		outs = 0;
		score = 0;
		swaps = 0;
		startRun = true;
		startTurn = true;
        List<String> team;

        //get the scripts on the players
        players = this.GetComponentsInChildren<Player>();

        PlayerPrefs.SetString("team_members", "Derek Jeter,Bill Hands,Danny DeVito,Don Mossi,Ed Mathews,Eddie Murray,Mace Windu,Mark McGwire,Ozzie Smith,Palm Tree");

        string members = PlayerPrefs.GetString("team_members");

        if (!String.IsNullOrEmpty(members))
        {
            team = members.Split(',').ToList();
        }
        else
        {
            team = new List<String>
            {
            "Derek Jeter",
            "Bill Hands",
            "Danny DeVito",
            "Don Mossi",
            "Ed Mathews",
            "Eddie Murray",
            "Mace Windu",
            "Mark McGwire",
            "Ozzie Smith",
            "Palm Tree"
            };
        }

        generateTeam(team);

		if (!pitching) {
			players[up4bat].transform.position = new Vector3(-.74f, -7.53f, 0f);
		}
	}

	public void StartTurn(){
		//if this team is pitching
		if (pitching && startTurn) {
			players [0].Pitch ();
			startTurn = false;
		}

		//if this team is batting
		else if (otherTeam.startTurn) {
			otherTeam.players [0].Pitch ();
			otherTeam.startTurn = false;
		}
	}

	void PlayTurn(){
		//if this team is pitching
		if (pitching) {
			if (ball.missed) {

			} else if (ball.hit) {

			}
		}

		//if this team is batting
		else {
			//get the runners ready
			if (startRun) {
				startRun = false;
				for (int i = 0; i < 10; i++) {
					if (players [i].targetBase == 5) {
						score += 1;
						players [i].targetBase = 0;
						players [i].transform.position = players [i].oldPosition;
					}
					else if (players [i].targetBase != 0)
						players [i].safe = false;
				}
			}

			//swing when in range
			if(ball.pitched && ball.transform.position.y <= -7.5)
				players [up4bat].Bat ();
			//if the batter missed
			else if (ball.missed) {
				ball.missed = false;
				//they get a strike
				strikes++;
				ball.transform.position = otherTeam.players [0].oldPosition;
				ball.velocity = Vector3.zero;
				startRun = true;
				//if they get 3 strikes, they get an out
				if (strikes >= 3) {
					strikes = 0;
					outs++;
					//if they get 3 outs, change sides
					if (outs >= 3) {
						outs = 0;
						changeTeams ();
					}
					//if its less than 3 outs, just swap the batter
					else {
						players [up4bat].transform.position = players [up4bat].oldPosition;
						up4bat++;
						players [up4bat].transform.position = new Vector3 (-.74f, -7.53f, 0f);
					}
				}
				otherTeam.startTurn = true;
			//if the batter hits the ball
			} else if (ball.hit) {
				//then everyone whos running needs to take a base
				for (int i = 0; i < 10; i++) {
					//if this player is running
					if (players [i].targetBase != 0) {
						if (!players [i].safe) {
							//if they are trying to get to home plate
							if (players [i].targetBase == 4) {
								//tell them to run there
								players [i].Run (new Vector3 (0f, -7.5f, 0f));
							}
							//if they are trying to get to a numbered base
							else {
								//tell them to go there
								players [i].Run (otherTeam.players [3 + players [i].targetBase].oldPosition);
							}
						} else {

						}
					}
				}
				//and the outfeilders on the other team need to try to get the ball
				otherTeam.players[7].Intercept();
				otherTeam.players[8].Intercept();
				otherTeam.players[9].Intercept();
				//if one of em gets it
				if (ball.caught) {
					//reset the stage, and give an out for each player who isnt safe
					for (int i = 0; i < 10; i++) {
						if (players [i].targetBase != 0 && !players [i].safe) {
							players [i].transform.position = players [i].oldPosition;
							players [i].targetBase = 0;
							players [i].safe = true;
							outs++;
							strikes = 0;
						}
					}
					startRun = true;
					otherTeam.startTurn = true;
					if (outs >= 3) {
						outs = 0;
						changeTeams ();
					} else {
						ball.hit = false;
						ball.caught = false;
						ball.transform.position = ball.oldPosition;

						up4bat++;
						players [up4bat].transform.position = new Vector3 (-.74f, -7.53f, 0f);
					}
				}
			}
		}
	}

	void changeTeams(){
		for (int i = 0; i < 10; i++) {
			players [i].transform.position = otherTeam.players [i].oldPosition;
			otherTeam.players [i].transform.position = players [i].oldPosition;

			players [i].oldPosition = players [i].transform.position;
			otherTeam.players [i].oldPosition = otherTeam.players [i].transform.position;
		}
		otherTeam.pitching = !otherTeam.pitching;
		pitching = !pitching;
		otherTeam.swaps += 1;
		swaps += 1;
		ball.pitched = false;
		ball.passed = false;
		ball.hit = false;
		ball.missed = false;
		ball.caught = false;
		if (pitching) {
			ball.holding = players [0];
			ball.transform.position = players [0].transform.position;
		} else {
			ball.holding = otherTeam.players [0];
			ball.transform.position = otherTeam.players [0].transform.position;
		}
	}

	void changeBatter() {
		//swap the batter's positions
		if (up4bat < 9) {
			up4bat += 1;
			players [up4bat].transform.position = new Vector3(5.9f, -9.57f, 0.0f);
		} else {
			up4bat = 0;
			players [up4bat].transform.position = new Vector3(5.9f, -9.57f, 0.0f);
		}
	}

    void generateTeam(List<String> _players)
    {
        //get the sprite renderers on the players
        SpriteRenderer[] sprites = this.GetComponentsInChildren<SpriteRenderer>();
        string path;

        for(int i = 0; i < players.Count(); i++)
        {
            string file;
            string name = _players[i];
            string folder = name + "/";

            Texture2D thisTexture = new Texture2D(2, 2);

            string ext = ".jpg";
            file = folder + name;

            thisTexture = (Texture2D)Resources.Load("Cards/" + file);

            Sprite newSprite = IMG2Sprite.instance.LoadNewSprite(thisTexture);
            sprites[i].sprite = newSprite;

            TextAsset stats = (TextAsset)Resources.Load("Cards/" + folder + "stats");

            string raw = stats.text;

            string[] text = raw.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
            );

            players[i].name = text[0];
            players[i].PlayerStrength = Int32.Parse(text[2]);
            players[i].PlayerStamina = Int32.Parse(text[3]);
            players[i].PlayerSpeed = Int32.Parse(text[4]);
            players[i].PlayerStyle = Int32.Parse(text[5]);
            players[i].PlayerAccuracy = Int32.Parse(text[6]);
        }
    }

	void generateRandomTeam(){
		//get the sprite renderers on the players
		SpriteRenderer [] sprites = this.GetComponentsInChildren<SpriteRenderer> ();

		//get info on all the cards in the folder
		string path = Application.dataPath + "/Sprites/Cards";
		Debug.Log (path);
		string [] fileInfo = Directory.GetFiles(path);

		//generate 10 random cards
		int [] rands = new int[10];
		int rand = UnityEngine.Random.Range (0,fileInfo.Length);
		for(int i = 0; i < 10; i++)
		{
			//if this card was already picked
			while(rands.Contains(rand) == true)
				//pick another one
				rand = UnityEngine.Random.Range (0,fileInfo.Length);
			rands [i] = rand;

			//get the name of this card from the messy file info
			string name = fileInfo [rand].Remove(0, path.Length +1);
			name = name.Remove (name.Length - 5, 5);
			Debug.Log (name);

			//once we've got the name, get the path for the sprite
			string info = Path.GetFullPath ("Assets\\Sprites\\Cards\\" + name + "\\" + name + ".jpg");
			if (!File.Exists (info)) {
				info = Path.GetFullPath ("Assets\\Sprites\\Cards\\" + name + "\\" + name + ".jpeg");
				if (!File.Exists (info)) {
					info = Path.GetFullPath ("Assets\\Sprites\\Cards\\" + name + "\\" + name + ".png");
					if (!File.Exists (info))
						info = Path.GetFullPath ("Assets\\Sprites\\Cards\\" + name + "\\" + name + ".gif");
				}
			}
			Sprite newSprite = IMG2Sprite.instance.LoadNewSprite (info);
			sprites [i].sprite = newSprite;

			//and the path for the stats
			string infoP = Path.GetFullPath("Assets\\Sprites\\Cards\\" + name + "\\stats.txt");
			string [] text = File.ReadAllLines (infoP);
            players[i].PlayerRarity = text[1];
			players [i].name = text [0];
			players [i].PlayerStrength = Int32.Parse(text [2]);
			players [i].PlayerStamina = Int32.Parse(text [3]);
			players [i].PlayerSpeed = Int32.Parse(text [4]);
			players [i].PlayerStyle = Int32.Parse(text [5]);
			players [i].PlayerAccuracy = Int32.Parse(text [6]);
		}
	}

	void Awake() {
	}
	
	// Update is called once per frame
	void Update () {
		PlayTurn ();
	}
}
