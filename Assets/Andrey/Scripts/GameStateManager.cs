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

    //Enum for MajorStates
    public enum MajorGameStates { MENU, GAME, GAMEOVER, GAMEWIN }
    private MajorGameStates e_majorGameStates;
    //Enum for SubStates
    public enum GameplayGameStates { RUNNING, PAUSED }
    private GameplayGameStates e_gameplayGameStates;

    public GameObject mainMenuPanel;
    public GameObject multiplayerPanel;
    public GameObject optionsPanel;
    public GameObject optionsSubPanel;


    //Swithing between MajorStates
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
                DoChangeGameplayGameState(GameplayGameStates.PAUSED);
                break;
        }
    }

    //Switching between SubStates
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

    public Button FreeForAll, TeamDeathMacth, LastManStanding; // Multiplayer menu buttons
    public Button Controls, HowToPlay, Audio, Video; // Options menu buttons
    public Button GameWinReplay; // Replay button GameWin state
    public Button GameOverReplay; // Replay button GameOver state

    // HERE WE CAN CONTROL WHICH GAMEPLAY MODE ACTIVATE ----->
    private void RunFreeForaAllMode()
    {
        Debug.Log("FREE FOR ALL MODE IS ACITVE");

        GameWinReplay.onClick.RemoveAllListeners();
        GameOverReplay.onClick.RemoveAllListeners();
        GameWinReplay.onClick.AddListener(RunFreeForaAllMode);
        GameOverReplay.onClick.AddListener(RunFreeForaAllMode);

        StartGame();
    }

    private void RunTeamDeathMacthMode()
    {
        Debug.Log("TEAM DEATH MACTH MODE IS ACITVE");

        GameWinReplay.onClick.RemoveAllListeners();
        GameOverReplay.onClick.RemoveAllListeners();
        GameWinReplay.onClick.AddListener(RunTeamDeathMacthMode);
        GameOverReplay.onClick.AddListener(RunTeamDeathMacthMode);

        StartGame();
    }

    private void RunLastManStandingMode()
    {
        Debug.Log("LAST MAN STANDING MODE IS ACITVE");

        GameWinReplay.onClick.RemoveAllListeners();
        GameOverReplay.onClick.RemoveAllListeners();
        GameWinReplay.onClick.AddListener(RunLastManStandingMode);
        GameOverReplay.onClick.AddListener(RunLastManStandingMode);

        StartGame();
    }

    // HERE WE CAN CONTROL WHICH STATE FROM OPTIONS MENU ACTIVATE ----->
    private void RunControlOptions()
    {
        Debug.Log("HERE WE CAN SEE CONTROL OPTIONS");
        
    }
    private void RunHowToPlayOptions()
    {
        Debug.Log("HERE WE CAN SEE HOW TO PLAY INSTRUCTIONS");
    }
    private void RunAudioOptions()
    {
        Debug.Log("HERE WE CAN SEE AUDIO OPTIONS");
    }
    private void RunVideoOptions()
    {
        Debug.Log("HERE WE CAN SEE VIDEO OPTIONS");
    }


    private void Start()
    {
        GameMenu();
        mainMenuPanel.SetActive(true);
        multiplayerPanel.SetActive(false);
        optionsPanel.SetActive(false);
        optionsSubPanel.SetActive(false);

        FreeForAll.onClick.AddListener(RunFreeForaAllMode); // Running FreeForAll button listener
        TeamDeathMacth.onClick.AddListener(RunTeamDeathMacthMode); // Running TeamDeathMacth button listener
        LastManStanding.onClick.AddListener(RunLastManStandingMode); // Running LastManStanding button listener

        Controls.onClick.AddListener(RunControlOptions); // Running Control button listener
        HowToPlay.onClick.AddListener(RunHowToPlayOptions); // Running HowToPlay button listener
        Audio.onClick.AddListener(RunAudioOptions); // Running Audio button listener
        Video.onClick.AddListener(RunVideoOptions); // Running Video button listener

    }


    //Main menu activation
    public void SetMainMenu()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        optionsSubPanel.SetActive(false);
        multiplayerPanel.SetActive(false);
    }

    //Multiplayer menu activation
    public void SetMultiplayerMenu()
    {
        multiplayerPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        optionsSubPanel.SetActive(false);
    }

    //Options menu activation
    public void SetOptionsMenu()
    {
        optionsPanel.SetActive(true);
        optionsSubPanel.SetActive(false);
        multiplayerPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
    }

    //Options menu activation
    public void SetSubOptionMenu()
    {
        optionsSubPanel.SetActive(true);
        optionsPanel.SetActive(false);
        multiplayerPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
    }



    // Game Manu State
    public void GameMenu()
    {
        DoChangeMajorGameState(MajorGameStates.MENU);
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        multiplayerPanel.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Alpha2) && !IsGameOver && !IsGamePaused)
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
