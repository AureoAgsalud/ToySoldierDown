using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        Paused,
        GameIntro,
        GameStart,
        GamePlaying,
        GameOver,
        GameEnd,
        LevelStart,
        LevelEnd,
    }

    public GameState currentGameState = GameState.GameIntro;

    // - Singleton Pattern
    public static GameManager instance = null;

    // - Reference to Player Camera
    public Camera playerCamera = null;

    // - Key card
    public bool hasKeyCard = false;
    public GameObject pauseMenu;

    // - Level Manager
    public LevelManager currentLevel;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Start()
    {
        Debug.Log("GameManager Started");
        ChangeState(GameState.GameStart);
    }
    private void Update()
    {
        if (PlayerInput.Instance.escapePressed)
        {
            if (currentGameState == GameState.Paused)
            {
                pauseMenu.SetActive(false);
                ChangeState(GameState.GamePlaying);
            }
            else if (currentGameState == GameState.GamePlaying)
            {
                ChangeState(GameState.Paused);
                pauseMenu.SetActive(true);
            }
        }
    }
    public void CinematicStarted()
    {
        Debug.Log("Cinematic Started");
        ChangeState(GameManager.GameState.LevelStart);
    }
    public void CinematicEnded()
    {
        Debug.Log("Cinematic Ended");
        playerCamera.transform.localPosition = Vector3.zero;
        ChangeState(GameManager.GameState.GamePlaying);
    }
    public void ChangeState(GameState newState)
    {
        currentGameState = newState;

        switch (newState)
        {
            case GameState.GameIntro:
                OnGameIntro();
                break;
            case GameState.GameStart:
                OnGameStart();
                break;
            case GameState.GamePlaying:
                OnGamePlaying();
                break;
            case GameState.GameOver:
                OnGameOver();
                break;
            case GameState.GameEnd:
                OnGameEnd();
                break;
            case GameState.LevelStart:
                OnLevelStart();
                break;
            case GameState.LevelEnd:
                OnLevelEnd();
                break;
            case GameState.Paused:
                OnGamePause();
                break;

        }
    }

    private void OnGameIntro()
    {
        Debug.Log("GameIntro State");
    }
    private void OnGameStart()
    {
        // - Do wahatever we need to do when the game "starts"
        Debug.Log("GameStart State");

        //Initialize what I need to Initialize for the game to be playable

        // - Once it is all done, begin playing
        ChangeState(GameState.GamePlaying);
    }
    private void OnGameEnd()
    {
        Debug.Log("Game End State");
        // - Logic that pertains the overarching game experience
        // - Calculating a score, determinging if the player won or lost the game
        // - Figuring what the next level will or be or next scene

    }
    private void OnGameOver()
    {
        Debug.Log("Game Over State");
        // - Handles logic when the player loses / dies
    }
    private void OnLevelStart()
    {
        // - Start Cinematics/ Instruction / tutorial video
        currentLevel.LevelStart();

        //StartCoroutine(ChangeStateDelay(GameState.GamePlaying, 2f));
    }
    private void OnGamePlaying()
    {
        Debug.Log("GamePlaying State");
        Time.timeScale = 1f;
    }
    private void OnLevelEnd()
    {
        Debug.Log("LevelEnd State");
        // - When we finish a level/ hit that ending cut scene
        currentLevel.LevelEnd();
    }
    private void OnGamePause()
    {
        Debug.Log("GamePause State");
        Time.timeScale = 0f; // freezes Game Time
    }

    // - Responsible for Making the game do its thing

    // - Scenes / Levels
    // - Title Screen 
    // - Level 1 
    // - Level 2
    // - Level 3

    // - Game States
    // - Pause
    // - Game Over
    // - OnGameStart
    // - OnGameEnd
}
