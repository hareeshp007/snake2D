using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI Score1Text;
    public bool IsPlayerCoOp=false;
    public int score = 0;
    public int score1 = 0;
    // Start is called before the first frame update
    void Awake()
    {
        ScoreText = GetComponent<TextMeshProUGUI>();
        if (IsPlayerCoOp)
        {
            Score1Text = GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Start()
    {
        RefreshGUI();
    }
    public void setIsPlayerCoOp(bool CoOp)
    {
        IsPlayerCoOp = CoOp;
    }
    public void ChangeColor(bool isshield,bool isPlayer1)
    {
        if (isshield&&isPlayer1)
        {
            ScoreText.color = Color.blue;
        }
        else if(isshield&&!isPlayer1)
        {   
            Score1Text.color=Color.blue;

        }else if (!isshield && isPlayer1)
        {
            ScoreText.color = Color.red;
        }else if (!isshield && !isPlayer1)
        {
            Score1Text.color = Color.red;
        }
    }
    public void ScoreCounter()
    {
        score++;
        Debug.Log(score);
        RefreshGUI();
    }
    public void ScoreDown()
    {
        score--;
        Debug.Log(score);
        RefreshGUI();
    }
    public void RefreshGUI()
    {
        ScoreText.text = "Score :" + score;
        if (IsPlayerCoOp) { Score1Text.text = "Score :" + score1; }
        
    }

    public void ScoreCounter1()
    {
        score1++;
        Debug.Log(score1);
        RefreshGUI();
    }
    public void ScoreDown1()
    {
        score1--;
        Debug.Log(score1);
        RefreshGUI();
    }
}
