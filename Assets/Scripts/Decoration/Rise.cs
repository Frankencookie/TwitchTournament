using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rise : MonoBehaviour
{
    public float speed = 10;
    
    void Start()
    {
        Destroy(gameObject, 3);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }
}
