using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : IFixedUpdateable
{
    private Text scoreText;
    private GameManager gameManager;

    public UI(Text _scoreText, GameManager _gameManager)
    {
        scoreText = _scoreText;
        gameManager = _gameManager;
    }

    public void OnFixedUpdate()
    {
        scoreText.text = "Score: " + gameManager.Score.ToString();
    }
}
