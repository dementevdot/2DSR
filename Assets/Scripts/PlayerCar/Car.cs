using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UserInput))]
public class Car : MonoBehaviour
{
    [SerializeField] private CarSpecs _carSpecs;
    [SerializeField] private CarMover _carMover;
    [SerializeField] private CarAnimator _carAnimator;
    [SerializeField] private Score _score;

    private Vector3 _startPosition;
    private UserInput _userInput;   
    private float _currentSpeed = 0;
    private float _mileage = 0;

    public float MaxSpeed => _carSpecs.MaxSpeed;
    public float MinSpeed => _carSpecs.MinSpeed;
    public float Acceleration => _carSpecs.Acceleration;
    public float Bracking => _carSpecs.Braking;
    public float Handleabitity => _carSpecs.Handleability;
    public float Mileage => _mileage;
    public float CurrentSpeed => _currentSpeed;
    public float CurrentScore => _score.CurrentScore;
    public float ScoreRatio => _score.Ratio;

    public event Action Collision;

    private void Awake()
    {
        _userInput = GetComponent<UserInput>(); 
        _startPosition = transform.position;
        _carMover.Init(this, _userInput);
        _carAnimator.Init(this);
        _score.Init(this, _carMover);
    }

    private void OnEnable()
    {
        _carMover.OnEnable();
    }

    private void OnDisable()
    {
        _carMover.OnDisable();  
    }

    private void Start()
    {
        _carMover.Start();
    }

    private void FixedUpdate()
    {
        _carMover.FixedUpdate();
    }

    private void Update()
    {
        _carAnimator.Update();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collision.Invoke();
        _score.GameOver();
        _carMover.DisableCarMoving();
    }

    public void Reset()
    {
        _currentSpeed = 0;
        _mileage = 0;
        transform.position = _startPosition;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        _carMover.OnCarReset(); 
    }

    public void Init()
    {

    }

    public void AddMileage(float distance) 
    {
        _mileage += distance;
    }

    public void IncreaseSpeed(float speed)
    {
        if (speed > 0)
            if (_currentSpeed + speed <= _carSpecs.MaxSpeed)
                _currentSpeed += speed;
    }

    public void DecreaseSpeed(float speed)
    {
        if (speed > 0)
            if (_currentSpeed - speed >= _carSpecs.MinSpeed)
                _currentSpeed -= speed;
    }

    [System.Serializable]
    private class CarSpecs
    {
        public float MaxSpeed;
        public float MinSpeed;
        public float Acceleration;
        public float Braking;
        public float Handleability;
    }

    [System.Serializable]
    private class CarMover
    {
        [SerializeField] private List<float> _laneCoordinates;

        private Car _car;
        private int _carPosition;
        private int _laneCount;
        private UserInput _userInput;
        private bool _isOppositeDirection = false;
        private bool _movingDisabled = false;
        private bool _playerAccelerationDisabled = false;
        private Coroutine _currentLaneChangeCoroutine;
        private Coroutine _currentFirstAccelerationCoroutine;

        public bool IsOppositeDirection => _isOppositeDirection;
        public float CarCurrentSpeed => _car.CurrentSpeed;

        public void Init(Car car, UserInput userInput)
        {
            _car = car;
            _userInput = userInput;
            _carPosition = 1;

            if (_laneCoordinates.Count > 0)
                _laneCount = _laneCoordinates.Count;
            else
                throw new NullReferenceException();
        }

        public void Start()
        {
            _playerAccelerationDisabled = true;

            if (_currentFirstAccelerationCoroutine != null)
                _car.StopCoroutine(_currentFirstAccelerationCoroutine);

            _currentFirstAccelerationCoroutine = _car.StartCoroutine(FirstAcceletation());
        }

        public void OnEnable()
        {
            _userInput.InputUpdated += Move;
        }

        public void OnDisable()
        {
            _userInput.InputUpdated -= Move;
        }

        public void FixedUpdate()
        {
            _car.transform.position += new Vector3(_car.CurrentSpeed * Time.deltaTime, 0, 0);
            _car.AddMileage(_car.CurrentSpeed * Time.deltaTime);
        }

        public void OnCarReset()
        {
            _movingDisabled = false;
            _isOppositeDirection = false;
            _carPosition = 1;

            Start();
        }

