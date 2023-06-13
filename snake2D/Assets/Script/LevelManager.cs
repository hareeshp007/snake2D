using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    public string SinglePlayerLevel;
    public string CoOpLevel;
    private void Awake()
    {
        
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void StartSinglePlayer()
    {
        SceneManager.LoadScene(SinglePlayerLevel);
    }
    public void StartCoop()
    {
        SceneManager.LoadScene(CoOpLevel);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
