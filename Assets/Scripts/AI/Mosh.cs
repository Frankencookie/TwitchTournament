using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mosh : MonoBehaviour
{
    public GameCharacter character;
    public NavMeshAgent agent;

    void OnEnable()
    {
        Events.OnBattleStart += StartMoshing;
        Events.OnBattleEnd += StopMoshing;
    }

    void OnDisable()
    {
        Events.OnBattleStart -= StartMoshing;
        Events.OnBattleEnd -= StopMoshing;
        StopAllCoroutines();
    }

    public void StartMoshing()
    {
        StartCoroutine("RepositionDelay");
    }

    public void StopMoshing()
    {
        StopAllCoroutines();
    }

    IEnumerator RepositionDelay()
    {
        while(true)
        {
            agent.SetDestination(new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)));
            yield return new WaitForSeconds(Random.Range(0, 3));
            character.DamageCharacter(10);
        }
    }
}
