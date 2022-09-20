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
    [SerializeField] private ScreenController _screenController;

    private float _currentScore;
    private float _bestScore;
    private float _totalScore;
    private int _ratio;
    private float _secondsBetweenRatioIncreese;
    private CarMover _carMover;
    private Coroutine _currentRatioIncreeseCountdownCoroutine;

    public float CurrentScore => _currentScore;
    public float BestScore => _bestScore;
    public float TotalScore => _totalScore;
    public int Ratio => _ratio;

    public event UnityAction BestScoreUpdated;

    private void Awake()
    {
        _carMover = GetComponent<CarMover>();
        _currentScore = 0;
        _totalScore = 0;
    }

    private void OnEnable()
    {
        _screenController.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _screenController.GameOver -= OnGameOver;
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

                if (_currentRatioIncreeseCountdownCoroutine != null)
                    StopCoroutine(_currentRatioIncreeseCountdownCoroutine);

                _currentRatioIncreeseCountdownCoroutine = StartCoroutine(RatioIncreeseCountdown());
            }
            else if (_secondsBetweenRatioIncreese == _ratioIncreeseTime)
            {
                if (_currentRatioIncreeseCountdownCoroutine != null)
                    StopCoroutine(_currentRatioIncreeseCountdownCoroutine);

                _currentRatioIncreeseCountdownCoroutine = StartCoroutine(RatioIncreeseCountdown());
            }
        }
        else
        {
            if (_currentRatioIncreeseCountdownCoroutine != null)
                StopCoroutine(_currentRatioIncreeseCountdownCoroutine);

            _ratio = _defaltRatio;
            _secondsBetweenRatioIncreese = _ratioIncreeseTime;
        }

        _currentScore += _carMover.CarCurrentSpeed * _ratio * Time.deltaTime;
    }

    private IEnumerator RatioIncreeseCountdown()
    {
        while (_secondsBetweenRatioIncreese > 0)
        {
            _secondsBetweenRatioIncreese -= Time.deltaTime;

            yield return null;
        }
    }

    private void OnGameOver() 
    {
        if (_currentScore > _bestScore)
            _bestScore = _currentScore;

        BestScoreUpdated?.Invoke();

        _totalScore += _currentScore;

        _currentScore = 0;
    }
}
