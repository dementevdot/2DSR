using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class GameOverScore : DisplayingParameters
{
    private TMP_Text _bestScore;

    private void OnEnable()
    {
        SetGameOverScore(0);
    }

    private void Awake()
    {
        _bestScore = GetComponent<TMP_Text>();
    }

    private void SetGameOverScore(float currentScore)
    {
        _bestScore.text = $"SCORE: {Mathf.Round(currentScore)}";
    }
}
