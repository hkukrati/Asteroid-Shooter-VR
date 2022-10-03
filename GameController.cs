// Main GameController script that manages the flow of the game and updates the player score. This script is tied to the XR camera rig. 

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int playerScore;
    private float timer;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private Canvas startMenu;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas timerCanvas;
    [SerializeField] private BoxCollider startButtonBoxCollider;

    // Enum to track the current Game State 
    public enum GameState
    {
        Menu,
        Playing,
        Gameover
    }

    public GameState currentGameStatus;

    // Set initial game state to just have the Start Menu visible
    void Awake()
    {
        currentGameStatus = GameState.Menu;
        startMenu.enabled = true;
        gameOverText.enabled = false;
        gameOverScoreText.enabled = false;
        timerCanvas.enabled = false;
    }

    void Update()
    {
        if (currentGameStatus == GameState.Playing)
        {
            // Subtract time between last frame from the total timer count 
            timer = timer - Time.deltaTime;

            // Display timer to the player 
            timerText.text = (Math.Ceiling(timer)).ToString();

            // If timer hits zero the game is over. Bring up the start menu and show the player their score
            if(timer <= 0)
            {
                gameOver();
                enableStartMenu();
            }
        }
    }

    public void updateScore(int score)
    {
        playerScore += score;
    }

    public void startGame()
    {
        // To start the game, enable the timer, clear the player score and set global GameStatus to "Playing"
        currentGameStatus = GameState.Playing;
        timerCanvas.enabled = true;
        resetGame();
        setTimer();
    }

    public int getCurrentScore()
    {
        return playerScore;
    }

    void gameOver()
    {
        // When the game is over, hide the timer from the player, bring up text that tells the player that the game is over and display the score obtained from the last game session
        currentGameStatus = GameState.Gameover;
        timerCanvas.enabled = false;
        gameOverText.enabled = true;
        gameOverScoreText.text = ($"You scored {playerScore.ToString()}");
        gameOverScoreText.enabled = true;
    }

    void enableStartMenu()
    {
        // Display the Start Menu and enable Start Menu collider so that player can restart the game
        currentGameStatus = GameState.Menu;
        startMenu.enabled = true;
        startButtonBoxCollider.enabled = true;
    }

    void setTimer()
    {
        timer = 30;
    }

    void resetGame()
    {
        playerScore = 0;
    }

}