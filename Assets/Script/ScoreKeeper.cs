﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {
    int score;
    Text myText;

    public Text highScore;
	
	void Start () {
        myText = GetComponent<Text>();
        Reset();

        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
	}
	
	public void Score(int points)
    {
        score += points;
        myText.text = score.ToString();

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore.text = score.ToString();
        }
    }

    public void Reset()
    {
        score = 0;
        myText.text = score.ToString();
        //PlayerPrefs.DeleteKey("HighScore"); //reset highscore
    }
}
