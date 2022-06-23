using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject prepareObject;
    public GameObject battleObject;
    public GameObject winObject;
    public TextMeshProUGUI victoryName;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUIMode(EUIMode mode)
    {
        prepareObject.SetActive(false);
        battleObject.SetActive(false);
        winObject.SetActive(false);

        switch (mode)
        {
            case EUIMode.prepare:
                prepareObject.SetActive(true);
                break;
            case EUIMode.battle:
                battleObject.SetActive(true);
                break;
            case EUIMode.win:
                winObject.SetActive(true);
                break;
            default:
                Debug.LogWarning("UI Mode setting broke");
                break;
        }
    }
}

public enum EUIMode
{
    prepare,
    battle,
    win
}