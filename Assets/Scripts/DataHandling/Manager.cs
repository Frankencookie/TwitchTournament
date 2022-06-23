using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    GameSM gameStateMachine;

    public static Manager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine("CreateSMDelay");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStateMachine != null)
        {
            gameStateMachine.UpdateState();
        }
    }

    IEnumerator CreateSMDelay()
    {
        yield return null;
        gameStateMachine = new GameSM(new PreparingState());
    }

}
