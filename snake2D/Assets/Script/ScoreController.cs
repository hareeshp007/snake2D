using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public int score = 0;
    // Start is called before the first frame update
    void Awake()
    {
        ScoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Start()
    {
        RefreshGUI();
    }
    public void ChangeColor(bool isshield)
    {
        if (isshield)
        {
            ScoreText.color = Color.blue;
        }
        else
        {
            ScoreText.color=Color.red;
        }
    }
    public void ScoreCounter()
    {
        score++;
        Debug.Log(score);
        RefreshGUI();
    }
    public void RefreshGUI()
    {
        ScoreText.text = "Score :" + score;
    }
}
