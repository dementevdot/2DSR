using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class GameOverScore : DisplayingParameters
{
    [SerializeField] private Score _score;

    private TMP_Text _bestScore;

    private void OnEnable()
    {
        SetGameOverScore();
    }

    private void Awake()
    {
        _bestScore = GetComponent<TMP_Text>();
    }

    private void SetGameOverScore()
    {
        _bestScore.text = $"SCORE: {Mathf.Round(_score.CurrentScore)}";
    }
}
