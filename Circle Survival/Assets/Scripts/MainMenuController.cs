using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController: MonoBehaviour
{
    private Text _highScoreText;
    public void Start()
    {
        _highScoreText=GameObject.Find("HS_value_text").GetComponent<Text>();
        _highScoreText.text=PlayerPrefs.GetFloat("HighScore", 0).ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
