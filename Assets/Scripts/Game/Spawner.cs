using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject testObject;
    public List<GameObject> spawnedPlayers = new List<GameObject>();

    public static Spawner instance;

    public GameObject SpawnMarker;
    public GameObject BattleMarker;

    void OnEnable()
    {
        instance = this;
        Events.OnPlayerJoined += SpawnPlayer;
        Events.OnPrepareBegin += DestroyAllPlayers;
        Events.OnBattleStart += MoveToArena;
    }

    void OnDisable()
    {
        Events.OnPlayerJoined -= SpawnPlayer;
        Events.OnPrepareBegin -= DestroyAllPlayers;
        Events.OnBattleStart -= MoveToArena;
    }

    public void SpawnPlayer(ChatPlayer player)
    {
        Debug.Log("Spawning " + player.playerName);
        GameObject character = Instantiate(testObject, new Vector3(SpawnMarker.transform.position.x + Random.Range(-4, 4), 2, SpawnMarker.transform.position.z + Random.Range(-4, 4)), Quaternion.identity);
        spawnedPlayers.Add(character);
        character.GetComponent<GameCharacter>().SetData(player);
        player.AddScore(20);
    }

    public GameCharacter FindPlayer(string name)
    {
        foreach (var item in spawnedPlayers)
        {
            GameCharacter character = item.GetComponent<GameCharacter>();

            if(character.GetName() == name)
            {
                return character;
            }
        }

        return null;
    }

    public void MoveToArena()
    {
        Debug.Log("lol");
        foreach (var item in spawnedPlayers)
        {
            item.GetComponent<NavMeshAgent>().enabled = false;
            item.transform.position = new Vector3(Random.Range(-10, 10), 2, Random.Range(-10, 10));
            item.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    public void DestroyAllPlayers()
    {
        foreach (var item in spawnedPlayers)
        {
            Destroy(item);
        }

        spawnedPlayers.Clear();
    }
}
