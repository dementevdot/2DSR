using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasComponents : MonoBehaviour
{
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private Speedometer _speedometer;
    [SerializeField] private CurrentScore _currentScore;

    public StartScreen StartScreen => _startScreen;
    public GameOverScreen GameOverScreen => _gameOverScreen;
    public Speedometer Speedometer => _speedometer;
    public CurrentScore CurrentScore => _currentScore;
}
