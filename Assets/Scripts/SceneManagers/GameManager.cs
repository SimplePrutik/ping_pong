using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject exitBtn;

    public static Action OnNewGameStarted = delegate { };

    public static Action<string, bool> OnScoreSet = delegate { };

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private int score;

    void Start()
    {
        exitBtn.SetActive(false);
#if UNITY_EDITOR
        exitBtn.SetActive(true);
#endif
        OutBorder.OnGameOvered += EndGame;
        StartGame();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitToMenu();
            }
        }
    }

    void EndGame()
    {
        StartCoroutine(GameOver());
    }

    void StartGame()
    {  
        StartCoroutine(NewGame());
    }

    IEnumerator NewGame()
    {
        yield return new WaitForSeconds(0.2f);
        OnScoreSet("Ready", true);
        yield return new WaitForSeconds(1);
        OnScoreSet("Steady", true);
        yield return new WaitForSeconds(1);
        OnScoreSet("Go!", true);
        yield return new WaitForSeconds(0.2f);
        ResetScore();
        OnNewGameStarted();
    }

    IEnumerator GameOver()
    {
        OnScoreSet($"Result: {score}", false);
        yield return new WaitForSeconds(2);
        StartGame();
    }

    void ResetScore()
    {
        score = 0;
        OnScoreSet(score.ToString(), false);
    }

    public void IncScore()
    {
        score++;
        OnScoreSet(score.ToString(), false);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void OnDestroy()
    {
        OutBorder.OnGameOvered -= EndGame;
    }
}
