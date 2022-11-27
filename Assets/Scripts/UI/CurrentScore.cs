using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class CurrentScore : DisplayingParameters
{
    private Car _car;
    private TMP_Text _currentScore;

    private void Awake()
    {
        _currentScore = GetComponent<TMP_Text>();
    }   

    private void Update()
    {
        if (_car.ScoreRatio > 1)
        {
            _currentScore.text = $"{Mathf.Round(_car.CurrentScore)} X{_car.ScoreRatio}";
            Debug.Log("1");
        }
        else
        {
            _currentScore.text = Mathf.Round(_car.CurrentScore).ToString();
            Debug.Log("0");
        }
    }

    public void Init(Car car)
    {
        _car = car;
    }
}
