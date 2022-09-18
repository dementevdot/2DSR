using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalScore : DisplayingParameters
{
    [SerializeField] private Score _score;

    private TMP_Text _totalScore;

    private void OnEnable()
    {
        SetTotalScore();
    }

    private void Awake()
    {
        _totalScore = GetComponent<TMP_Text>();
    }

    private void SetTotalScore()
    {
        _totalScore.text = $"TOTAL SCORE: {Mathf.Round(_score.TotalScore)}";
    }
}
