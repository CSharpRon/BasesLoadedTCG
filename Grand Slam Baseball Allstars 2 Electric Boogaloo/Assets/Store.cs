using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using System;

public class Store : MonoBehaviour
{

    public List<PlayerObject> playerList;
    public int playerIndex;
    public Image pImage;
    public Text nameText;
    public Text rarityText;
    public Text strengthText;
    public Text staminaText;
    public Text speedText;
    public Text styleText;
    public Text accuracyText;

    public static PlayerObject[] players;

    public static PlayerObject currentPlayer;

    public void ReturnMainMenu()
    {
        Debug.Log("viewMainMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public void next()
    {
        // Don't ask me why but the index was not being saved between hitting next and prev.
        // This ensures that the playerIndex is always current to whatever player was showing on the screen before "next" was pressed
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

    public void PurchasePack()
    {
        Debug.Log("Player is buying pack");

        int currency = PlayerPrefs.GetInt("currency");

        if (currency >= 100)
        {

        }
        else
        {

        }

        PlayerObject player;
        playerIndex = 0;

        //Loading in the cards
        var cards = Resources.LoadAll("Cards/");

        //get the sprite renderers on the players
        SpriteRenderer[] sprites = this.GetComponentsInChildren<SpriteRenderer>();

        //Initializing rand (rand represents a random card)
        int rand = UnityEngine.Random.Range(0, cards.Length);
        //Do this 5 times since we need 5 random cards
        for (int i = 0; i < 5; i++)
        {
            //Grab a random card
            rand = UnityEngine.Random.Range(0, cards.Length);

            player = new PlayerObject();

            // Find it's name
            string name = Resources.LoadAll("Cards/")[rand].name;

            string folder = name + "/";

            Texture2D thisTexture = new Texture2D(2, 2);

            string file = folder + name;

            //Once we've got the name, get the path for the sprite
            thisTexture = (Texture2D)Resources.Load("Cards/" + file);

            Sprite newSprite = IMG2Sprite.instance.LoadNewSprite(thisTexture);

            //And the path for the stats
            var stats = (TextAsset)Resources.Load("Cards/" + name + "/" + "stats");

            string raw = stats.text;

            // Since we are drawing it from a TextAsset, we cannot use File.ReadLines and have to do a hack to split the string
            string[] text = raw.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
            );

            player.PlayerSprite = newSprite;
            player.PlayerName = text[0];
            player.PlayerRarity = text[1];
            player.PlayerStrength = Int32.Parse(text[2]);
            player.PlayerStamina = Int32.Parse(text[3]);
            player.PlayerSpeed = Int32.Parse(text[4]);
            player.PlayerStyle = Int32.Parse(text[5]);
            player.PlayerAccuracy = Int32.Parse(text[6]);

            //Add the card to the user's list of cards
            playerList.Add(player);


        }

        currentPlayer = playerList[playerIndex];
        //Update the sprites 
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
    void Start()
    {
        playerIndex = 0;
        playerList = new List<PlayerObject>(5);
    }
}
