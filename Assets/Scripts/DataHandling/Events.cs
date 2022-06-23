using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public delegate void OnMessageRecievedEvent(ChatMessage message);
    public delegate void OnCommandRecievedEvent(ECommands command, string user, string message);
    public delegate void OnPlayerEvent(ChatPlayer player);
    public delegate void OnGameEvent();

    public static OnMessageRecievedEvent OnMessageRecieved;
    public static OnCommandRecievedEvent OnCommandRecieved;

    public static OnPlayerEvent OnPlayerJoined;
    public static OnPlayerEvent OnPlayerWin;
    public static OnPlayerEvent OnPlayerDied;

    public static OnGameEvent OnBattleStart;
    public static OnGameEvent OnBattleEnd;

    public static OnGameEvent OnPrepareBegin;
    public static OnGameEvent OnWinEnd;

    public static void MessageRecieved(ChatMessage message)
    {
        OnMessageRecieved?.Invoke(message);
    }

    public static void CommandRecieved(ECommands command, string user, string message)
    {
        OnCommandRecieved?.Invoke(command, user, message);
    }

    public static void PlayerJoined(ChatPlayer player)
    {
        OnPlayerJoined?.Invoke(player);
    }

    public static void PlayerWon(ChatPlayer winner)
    {
        OnPlayerWin?.Invoke(winner);
    }

    public static void PlayerDied(ChatPlayer player)
    {
        OnPlayerDied?.Invoke(player);
    }

    public static void BattleStart()
    {
        OnBattleStart?.Invoke();
    }

    public static void BattleEnd()
    {
        OnBattleEnd?.Invoke();
    }

    public static void PreparationBegin()
    {
        OnPrepareBegin?.Invoke();
    }

    public static void WinEnd()
    {
        OnWinEnd?.Invoke();
    }
}