        private void Move(Vector3 vector)
        {
            int minimumPositionFromWhichCanChangeLaneToTheRight = 0;

            if (_movingDisabled == false)
            {
                switch (vector.z)
                {
                    case 1:
                        if (_carPosition < _laneCount - 1)
                            StartLaneChange(true);

                        return;
                    case -1:
                        if (_carPosition > minimumPositionFromWhichCanChangeLaneToTheRight)
                            StartLaneChange(false);

                        return;
                }
            }

            if (_playerAccelerationDisabled == false)
            {
                switch (vector.x)
                {
                    case 1:
                        _car.IncreaseSpeed(_car.Acceleration * Time.deltaTime);
                        return;
                    case -1:
                        _car.DecreaseSpeed(_car.Bracking * Time.deltaTime);

                        return;
                }
            }
        }

        private void StartLaneChange(bool isLeftDirection)
        {
            if (_currentLaneChangeCoroutine != null)
                _car.StopCoroutine(_currentLaneChangeCoroutine);

            _currentLaneChangeCoroutine = _car.StartCoroutine(LaneChange(isLeftDirection));
        }

        private IEnumerator LaneChange(bool isLeftDirection)
        {
            int roadDirections = 2;

            Vector3 direction = _car.transform.position;

            if (isLeftDirection)
            {
                if (_carPosition < _laneCount)
                {
                    direction = new Vector3(0, _laneCoordinates[++_carPosition], _carPosition);

                    if (_carPosition == _laneCount / roadDirections)
                        _isOppositeDirection = true;
                }
            }
            else
            {
                if (_carPosition > 0)
                {
                    direction = new Vector3(0, _laneCoordinates[--_carPosition], _carPosition);

                    if (_carPosition == _laneCount / roadDirections - 1)
                        _isOppositeDirection = false;
                }
            }

            while (_car.transform.position.y != direction.y || _car.transform.position.z != direction.z)
            {
                _car.transform.position = new Vector3(
                    _car.transform.position.x,
                    Mathf.MoveTowards(_car.transform.position.y, direction.y, Time.deltaTime * _car.Handleabitity),
                    Mathf.MoveTowards(_car.transform.position.z, direction.z, Time.deltaTime * _car.Handleabitity));

                yield return null;
            }
        }

        private IEnumerator FirstAcceletation()
        {
            while (_car.CurrentSpeed < _car.MinSpeed)
            {
                _car.IncreaseSpeed(_car.Acceleration * Time.deltaTime);
                yield return null;
            }

            _playerAccelerationDisabled = false;
        }

        public void DisableCarMoving()
        {
            if (_currentLaneChangeCoroutine != null)
                _car.StopCoroutine(_currentLaneChangeCoroutine);

            _movingDisabled = true;
        }
    }

    [System.Serializable]
    private class CarAnimator
    {
        [SerializeField] private GameObject _frontWheel;
        [SerializeField] private GameObject _rearWheel;

        private Car _car;
        private float _wheelRotation = 0;
        private int _ratio = 200;

        public void Init(Car car)
        {
            _car = car;
        }

        public void Update()
        {
            _wheelRotation = _car.CurrentSpeed * _ratio * Time.deltaTime;

            _frontWheel.transform.Rotate(0, 0, -_wheelRotation);
            _rearWheel.transform.Rotate(0, 0, -_wheelRotation);
        }
    }

    [System.Serializable]
    private class Score
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
        private Coroutine _currentRatioIncreeseCountdownCoroutine;

        public float CurrentScore => _currentScore;
        public float BestScore => _bestScore;
        public float TotalScore => _totalScore;
        public int Ratio => _ratio;

        public event Action BestScoreUpdated;

        public void Init(Car car,CarMover carMover)
        {
            _car = car;
            _carMover = carMover;
            _currentScore = 0;
            _totalScore = 0;
        }

        public void AddScore(float score)
        {
            _currentScore += score;
        }

        public void GameOver()
        {
            if (_currentScore > _bestScore)
                _bestScore = _currentScore;

            BestScoreUpdated?.Invoke();

            _totalScore += _currentScore;

            _currentScore = 0;
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
                        _car.StopCoroutine(_currentRatioIncreeseCountdownCoroutine);

                    _currentRatioIncreeseCountdownCoroutine = _car.StartCoroutine(RatioIncreeseCountdown());
                }
                else if (_secondsBetweenRatioIncreese == _ratioIncreeseTime)
                {
                    if (_currentRatioIncreeseCountdownCoroutine != null)
                        _car.StopCoroutine(_currentRatioIncreeseCountdownCoroutine);

                    _currentRatioIncreeseCountdownCoroutine = _car.StartCoroutine(RatioIncreeseCountdown());
                }
            }
            else
            {
                if (_currentRatioIncreeseCountdownCoroutine != null)
                    _car.StopCoroutine(_currentRatioIncreeseCountdownCoroutine);

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
    }
}
