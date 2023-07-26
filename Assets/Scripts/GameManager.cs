using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject playerPrefab;

    public GameObject menuPanel;
    public GameObject playPanel;
    public GameObject levelCompletePanel;
    public GameObject gameOverPanel;

    public Text scoreText;
    public Text ballsText;
    public Text levelText;

    public GameObject[] levels;

    public static GameManager Instance { get; private set; }

    public enum State {MENU, INIT, PLAY, LEVELCOMPLETE, LOADLEVEL, GAMEOVER}
    State state;
    GameObject currentBall;
    GameObject currentLevel;
    bool isSwitchingState;

    private int score;

    public int Score
    {
        get { return score; }
        set { score = value;
            scoreText.text = "SCORE: " + score;
        }
    }

    private int balls;

    public int Balls
    {
        get { return balls; }
        set { balls = value; }
    }

    private int level;

    public int Level
    {
        get { return level; }
        set { level = value; }
    }


    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }

    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState,delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        state = newState;
        BeginState(newState);
        isSwitchingState = false;
    }

    private void Start()
    {
        Instance = this;
        SwitchState(State.MENU);
    }

    private void Update()
    {
        switch (state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                if (currentBall == null)
                {
                    if (Balls > 0)
                    {
                        currentBall = Instantiate(ballPrefab);
                    }
                    else
                    {
                        SwitchState(State.GAMEOVER);
                    }
                }
                break;
            case State.LEVELCOMPLETE:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                break;
        }
    }

    void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                menuPanel.SetActive(true);
                break;
            case State.INIT:
                playPanel.SetActive(true);
                Score = 0;
                Level = 0;
                Balls = 3;
                Instantiate(playerPrefab);
                SwitchState(State.LOADLEVEL);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETE:
                levelCompletePanel.SetActive(true);
                break;
            case State.LOADLEVEL:
                if (Level >= levels.Length)
                {
                    SwitchState(State.GAMEOVER);
                }
                else
                {
                    currentLevel = Instantiate(levels[Level]);
                    SwitchState(State.PLAY);
                }
                break;
            case State.GAMEOVER:
                gameOverPanel.SetActive(true);
                break;
        }
    }

    void EndState()
    {
        switch (state)
        {
            case State.MENU:
                menuPanel.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETE:
                levelCompletePanel.SetActive(false);
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                playPanel.SetActive(false);
                gameOverPanel.SetActive(false);
                break;
        }
    }
}
