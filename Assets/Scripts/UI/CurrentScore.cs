using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentScore : DisplayingParameters
{
    [SerializeField] private Score _score;

    private TMP_Text _currentScore;

    private void Awake()
    {
        _currentScore = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (_score.Ratio > 1)
        {
            _currentScore.text = $"{Mathf.Round(_score.CurrentScore)} X{_score.Ratio}";
        }
        else
        {
            _currentScore.text = Mathf.Round(_score.CurrentScore).ToString();
        }
    }
}
