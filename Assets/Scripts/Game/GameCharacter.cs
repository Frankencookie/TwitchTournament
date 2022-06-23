using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCharacter : MonoBehaviour
{
    ChatPlayer characterData;
    public MeshRenderer meshRenderer;
    public TextMeshPro nametag;

    int hitpoints = 100;

    public void SetData(ChatPlayer data)
    {
        characterData = data;
        SetColour(characterData.playerColor);
        SetName(characterData.playerName);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColour(Color colour)
    {
        characterData.playerColor = colour;
        meshRenderer.material.color = colour;
    }

    public void SetName(string name)
    {
        if(nametag != null)
        {
            nametag.text = name;
        }
    }

    public string GetName()
    {
        return characterData.playerName;
    }

    public void DamageCharacter(int amount)
    {
        Debug.Log("Damaged");
        hitpoints -= amount;
        if(hitpoints < 1)
        {
            Die();
        }
    }

    public void Die()
    {
        Events.PlayerDied(characterData);
        Destroy(gameObject);
    }

    public void GenerateRandomPersonality()
    {

    }
}

public struct AiPersonality
{
    float funny;
    float greedy;
    float athletic;
    float unpredictability;

    void RandomisePersonality()
    {
        funny = Random.Range(0,1);
        greedy = Random.Range(0,1);
        athletic = Random.Range(0,1);
        unpredictability = Random.Range(0,1);
    }
}