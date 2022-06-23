using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Commands : MonoBehaviour
{
    public GameObject SpeechBubble;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        Events.OnMessageRecieved += MessageRecieved;
    }

    void OnDisable()
    {
        Events.OnMessageRecieved -= MessageRecieved;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MessageRecieved(ChatMessage newMessage)
    {
        if(newMessage.message.StartsWith("!"))
        {
            Debug.Log("Command Recieved");

            switch(newMessage.message)
            {
                case "!join":
                    Debug.Log("Joining");
                    Events.CommandRecieved(ECommands.join, newMessage.user, newMessage.message);
                    break;
                case "!color":
                case "!colour":
                    Debug.Log("Randomising Colour");
                    Events.CommandRecieved(ECommands.colour, newMessage.user, newMessage.message);
                    break;
                default:
                    Debug.Log("Command Not Recognised");
                    break;
            }

            if(newMessage.message.Contains("!say"))
            {
                string sayMessage = newMessage.message.Substring(4);
                Debug.Log("Saying " + sayMessage);
                Events.CommandRecieved(ECommands.say, newMessage.user, sayMessage);
                if(PlayerManager.IsPlayerInGame(newMessage.user))
                {
                    Vector3 offset = new Vector3(0, 1, 0);
                    GameObject bubble = Instantiate(SpeechBubble, Spawner.instance.FindPlayer(newMessage.user).transform.position + offset, Quaternion.identity);
                    bubble.GetComponentInChildren<TextMeshPro>().text = sayMessage;
                }

            }

        }
    }
}

public enum ECommands
{
    join,
    colour,
    say
}