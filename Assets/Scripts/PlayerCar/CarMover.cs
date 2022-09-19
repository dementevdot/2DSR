using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Car))]
[RequireComponent(typeof(UserInput))]
public class CarMover : MonoBehaviour
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
    private Coroutine _currentStartAccelerationCoroutine;
    private int differenceBetweenCountingFromOneAndCountingFromZero = 1;

    public bool IsOppositeDirection => _isOppositeDirection;
    public float CarCurrentSpeed => _car.CurrentSpeed;

    private void Awake()
    {
        _car = GetComponent<Car>();
        _userInput = GetComponent<UserInput>();
        _laneCount = _laneCoordinates.Count;
    }

    private void Start()
    {
        _playerAccelerationDisabled = true;

        if (_currentStartAccelerationCoroutine != null)
            StopCoroutine(_currentStartAccelerationCoroutine);

        _currentStartAccelerationCoroutine = StartCoroutine(StartAcceleration());
    }

    private void OnEnable()
    {
        _car.ResetGame += OnGameReset;
        _userInput.InputUpdated += Move;
    }

    private void OnDisable()
    {
        _car.ResetGame -= OnGameReset;
        _userInput.InputUpdated -= Move;
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(_car.CurrentSpeed * Time.deltaTime, 0, 0);
        _car.AddMileage(_car.CurrentSpeed * Time.deltaTime);
    }

    private void OnGameReset()
    {
        _movingDisabled = false;
        _isOppositeDirection = false;
        _carPosition = 0;
        Start();
    }

    private void Move(Vector3 vector)
    {
        int minimumPositionFromWhichCanChangeLaneToTheRight = 0;

        if (_movingDisabled == false)
        {
            if (vector.z != 0)
            {
                if (vector.z == -1)
                {
                    if (_carPosition > minimumPositionFromWhichCanChangeLaneToTheRight)
                    {
                        StartLaneChange(false);
                    }
                }
                else if (vector.z == 1)
                {
                    if (_carPosition < _laneCount - differenceBetweenCountingFromOneAndCountingFromZero)
                    {
                        StartLaneChange(true);
                    }
                }
            }
        }


        if (_playerAccelerationDisabled == false)
        {
            if (vector.x != 0)
            {
                if (vector.x == -1)
                {
                    _car.DecreaseSpeed(_car.Bracking * Time.deltaTime);
                }
                else
                {
                    _car.IncreaseSpeed(_car.Acceleration * Time.deltaTime);
                }
            }
        }
    }

    private void StartLaneChange(bool isLeftDirection)
    {
        if (_currentLaneChangeCoroutine != null)
            StopCoroutine(_currentLaneChangeCoroutine);

        _currentLaneChangeCoroutine = StartCoroutine(LaneChange(isLeftDirection));
    }

    private IEnumerator LaneChange(bool isLeftDirection)
    {
        int numberWhenDividedByWhichYouCanGetHalfTheNumber = 2;

        Vector3 direction = transform.position;

        if (isLeftDirection)
        {
            if (_carPosition < _laneCount)
            {
                direction = new Vector3(0, _laneCoordinates[++_carPosition], _carPosition);

                if (_carPosition == _laneCount / numberWhenDividedByWhichYouCanGetHalfTheNumber)
                    _isOppositeDirection = true;
            }
        }
        else
        {
            if (_carPosition > 0)
            {
                direction = new Vector3(0, _laneCoordinates[--_carPosition], _carPosition);

                if (_carPosition == _laneCount / numberWhenDividedByWhichYouCanGetHalfTheNumber - differenceBetweenCountingFromOneAndCountingFromZero)
                    _isOppositeDirection = false;
            }
        }

        while (transform.position.y != direction.y && transform.position.z != direction.z)
        {
            transform.position = new Vector3(
                transform.position.x, 
                Mathf.MoveTowards(transform.position.y, direction.y, Time.deltaTime * _car.Handleabitity), 
                Mathf.MoveTowards(transform.position.z, direction.z, Time.deltaTime * _car.Handleabitity));

            yield return null;
        }
    }

    private IEnumerator StartAcceleration()
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
            StopCoroutine(_currentLaneChangeCoroutine);

        _movingDisabled = true;
    }
}
