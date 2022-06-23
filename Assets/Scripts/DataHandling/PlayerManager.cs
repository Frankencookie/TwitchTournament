using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerManager : MonoBehaviour
{
    static List<ChatPlayer> allPlayers = new List<ChatPlayer>();

    static List<ChatPlayer> currentPlayers = new List<ChatPlayer>();

    public static ChatPlayer GetPlayer(string name)
    {
        foreach (var item in allPlayers)
        {
            if(item.playerName == name)
            {
                return item;
            }
        }

        return null;
    }

    public void OnEnable()
    {
        Events.OnPlayerDied += PlayerDied;
    }

    public void OnDisable()
    {
        Events.OnPlayerDied -= PlayerDied;
    }

    public static void ClearCurrentPlayers()
    {
        currentPlayers.Clear();
    }

    public static void ClearAllPlayers()
    {
        allPlayers.Clear();
    }

    public static bool DoesPlayerExist(string name)
    {
        return GetPlayer(name) != null;
    }

    public static void AddNPC()
    {
        string name = "NPC_" + NPCNames.instance.names[Random.Range(0, NPCNames.instance.names.Length - 1)];
        if(IsPlayerInGame(name))
        {
            return;
        }
        if(DoesPlayerExist(name))
        {
            AddExistingPlayer(name);
        }
        else
        {
            AddNewPlayer(name);
        }
    }

    public static void AddPlayer(ChatPlayer newPlayer)
    {
        allPlayers.Add(newPlayer);
    }

    public static void AddNewPlayer(string name)
    {
        Debug.Log("NewPlayer");
        ChatPlayer newPlayer = new ChatPlayer(name, Random.ColorHSV(0, 1, 1, 1, 1, 1), 100, 20, 0, 0);
        AddPlayer(newPlayer);
        currentPlayers.Add(newPlayer);
        Events.PlayerJoined(newPlayer);
    }

    public static void AddExistingPlayer(string name)
    {
        if(DoesPlayerExist(name))
        {
            ChatPlayer playerToAdd = GetPlayer(name);
            currentPlayers.Add(playerToAdd);
            Events.PlayerJoined(playerToAdd);
        }
    }

    public static bool IsPlayerInGame(string name)
    {
        foreach (var item in currentPlayers)
        {
            if(item.playerName == name)
            {
                return true;
            }
        }

        return false;
    }

    public static void LoadPlayersFromDisk(string fileName)
    {
        string saveFile = Application.persistentDataPath + "/" + fileName + ".data";

        if(File.Exists(saveFile))
        {
            string contents = File.ReadAllText(saveFile);

            SavablePlayers newPlayers = JsonUtility.FromJson<SavablePlayers>(contents);

            allPlayers.AddRange(newPlayers.players);

            Debug.Log("All players loaded from " + saveFile);
            Debug.Log("Loaded " + allPlayers.Count + " Players");
        }
        else
        {
            Debug.Log("No File Found");
        }
    }

    public static void SavePlayersToDisk(string fileName)
    {
        string saveFile = Application.persistentDataPath + "/" + fileName + ".data";
        Debug.Log("AllPlayers has " + allPlayers.Count);
        SavablePlayers playersToSave = new SavablePlayers();
        playersToSave.players = allPlayers.ToArray();

        string jsonString = JsonUtility.ToJson(playersToSave);

        File.WriteAllText(saveFile, jsonString);

        Debug.Log("All players saved to " + saveFile);
    }

    public static void PlayerDied(ChatPlayer player)
    {
        currentPlayers.Remove(player);
        if(currentPlayers.Count < 2)
        {
            Events.PlayerWon(currentPlayers[0]);
        }
    }
}

[System.Serializable]
public class ChatPlayer
{
    public string playerName;
    public int playerHealth;
    public int playerCoins;
    public int playerWinCoins;
    public int playerScore;
    public Color playerColor;

    public ChatPlayer(string name, Color color, int health = 0, int coins = 0, int winCoins = 0, int score = 0)
    {
        playerName = name;
        playerHealth = health;
        playerCoins = coins;
        playerScore = score;
        playerWinCoins = winCoins;
        playerColor = color;
    }

    public void AddVictoryScore(int amount)
    {
        playerWinCoins += amount;
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
    }

    public void SubtractScore(int amount)
    {
        playerScore -= amount;
    }
}

[System.Serializable]
public class SavablePlayers
{
    public ChatPlayer[] players;
}