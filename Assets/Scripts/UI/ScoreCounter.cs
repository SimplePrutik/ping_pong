using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{

    public Text scoreValue;

    private void Start()
    {
        GameManager.OnScoreSet += SetScore;
    }

    void SetScore(string score, bool timer)
    {
        scoreValue.color = timer ? Color.red : Color.black;
        scoreValue.text = score;
    }

    private void OnDestroy()
    {
        GameManager.OnScoreSet -= SetScore;
    }
}
