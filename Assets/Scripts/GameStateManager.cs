using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour {

    #region Singleton

    public static GameStateManager cl_GameStateManager;

    void Awake()
    {

        cl_GameStateManager = this;
    }
    #endregion 

    public GameObject[] gameStates;
    public enum GameStates { MENU, GAME, PAUSE, GAMEOVER, GAMEWIN }
    private GameStates e_gameStates;
   

    public bool b_IsGamePaused = false;
    public bool b_IsGameModeActive = false;
    public bool b_IsGameOver = false;
    public bool b_IsGameWin = false;


    private void Start()
    {
        GameMenu();
    }


    // Game Manu State
    public void GameMenu()
    {
        b_IsGameModeActive = false;
        b_IsGamePaused = false;
        b_IsGameOver = false;
        b_IsGameWin = false;
        e_gameStates = GameStates.MENU;
        ChangeGameStates();

    }

    // Game State
    public void StartGame()
    {
        b_IsGameModeActive = true;
        e_gameStates = GameStates.GAME;
        ChangeGameStates();

    }
    // Activate the Pause State
    public void PauseGame()
    {
        e_gameStates = GameStates.PAUSE;
        ChangeGameStates();
        b_IsGameModeActive = false;
        b_IsGamePaused = true;
        Time.timeScale = 0f;

    }
    //Unpause game
    public void UnpausedGame()
    {
        e_gameStates = GameStates.GAME;
        ChangeGameStates();
        b_IsGameModeActive = true;
        b_IsGamePaused = false;
        Time.timeScale = 1f;
    }
    // Game Over State
    internal void GameOver()
    {
        gameStates[1].SetActive(false);
        gameStates[3].SetActive(true);
        b_IsGameOver = true;

    }
    // Game Win State
    internal void GameWin()
    {
        gameStates[1].SetActive(false);
        gameStates[4].SetActive(true);
        b_IsGameWin = true;

    }
    // Game Exit
    public void GameExit()
    {
        Application.Quit();
    }


    public void ReloadScene()
    { SceneManager.LoadScene(0); }



    // Switching between states based on ENUM
    private void ChangeGameStates()
    {

        switch (e_gameStates)
        {
            case GameStates.MENU:
                gameStates[0].SetActive(true); // MENU
                gameStates[1].SetActive(false); // GAME
                gameStates[2].SetActive(false); // PAUSE
                gameStates[3].SetActive(false); // GAMEOVER
                gameStates[4].SetActive(false); // GAMEWIN


                break;

            case GameStates.GAME:
                gameStates[0].SetActive(false); // MENU
                gameStates[1].SetActive(true); // GAME
                gameStates[2].SetActive(false); // PAUSE
                gameStates[3].SetActive(false); // GAMEOVER
                gameStates[4].SetActive(false); // GAMEWIN

                break;

            case GameStates.PAUSE:
                gameStates[0].SetActive(false); // MENU
                gameStates[1].SetActive(true); // GAME
                gameStates[2].SetActive(true); // PAUSE
                gameStates[3].SetActive(false); // GAMEOVER
                gameStates[4].SetActive(false); // GAMEWIN
                break;

            case GameStates.GAMEOVER:
                gameStates[0].SetActive(false); // MENU
                gameStates[1].SetActive(false); // GAME
                gameStates[2].SetActive(false); // PAUSE
                gameStates[3].SetActive(true); // GAMEOVER
                gameStates[4].SetActive(false); // GAMEWIN

                break;

            case GameStates.GAMEWIN:
                gameStates[0].SetActive(false); // MENU
                gameStates[1].SetActive(false); // GAME
                gameStates[2].SetActive(false); // PAUSE
                gameStates[3].SetActive(false); // GAMEOVER
                gameStates[4].SetActive(true); // GAMEWIN

                break;

        }

    }


    void Update () {

        //Just for test 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameMenu();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !b_IsGameOver)
        {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !b_IsGameOver)
        {
            if (b_IsGamePaused)
            {
                UnpausedGame();
            }
            else { PauseGame();}
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && !b_IsGameOver && b_IsGameModeActive)
        {
            GameOver();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && !b_IsGameWin && b_IsGameModeActive)
        {
            gameStates[4].SetActive(false);
            GameOver();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && !b_IsGameOver && b_IsGameModeActive)
        {
            gameStates[3].SetActive(false);
            GameWin();
        }
    }
}
