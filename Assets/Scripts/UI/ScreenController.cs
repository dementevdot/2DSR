using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenController : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private RoadGenerator _roadGenerator;
    [SerializeField] private List<TrafficGenerator> _trafficGenerators;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private Speedometer _speedometer;
    [SerializeField] private CurrentScore _currentScore;

    public event UnityAction GameOver;

    private void OnEnable()
    {
        _startScreen.StartButtonClick += OnStartButtonClick;
        _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
    }

    private void OnDisable()
    {
        _startScreen.StartButtonClick -= OnStartButtonClick;
        _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
    }

    private void Start()
    {
        _car.Reset();
        Time.timeScale = 0;
        _startScreen.Open(); 
        _gameOverScreen.Close();
        _speedometer.Close();
        _currentScore.Close();
    }

    private void OnStartButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }
    private void OnRestartButtonClick()
    {
        _gameOverScreen.Close();

        foreach (var generator in _trafficGenerators)
            generator.ResetPool();

        _roadGenerator.ResetPool();
        Start();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _car.Reset();
        _speedometer.Open();
        _currentScore.Open();
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        _speedometer.Close();
        _currentScore.Close();
        _gameOverScreen.Open();
        GameOver?.Invoke();
    }
}
