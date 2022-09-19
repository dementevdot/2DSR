using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScore : DisplayingParameters
{
    [SerializeField] private Score _score;

    private TMP_Text _bestScore;

    private void OnEnable()
    {
        SetBestScore();
        _score.BestScoreUpdated += SetBestScore;
    }

    private void OnDisable()
    {
        _score.BestScoreUpdated -= SetBestScore;
    }

    private void Awake()
    {
        _bestScore = GetComponent<TMP_Text>();
    }

    private void SetBestScore()
    {
        _bestScore.text = $"BEST: {Mathf.Round(_score.BestScore)}";
    }
}
