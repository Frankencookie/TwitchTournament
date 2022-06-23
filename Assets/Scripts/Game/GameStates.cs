using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected float currentTime = 0;
    protected GameSM parentSM;

    public virtual void InitState(GameSM parent)
    {
        InitEventCallbacks();
        parentSM = parent;
    }

    public virtual void DestroyState()
    {
        ClearEventCallbacks();
    }

    public abstract void OnStateBegin();

    public abstract void OnStateUpdate();

    public abstract void OnStateExit();

    protected void InitEventCallbacks()
    {
        Events.OnMessageRecieved += OnMessageRecieved;
        Events.OnCommandRecieved += OnCommandRecieved;
        Events.OnPlayerJoined += OnPlayerJoined;
        Events.OnBattleStart += OnBattleStart;
        InitExtraEvents();
    }

    protected void ClearEventCallbacks()
    {
        Events.OnMessageRecieved -= OnMessageRecieved;
        Events.OnCommandRecieved -= OnCommandRecieved;
        Events.OnPlayerJoined -= OnPlayerJoined;
        Events.OnBattleStart -= OnBattleStart;
        ClearExtraEvents();
    }

    protected virtual void InitExtraEvents()
    {

    }

    protected virtual void ClearExtraEvents()
    {

    }

    public void UpdateTimer()
    {
        currentTime += Time.deltaTime;
    }

    protected virtual void OnMessageRecieved(ChatMessage message)
    {

    }

    protected virtual void OnCommandRecieved(ECommands command, string user, string message)
    {

    }

    protected virtual void OnPlayerJoined(ChatPlayer player)
    {

    }

    protected virtual void OnBattleStart()
    {

    }
}

public class PreparingState : GameState
{
    public override void OnStateBegin()
    {
        Debug.Log(UIManager.instance);
        UIManager.instance.SetUIMode(EUIMode.prepare);
        PlayerManager.ClearCurrentPlayers();
        PlayerManager.ClearAllPlayers();
        Spawner.instance.DestroyAllPlayers();
        PlayerManager.LoadPlayersFromDisk("players");
        Camera.main.transform.position = GameObject.Find("CameraPoint2").transform.position;
        Camera.main.transform.rotation = GameObject.Find("CameraPoint2").transform.rotation;
    }

    public override void OnStateUpdate()
    {
        
    }

    public override void OnStateExit()
    {
        PlayerManager.SavePlayersToDisk("players");
    }

    protected override void OnCommandRecieved(ECommands command, string user, string message)
    {
        switch(command)
        {
            case ECommands.join:
                if(PlayerManager.IsPlayerInGame(user))
                {
                    Debug.Log("Already In Game");
                    return;
                }
                if(PlayerManager.DoesPlayerExist(user))
                {
                    PlayerManager.AddExistingPlayer(user);
                }
                else
                {
                    PlayerManager.AddNewPlayer(user);
                }
                break;
            case ECommands.colour:
                GameCharacter character = Spawner.instance.FindPlayer(user);
                if(character)
                {
                    character.SetColour(Random.ColorHSV(0,1,1,1,1,1));
                }
                break;
            default:
                Debug.Log("Something broke on the Preparing state command interpreter");
                return;
        }
    }

    protected override void OnPlayerJoined(ChatPlayer player)
    {
        
    }

    protected override void OnBattleStart()
    {
        parentSM.ChangeState(new BattleState());
    }
}

public class BattleState : GameState
{
    public override void OnStateBegin()
    {
        UIManager.instance.SetUIMode(EUIMode.battle);
        Camera.main.transform.position = GameObject.Find("CameraPoint1").transform.position;
        Camera.main.transform.rotation = GameObject.Find("CameraPoint1").transform.rotation;
    }

    public override void OnStateUpdate()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    protected override void InitExtraEvents()
    {
        Events.OnPlayerWin += PlayerWon;
    }

    protected override void ClearExtraEvents()
    {
        Events.OnPlayerWin -= PlayerWon;
    }

    void PlayerWon(ChatPlayer player)
    {
        player.AddVictoryScore(10);
        PlayerManager.SavePlayersToDisk("players");
        UIManager.instance.SetUIMode(EUIMode.win);
        UIManager.instance.victoryName.text = player.playerName;
        parentSM.ChangeState(new WinState());
    }
}

public class WinState : GameState
{
    public override void OnStateBegin()
    {
        Events.OnBattleEnd();
    }

    public override void OnStateUpdate()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    protected override void InitExtraEvents()
    {
        Events.OnWinEnd += ContinueClicked;
    }

    protected override void ClearExtraEvents()
    {
        Events.OnWinEnd -= ContinueClicked;
    }

    protected void ContinueClicked()
    {
        parentSM.ChangeState(new PreparingState());
    }

}