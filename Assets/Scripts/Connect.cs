using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class Connect : MonoBehaviour
{
#region info
    public string username = "BespingoBot";
    public string password = "oauth:1ua8o3ter6wkdj35mudtctbiuman5u";
    public string channelName = "bespingo";
#endregion
    TcpClient twitchClient;
    StreamReader reader;
    StreamWriter writer;

    float reconnectTimer;
    float reconnectAfter = 5.0f;

    public static Connect instance;
    public GameObject errorObject;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;

            ConnectToTwitch();
        }
        else
        {
            Destroy(this);
        }
    }

    void OnDisable()
    {
        twitchClient.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if(twitchClient.Connected == false)
        {
            Debug.Log("Lost Connection");
            reconnectTimer += Time.deltaTime;
            if(reconnectTimer > reconnectAfter)
            {
                ConnectToTwitch();
                reconnectTimer = 0;
            }
        }
    }

    void ConnectToTwitch()
    {
        if(username == "" || password == "" || channelName == "")
        {
            errorObject.SetActive(true);
            return;
        }
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());
        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("JOIN #" + channelName);

        writer.Flush();
        Debug.Log("Connected");
    }

    public StreamData GetTwitchData()
    {
        StreamData data = new StreamData();
        data.twitchClient = twitchClient;
        data.reader = reader;
        data.writer = writer;
        return data;
    }
}

public struct StreamData
{
    public TcpClient twitchClient;
    public StreamReader reader;
    public StreamWriter writer;
}