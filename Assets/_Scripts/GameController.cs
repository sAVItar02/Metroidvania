using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameSession gameSession;
    void Awake()
    {
        int numGameControllers = FindObjectsOfType<GameController>().Length;
        if(numGameControllers > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Update()
    {
        
    }
}
