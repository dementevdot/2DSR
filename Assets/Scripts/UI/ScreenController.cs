using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private RoadGenerator _roadGenerator;
    [SerializeField] private TrafficGenerator _oppositeGenerator;
    [SerializeField] private TrafficGenerator _passingGenerator;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;

    private void OnEnable()
    {
        _startScreen.StartButtonClick += OnStartButtonClick;
        _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
        _car.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _startScreen.StartButtonClick -= OnStartButtonClick;
        _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
        _car.GameOver -= OnGameOver;
    }
    private void Start()
    {
        Time.timeScale = 0;
        _gameOverScreen.Close();
        _startScreen.Open();
    }

    private void OnStartButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }
    private void OnRestartButtonClick()
    {
        _gameOverScreen.Close();
        _oppositeGenerator.ResetPool();
        _passingGenerator.ResetPool();
        _roadGenerator.ResetPool();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _car.Reset();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _gameOverScreen.Open();
    }
}
