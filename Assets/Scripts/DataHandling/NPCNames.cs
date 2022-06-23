using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNames : MonoBehaviour
{
    public static NPCNames instance;

    public string[] names;

    public void OnEnable()
    {
        instance = this;
    }
}
