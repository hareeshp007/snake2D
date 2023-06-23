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
        SoundManager.Instance.Play(Sounds.ButtonClick);
        int currLevel=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currLevel);
    }
    public void Lobby()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(LobbyLevel);
    }
    public void EnableTutorial()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        this.gameObject.SetActive(true);  
    }
    
}
