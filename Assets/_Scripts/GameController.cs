using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameSession gameSession;
    [SerializeField] GameObject[] heroes;

    public int totalCoins = 2000;

    private GameObject spawnPoint;
    private CinemachineVirtualCamera cam;
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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "Main Menu")
        {
            spawnPoint = GameObject.Find("Spawn Point");
            cam = FindObjectOfType<CinemachineVirtualCamera>();
            var instantiated =  Instantiate(heroes[gameSession.selectedHero], spawnPoint.transform.position, Quaternion.identity);
            cam.Follow = instantiated.transform;
            gameSession.coinsInCurrentLevel = 0;
            gameSession.expInCurrentLevel = 0;
        }
    }
    
    void Update()
    {
        
    }
}
