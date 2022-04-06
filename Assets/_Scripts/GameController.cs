using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameSession gameSession;
    [SerializeField] GameObject[] heroes;

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
        if(scene.name == "Level 1")
        {
            spawnPoint = GameObject.Find("Spawn Point");
            cam = FindObjectOfType<CinemachineVirtualCamera>();
            var instantiated =  Instantiate(heroes[gameSession.selectedHero], spawnPoint.transform.position, Quaternion.identity);
            cam.Follow = instantiated.transform;
            

        }
    }
    /*private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log(currentScene);

        if(currentScene == "Level 1")
        {
            spawnPoint = GameObject.Find("Spawn Point");
            Instantiate(heroes[gameSession.selectedHero], spawnPoint.transform.position, Quaternion.identity);
        }
    }*/
    void Update()
    {
        
    }
}
