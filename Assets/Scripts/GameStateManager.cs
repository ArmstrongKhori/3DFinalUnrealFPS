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
    public GameObject[] gameplayGameStates;

    public enum MajorGameStates { MENU, GAME, GAMEOVER, GAMEWIN }
    private MajorGameStates e_majorGameStates;
    public void DoChangeMajorGameState(MajorGameStates state)
    {
        gameStates[(int)e_majorGameStates].SetActive(false);
        //
        //
        e_majorGameStates = state;
        //
        //
        gameStates[(int)e_majorGameStates].SetActive(true);
        switch (e_majorGameStates)
        {
            case MajorGameStates.GAME:
                DoChangeGameplayGameState(GameplayGameStates.RUNNING);
                break;
        }
    }


    public enum GameplayGameStates { RUNNING, PAUSED }
    private GameplayGameStates e_gameplayGameStates;
    public void DoChangeGameplayGameState(GameplayGameStates state)
    {
        gameplayGameStates[(int)e_gameplayGameStates].SetActive(false);
        //
        //
        e_gameplayGameStates = state;
        //
        //
        gameplayGameStates[(int)e_gameplayGameStates].SetActive(true);
        switch (e_gameplayGameStates)
        {
            case GameplayGameStates.PAUSED:
                Time.timeScale = 0f;
                break;
            case GameplayGameStates.RUNNING:
                Time.timeScale = 1f;
                break;
        }

    }

    public bool IsGameActive { get { return (e_gameplayGameStates == GameplayGameStates.RUNNING); } }
    public bool IsGameOver { get { return (e_majorGameStates == MajorGameStates.GAMEOVER || e_majorGameStates == MajorGameStates.GAMEWIN); } }
    public bool IsGameWon { get { return (e_majorGameStates == MajorGameStates.GAMEWIN); } }
    public bool IsGamePaused { get { return (e_majorGameStates == MajorGameStates.GAME) && !IsGameActive; } }


    private void Start()
    {
        GameMenu();
    }


    // Game Manu State
    public void GameMenu()
    {
        DoChangeMajorGameState(MajorGameStates.MENU);
    }

    // Game State
    public void StartGame()
    {
        DoChangeMajorGameState(MajorGameStates.GAME);
    }
    // Activate the Pause State
    public void PauseGame()
    {
        DoChangeGameplayGameState(GameplayGameStates.PAUSED);
    }
    //Unpause game
    public void UnpausedGame()
    {
        DoChangeGameplayGameState(GameplayGameStates.RUNNING);
    }
    // Game Over State
    internal void GameOver()
    {
        DoChangeMajorGameState(MajorGameStates.GAMEOVER);
    }
    // Game Win State
    internal void GameWin()
    {
        DoChangeMajorGameState(MajorGameStates.GAMEWIN);
    }
    // Game Exit
    public void GameExit()
    {
        Application.Quit();
    }


    public void ReloadScene()
    { SceneManager.LoadScene(0); }
    



    void Update () {

        //Just for test 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameMenu();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !IsGameOver)
        {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !IsGameOver)
        {
            if (IsGamePaused)
            {
                UnpausedGame();
            }
            else { PauseGame();}
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && !IsGameOver)
        {
            GameOver();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && !IsGameOver)
        {
            GameWin();
        }
    }
}
