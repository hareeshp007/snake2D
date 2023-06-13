using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    public string LobbyLevel;
    

    public void RestartLevel()
    {
        int currLevel=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currLevel);
    }
    public void Lobby()
    {
        SceneManager.LoadScene(LobbyLevel);
    }
    public void EnableTutorial()
    {
       this.gameObject.SetActive(true);  
    }
    
}
