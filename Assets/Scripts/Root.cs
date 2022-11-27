using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Prefabs _prefabs;
    [SerializeField] private List<TrafficGenerator> _trafficGenerators;

    private StartScreen _startScreen;
    private GameOverScreen _gameOverScreen;
    private Speedometer _speedometer;
    private CurrentScore _currentScore;
    private Car _car;
    private CameraFollowing _cameraFollowing;
    private RoadGenerator _roadGenerator;
    private const float  _cameraFollowingXOffset = 4;

    private void Awake()
    {
        if (Instantiate(_prefabs.Canvas).TryGetComponent<Canvas>(out Canvas canvas))
        {
            if (canvas.TryGetComponent<CanvasComponents>(out CanvasComponents canvasComponents))
            {
                _startScreen = canvasComponents.StartScreen;
                _gameOverScreen = canvasComponents.GameOverScreen;
                _speedometer = canvasComponents.Speedometer;
                _currentScore = canvasComponents.CurrentScore;
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        else
        {
            throw new NullReferenceException();
        }

        if (Instantiate(_prefabs.Car).TryGetComponent<Car>(out Car car))
            _car = car;
        else
            throw new NullReferenceException();

        if (_camera.TryGetComponent<CameraFollowing>(out CameraFollowing cameraFollowing))
            _cameraFollowing = cameraFollowing;
        else
            throw new NullReferenceException();

        if (Instantiate(_prefabs.RoadGenerator).TryGetComponent<RoadGenerator>(out RoadGenerator roadGenerator))
            _roadGenerator = roadGenerator;
        else
            throw new NullReferenceException();

        _car.Init();
        _cameraFollowing.Init(_car.GetComponent<Transform>(), _cameraFollowingXOffset);
        _roadGenerator.Init(_prefabs.Road, _car, Instantiate(_prefabs.EmptyGameObject));
        _currentScore.Init(_car);
        _speedometer.Init(_car);
    }

    private void OnEnable()
    {
        _startScreen.StartButtonClick += OnStartButtonClick;
        _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
        _car.Collision += EndGame;
    }

    private void OnDisable()
    {
        _startScreen.StartButtonClick -= OnStartButtonClick;
        _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
        _car.Collision -= EndGame;
    }

    private void Start()
    {
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

    private void EndGame()
    {
        Time.timeScale = 0;
        _speedometer.Close();
        _currentScore.Close();
        _gameOverScreen.Open();
    }


    [System.Serializable]
    private class Prefabs
    {
        public GameObject Canvas;
        public GameObject Car;
        public GameObject RoadGenerator;
        public GameObject Road;
        public GameObject EmptyGameObject;
    }
}

