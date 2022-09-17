using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Car))]
[RequireComponent(typeof(UserInput))]
public class CarMover : MonoBehaviour
{
    [SerializeField] private float _oppositeDirectionYCoordinate;
    [SerializeField] private float _passingDirectionYCoordinate;

    private Car _car;
    private UserInput _userInput;
    private float _currentSpeed = 0;
    private bool _isOppositeDirection = false;
    private Coroutine _currentLaneChangeCoroutine;

    private void Awake()
    {
        _car = GetComponent<Car>();
        _userInput = GetComponent<UserInput>();
    }

    private void OnEnable()
    {
        _userInput.InputUpdated += Move;
    }

    private void OnDisable()
    {
        _userInput.InputUpdated -= Move;
    }

    private void FixedUpdate()
    {
        _car.AddMileage(_currentSpeed * Time.deltaTime);

        transform.position += new Vector3(_currentSpeed * Time.deltaTime, 0, 0);
    }

    private void Move(Vector3 vector)
    {
        if (vector.z != 0)
        {
            if (_isOppositeDirection)
            {
                if (vector.z == -1)
                {
                    if (_currentLaneChangeCoroutine != null)
                        StopCoroutine(_currentLaneChangeCoroutine);

                    _currentLaneChangeCoroutine = StartCoroutine(LaneChange());

                    _isOppositeDirection = false;
                }
            }
            else
            {
                if (vector.z == 1)
                {
                    if (_currentLaneChangeCoroutine != null)
                        StopCoroutine(_currentLaneChangeCoroutine);

                    _currentLaneChangeCoroutine = StartCoroutine(LaneChange());

                    _isOppositeDirection = true;
                }
            }
        }

        if (vector.x != 0)
        {
            if (vector.x == -1)
            {
                if (_currentSpeed > _car.MinSpeed)
                    _currentSpeed -= _car.Bracking * Time.deltaTime;
            }
            else
            {
                if (_currentSpeed  < _car.MaxSpeed)
                    _currentSpeed += _car.Acceleration * Time.deltaTime;
            }
        }
    }

    private IEnumerator LaneChange()
    {
        Vector3 direction;

        if (_isOppositeDirection)
        {
            direction = new Vector3(0, _passingDirectionYCoordinate, 0);
        }
        else
        {
            direction = new Vector3(0, _oppositeDirectionYCoordinate, 0);
        }

        while (transform.position.y != direction.y)
        {
            transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, direction.y, Time.deltaTime * 5f), transform.position.z);

            yield return null;
        }
    }
}
