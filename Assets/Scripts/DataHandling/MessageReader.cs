using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using UnityEngine;

public class MessageReader : MonoBehaviour
{
    TcpClient twitchChat;
    StreamReader reader;
    StreamWriter writer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(twitchChat == null || twitchChat.Available == 0)
        {
            GetData();
            return;
        }
        if(twitchChat.Available > 0)
        {
            ReadChat();
        }
    }

    public void ReadChat()
    {
        string message = reader.ReadLine();
        if(message.StartsWith("PING"))
        {
            Debug.Log("Pinged, ponging");
            writer.WriteLine("PONG");
        }
        else if(message.Contains("PRIVMSG"))
        {
            ChatMessage cMessage = new ChatMessage();

            //Get username
            int splitPoint = message.IndexOf("!", 1);
            string chatName = message.Substring(0, splitPoint); 
            chatName = chatName.Substring(1);
            cMessage.user = chatName;

            //Get Message
            splitPoint = message.IndexOf(":", 1); 
            message = message.Substring(splitPoint + 1);
            cMessage.message = message.ToLower();

            Debug.Log("Username is " + cMessage.user);
            Debug.Log("Message is " + cMessage.message);

            Events.MessageRecieved(cMessage);
        }
    }

    public void GetData()
    {
        StreamData data = Connect.instance.GetTwitchData();

        twitchChat = data.twitchClient;
        reader = data.reader;
        writer = data.writer;
    }
}
