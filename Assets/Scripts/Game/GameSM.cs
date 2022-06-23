using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSM
{
    protected GameState currentState;

    public GameSM(GameState startState = null)
    {
        if(startState != null)
        {
            ChangeState(startState);
        }
    }

    public void UpdateState()
    {
        if(currentState != null)
        {
            currentState.OnStateUpdate();
            currentState.UpdateTimer();
        }
    }

    public void ChangeState(GameState newState, bool callExit = true)
    {
        if(currentState != null && callExit)
        {
            currentState.OnStateExit();
            currentState.DestroyState();
        }

        currentState = newState;
        newState.InitState(this);
        newState.OnStateBegin();
    }
}
