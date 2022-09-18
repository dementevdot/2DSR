using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Car))]
[RequireComponent(typeof(CarMover))]
public class Score : MonoBehaviour
{
    [SerializeField] private int _defaltRatio;
    [SerializeField] private float _ratioIncreeseTime;

    private float _currentScore;
    private float _bestScore;
    private float _totalScore;
    private int _ratio;
    private float _secondsBetweenRatioIncreese;
    private Car _car;
    private CarMover _carMover;

    public float TotalScore => _totalScore;
    public float CurrentScore => _currentScore;
    public int Ratio => _ratio;

    private void Awake()
    {
        _car = GetComponent<Car>();
        _carMover = GetComponent<CarMover>();
        _currentScore = 0;
        _totalScore = 0;
    }

    private void OnEnable()
    {
        _car.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _car.GameOver -= OnGameOver;
    }

    public void AddScore(float score)
    {
        _currentScore += score;
    }

    private void FixedUpdate()
    {
        if (_carMover.IsOppositeDirection)
        {
            if (_secondsBetweenRatioIncreese <= 0)
            {
                _ratio++;
                _secondsBetweenRatioIncreese = _ratioIncreeseTime;
            }
            else
            {
                _secondsBetweenRatioIncreese -= Time.deltaTime;
            }
        }
        else
        {
            _ratio = _defaltRatio;
            _secondsBetweenRatioIncreese = _ratioIncreeseTime;
        }

        _currentScore += _carMover.CarCurrentSpeed * _ratio * Time.deltaTime;
    }

    private void OnGameOver() 
    {
        if (_currentScore > _bestScore)
            _bestScore = _currentScore;

        _totalScore += _currentScore;

        _currentScore = 0;

    }
}
